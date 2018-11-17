using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;
using System.Numerics;

namespace BLITTEngine.Core.Graphics
{
    public enum CanvasFullscreenStretchMode
    {
        PixelPerfect,
        LetterBox,
        Stretch,
        Fit
    }

    // PASS 0 -> RENDER TO RENDERTARGET
    // PASS 1 -> RENDER RENDERTARGET TEXTURE TO BACKBUFFER

    public unsafe class Canvas
    {


        public int Width { get; }

        public int Height { get; }

        private int vertex_max_count;

        private Texture2D current_texture;

        private Vertex2D[] vertex_array;

        private Vertex2D[] render_surface_vertex_array;

        private ushort[] index_array;

        private IndexBuffer index_buffer;

        private int vertex_index;

        private int quad_count;

        private IntRect render_area;

        private bool ready_to_draw;

        private BlendMode current_blend_mode;

        private RenderState render_state;

        private readonly RenderTarget renderer_surface;

        private RenderTarget current_target;

        private CanvasFullscreenStretchMode stretch_mode = CanvasFullscreenStretchMode.PixelPerfect;

        private ShaderProgram default_shader;

        private ShaderProgram current_shader;

        private readonly GraphicsContext gfx;

        internal Canvas(GraphicsContext graphics_context, int width, int height, int max_vertex_count)
        {
            gfx = graphics_context;

            vertex_max_count = max_vertex_count;

            Width = width;

            Height = height;

            renderer_surface = Content.CreateRenderTarget(width, height);

            default_shader = Content.GetBuiltinShader("base_2d");

            default_shader.AddTextureUniform("texture_2d");

            current_shader = default_shader;

            OnScreenResized(width, height);

            SetBlendMode(BlendMode.AlphaBlend);

            _InitRenderBuffers();

        }

        public void SetBlendMode(BlendMode blend_mode)
        {
            if (current_blend_mode == blend_mode)
            {
                return;
            }

            current_blend_mode = blend_mode;

            switch (blend_mode)
            {
                case BlendMode.AlphaBlend:

                    render_state = RenderState.WriteRGB |
                                   RenderState.BlendFunction(RenderState.BlendSourceAlpha,
                                                             RenderState.BlendInverseSourceAlpha);
                    break;

                case BlendMode.AlphaAdd:

                    render_state = RenderState.WriteRGB |
                                   RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);
                    break;

                case BlendMode.ColorMul:

                    render_state = RenderState.WriteRGB | RenderState.BlendDarken;
                    break;
                case BlendMode.None:

                    render_state = RenderState.WriteRGB;
                    break;
            }
        }

        public void Clear(ref Color color)
        {
            gfx.Clear(0, color);
        }

        public void Begin(RenderTarget target = null)
        {
            ready_to_draw = true;

            current_target = target ?? renderer_surface;

            gfx.SetRenderTarget(0, current_target);
        }

        public void End()
        {
            RenderBatch();

            _DrawScreenSurface();

            ready_to_draw = false;
        }

        public void RenderQuad(ref Quad quad)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != quad.Tex ||
                current_blend_mode != quad.Blend)
            {
                RenderBatch();

                if (current_blend_mode != quad.Blend)
                {
                    SetBlendMode(quad.Blend);
                }

                current_texture = quad.Tex;
            }

            int vidx = vertex_index;
            Quad qv = quad;

            ref Vertex2D v0 = ref qv.V0;
            ref Vertex2D v1 = ref qv.V1;
            ref Vertex2D v2 = ref qv.V2;
            ref Vertex2D v3 = ref qv.V3;

            fixed (Vertex2D* vertex_ptr = vertex_array)
            {
                *(vertex_ptr + vidx++) = new Vertex2D(v0.X, v0.Y, v0.Tx, v0.Ty, v0.Col);
                *(vertex_ptr + vidx++) = new Vertex2D(v1.X, v1.Y, v1.Tx, v1.Ty, v1.Col);
                *(vertex_ptr + vidx++) = new Vertex2D(v2.X, v2.Y, v2.Tx, v2.Ty, v2.Col);
                *(vertex_ptr + vidx) = new Vertex2D(v3.X, v3.Y, v3.Tx, v3.Ty, v3.Col);
            }

            unchecked
            {
                vertex_index += 4;
                quad_count++;
            }
        }

        private void RenderBatch()
        {
            if (vertex_index == 0)
            {
                return;
            }

            TransientVertexBuffer vertex_buffer = gfx.StreamVertices2D(vertex_array, vertex_index);
            gfx.Submit(0, vertex_index, current_shader, index_buffer, vertex_buffer, current_texture, render_state);
            
            vertex_index = 0;
            quad_count = 0;
        }

        public void SaveScreenShot(string path)
        {
            gfx.TakeScreenShot(path);
        }

        internal void OnScreenResized(int width, int height)
        {
            Console.WriteLine($"CANVAS : ON SCREEN RESIZED: {width}, {height}");

            gfx.ResizeBackBuffer(width, height);

            int canvas_w = width;
            int canvas_h = height;

            switch (stretch_mode)
            {
                case CanvasFullscreenStretchMode.PixelPerfect:

                    if (width > canvas_w || height > canvas_h)
                    {
                        float ar_origin = (float)canvas_w / canvas_h;
                        float ar_new = (float)width / height;

                        int scale_w = Calc.FloorToInt((float)width / canvas_w);
                        int scale_h = Calc.FloorToInt((float)height / canvas_h);

                        if (ar_new > ar_origin)
                        {
                            scale_w = scale_h;
                        }
                        else
                        {
                            scale_h = scale_w;
                        }

                        int margin_x = (width - canvas_w * scale_w) / 2;
                        int margin_y = (height - canvas_h * scale_h) / 2;

                        render_area = IntRect.FromBox(margin_x, margin_y, canvas_w * scale_w, canvas_h * scale_h);
                    }
                    else
                    {
                        render_area = IntRect.FromBox(0, 0, canvas_w, canvas_h);
                    }

                    break;
                case CanvasFullscreenStretchMode.LetterBox:

                    if (width > canvas_w || height > canvas_h)
                    {
                        float ar_origin = (float)canvas_w / canvas_h;
                        float ar_new = (float)width / height;

                        float scale_w = ((float)width / canvas_w);
                        float scale_h = ((float)height / canvas_h);

                        if (ar_new > ar_origin)
                        {
                            scale_w = scale_h;
                        }
                        else
                        {
                            scale_h = scale_w;
                        }

                        var margin_x = (int)((width - canvas_w * scale_w) / 2);
                        var margin_y = (int)((height - canvas_h * scale_h) / 2);

                        render_area = IntRect.FromBox(margin_x, margin_y, (int)(canvas_w * scale_w), (int)(canvas_h * scale_h));

                    }
                    else
                    {
                        render_area = IntRect.FromBox(0, 0, canvas_w, canvas_h);
                    }

                    break;
                case CanvasFullscreenStretchMode.Stretch:

                    render_area = IntRect.FromBox(0, 0, width, height);

                    break;
                case CanvasFullscreenStretchMode.Fit:

                    break;
            }

            Console.WriteLine($"Render Area: {render_area.X1}, {render_area.Y1}, {render_area.Width}, {render_area.Height}");

            gfx.SetViewport(0, 0, 0, render_area.Width, render_area.Height);
            gfx.SetViewport(1, 0, 0, width, height);

            Matrix4x4 projection_matrix = Matrix4x4.CreateOrthographicOffCenter(
                left: 0,
                right: render_area.Width,
                bottom: render_area.Height,
                top: 0,
                zNearPlane: 0.0f,
                zFarPlane: 1000.0f
            );

            Matrix4x4 projection_matrix2 = Matrix4x4.CreateOrthographicOffCenter(
                left: 0,
                right: width,
                bottom: height,
                top: 0,
                zNearPlane: 0.0f,
                zFarPlane: 1.0f
            );

            gfx.SetTransform(0, &projection_matrix.M11);
            gfx.SetTransform(1, &projection_matrix2.M11);

        }

        private void _InitRenderBuffers()
        {
            vertex_array = new Vertex2D[vertex_max_count];

            render_surface_vertex_array = new Vertex2D[4];

            index_array = new ushort[(vertex_max_count / 4) * 6];

            ushort indice_i = 0;

            for (int i = 0; i < index_array.Length; i += 6, indice_i += 4)
            {
                index_array[i + 0] = (ushort)(indice_i + 0);
                index_array[i + 1] = (ushort)(indice_i + 1);
                index_array[i + 2] = (ushort)(indice_i + 2);
                index_array[i + 3] = (ushort)(indice_i + 0);
                index_array[i + 4] = (ushort)(indice_i + 2);
                index_array[i + 5] = (ushort)(indice_i + 3);
            }

            index_buffer = gfx.CreateIndexBuffer(index_array);
        }

        private void _DrawScreenSurface()
        {
            int rx = render_area.X1;
            int ry = render_area.Y1;
            int rx2 = render_area.X2;
            int ry2 = render_area.Y2;

            fixed (Vertex2D* vertex_ptr = render_surface_vertex_array)
            {
                *(vertex_ptr + 0) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
                *(vertex_ptr + 1) = new Vertex2D(rx2, ry, 1, 0, 0xFFFFFFFF);
                *(vertex_ptr + 2) = new Vertex2D(rx2, ry2, 1, 1, 0xFFFFFFFF);
                *(vertex_ptr + 3) = new Vertex2D(rx, ry2, 0, 1, 0xFFFFFFFF);
            }

            TransientVertexBuffer vertex_buffer = gfx.StreamVertices2D(render_surface_vertex_array, 4);

            gfx.Submit(view: 1, vertex_count: 4, current_shader, index_buffer, vertex_buffer, renderer_surface.Texture, RenderState.None | RenderState.WriteRGB);
          
        }
    }
}