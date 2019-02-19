using System;
using System.Numerics;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Resources;

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
        private readonly GraphicsContext gfx;

        private readonly RenderTarget renderer_surface;

        private BlendMode current_blend_mode;

        private readonly ShaderProgram current_shader;

        private RenderTarget current_target;

        private Texture2D current_texture;

        private readonly Font default_font;

        private readonly ShaderProgram default_shader;

        private ushort[] index_array;

        private IndexBuffer index_buffer;

        private readonly Texture2D prim_texture;

        private readonly BlendMode primitives_blend_mode;

        private uint primitives_color = 0xFFFFFFFF;

        private int quad_count;

        private bool ready_to_draw;

        private IntRect render_area;

        private RenderState render_state;

        private Vertex2D[] render_surface_vertex_array;

        private readonly CanvasFullscreenStretchMode stretch_mode = CanvasFullscreenStretchMode.PixelPerfect;

        private Vertex2D[] vertex_array;

        private int vertex_index;

        private readonly int vertex_max_count;

        internal Canvas(GraphicsContext graphics_context, int width, int height, int max_vertex_count)
        {
            gfx = graphics_context;

            vertex_max_count = max_vertex_count;

            Width = width;

            Height = height;

            renderer_surface = Game.Instance.ContentManager.CreateRenderTarget(width, height);

            string shader_to_load;

            switch (graphics_context.Info.RendererBackend)
            {
                case RendererBackend.Direct3D9:
                case RendererBackend.Direct3D11:
                case RendererBackend.Direct3D12:
                    shader_to_load = "dx_base_2d";
                    break;
                case RendererBackend.OpenGL:
                    shader_to_load = "gx_base_2d";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Can't load shader for this renderer backend: " + graphics_context.Info.RendererBackend);
            }

            default_shader = Game.Instance.ContentManager.Get<ShaderProgram>(shader_to_load);

            default_font = Game.Instance.ContentManager.Get<Font>("default_font2");

            default_shader.AddTextureUniform("texture_2d");

            current_shader = default_shader;

            OnScreenResized(width, height);

            SetBlendMode(BlendMode.AlphaBlend);

            gfx.SetClearColor(0, 0x000000FF);

            gfx.SetClearColor(1, 0x000000FF);

            var prim_pixmap = new Pixmap(2, 2);
            prim_pixmap.Fill(Color.White);

            primitives_blend_mode = BlendMode.AlphaBlend;

            prim_texture = Game.Instance.ContentManager.CreateTexture(prim_pixmap);

            prim_pixmap.Dispose();

            _InitRenderBuffers();
        }

        public int Width { get; }

        public int Height { get; }

        public void SetBlendMode(BlendMode blend_mode)
        {
            if (current_blend_mode == blend_mode) return;

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

        public void Clear(Color color)
        {
            gfx.SetClearColor(0, color.GetIntRgba());
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

        /// <summary>
        ///     Set Primitives Draw Color
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            primitives_color = color;
        }


        public void DrawQuad(ref Quad quad)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != quad.Texture ||
                current_blend_mode != quad.Blend)
            {
                RenderBatch();

                if (current_blend_mode != quad.Blend) SetBlendMode(quad.Blend);

                current_texture = quad.Texture;
            }

            var vidx = vertex_index;
            var qv = quad;

            ref var v0 = ref qv.V0;
            ref var v1 = ref qv.V1;
            ref var v2 = ref qv.V2;
            ref var v3 = ref qv.V3;

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

        public void DrawRect(float x, float y, float w, float h)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);

                current_texture = prim_texture;
            }

            var vidx = vertex_index;

            var col = primitives_color;

            fixed (Vertex2D* vertex_ptr = vertex_array)
            {
                // Top Edge

                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + 1, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + 1, 0, 0, col);

                // Bottom Edge
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h + 1, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h + 1, 0, 0, col);

                // Left Edge
                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + 1, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + 1, y + h, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h, 0, 0, col);

                // Right Edge
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w + 1, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w + 1, y + h + 1, 0, 0, col);
                *(vertex_ptr + vidx) = new Vertex2D(x + w, y + h + 1, 0, 0, col);
            }

            unchecked
            {
                vertex_index += 16;
                quad_count += 4;
            }
        }

        public void FillRect(float x, float y, float w, float h)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);

                current_texture = prim_texture;
            }

            var vidx = vertex_index;

            fixed (Vertex2D* vertex_ptr = vertex_array)
            {
                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, primitives_color);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, primitives_color);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, primitives_color);
                *(vertex_ptr + vidx) = new Vertex2D(x, y + h, 0, 0, primitives_color);
            }

            unchecked
            {
                vertex_index += 4;
                quad_count++;
            }
        }


        public void DrawString(float x, float y, string text, float scale = 1.0f)
        {
            DrawString(default_font, x, y, text, scale);
        }
        
        public void DrawString(Font font, float x, float y, string text, float scale = 1.0f)
        {
            var str_len = text.Length;
            
            if (str_len == 0)
            {
                return;
            }
            
            var dx = x;
            
            var letters = font.letters;
            var pre_spacings = font.pre_spacings;
            var post_spacings = font.post_spacings;
            
            for (var i = 0; i < str_len; ++i)
            {
                int ch_idx = text[i];

                if (letters[ch_idx] == null) ch_idx = '?';

                if (letters[ch_idx] != null)
                {
                    dx += pre_spacings[ch_idx] * scale;
                    letters[ch_idx].DrawEx(this, dx, y, 0.0f, scale , scale);
                    dx += (letters[ch_idx].Width + post_spacings[ch_idx] ) * scale ;
                }
            }
        }
        
        private void RenderBatch()
        {
            if (vertex_index == 0) return;

            var vertex_buffer = gfx.StreamVertices2D(vertex_array, vertex_index);
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

            var canvas_w = Width;
            var canvas_h = Height;

            switch (stretch_mode)
            {
                case CanvasFullscreenStretchMode.PixelPerfect:

                    if (width > canvas_w || height > canvas_h)
                    {
                        var ar_origin = (float) canvas_w / canvas_h;
                        var ar_new = (float) width / height;

                        var scale_w = Calc.FloorToInt((float) width / canvas_w);
                        var scale_h = Calc.FloorToInt((float) height / canvas_h);

                        if (ar_new > ar_origin)
                            scale_w = scale_h;
                        else
                            scale_h = scale_w;

                        var margin_x = (width - canvas_w * scale_w) / 2;
                        var margin_y = (height - canvas_h * scale_h) / 2;

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
                        var ar_origin = (float) canvas_w / canvas_h;
                        var ar_new = (float) width / height;

                        var scale_w = (float) width / canvas_w;
                        var scale_h = (float) height / canvas_h;

                        if (ar_new > ar_origin)
                            scale_w = scale_h;
                        else
                            scale_h = scale_w;

                        var margin_x = (int) ((width - canvas_w * scale_w) / 2);
                        var margin_y = (int) ((height - canvas_h * scale_h) / 2);

                        render_area = IntRect.FromBox(margin_x, margin_y, (int) (canvas_w * scale_w),
                            (int) (canvas_h * scale_h));
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

            Console.WriteLine(
                $"Render Area: {render_area.X1}, {render_area.Y1}, {render_area.Width}, {render_area.Height}");

            gfx.SetViewport(0, 0, 0, render_area.Width, render_area.Height);
            gfx.SetViewport(1, 0, 0, width, height);

            var projection_matrix = Matrix4x4.CreateOrthographicOffCenter(
                0,
                render_area.Width,
                render_area.Height,
                0,
                0.0f,
                1000.0f
            );

            var projection_matrix2 = Matrix4x4.CreateOrthographicOffCenter(
                0,
                width,
                height,
                0,
                0.0f,
                1.0f
            );

            gfx.SetTransform(0, &projection_matrix.M11);
            gfx.SetTransform(1, &projection_matrix2.M11);
        }

        private void _InitRenderBuffers()
        {
            vertex_array = new Vertex2D[vertex_max_count];

            render_surface_vertex_array = new Vertex2D[4];

            index_array = new ushort[vertex_max_count / 4 * 6];

            ushort indice_i = 0;

            for (var i = 0; i < index_array.Length; i += 6, indice_i += 4)
            {
                index_array[i + 0] = (ushort) (indice_i + 0);
                index_array[i + 1] = (ushort) (indice_i + 1);
                index_array[i + 2] = (ushort) (indice_i + 2);
                index_array[i + 3] = (ushort) (indice_i + 0);
                index_array[i + 4] = (ushort) (indice_i + 2);
                index_array[i + 5] = (ushort) (indice_i + 3);
            }

            index_buffer = gfx.CreateIndexBuffer(index_array);
        }

        private void _DrawScreenSurface()
        {
            var rx = render_area.X1;
            var ry = render_area.Y1;
            var rx2 = render_area.X2;
            var ry2 = render_area.Y2;

            fixed (Vertex2D* vertex_ptr = render_surface_vertex_array)
            {
                *(vertex_ptr + 0) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
                *(vertex_ptr + 1) = new Vertex2D(rx2, ry, 1, 0, 0xFFFFFFFF);
                *(vertex_ptr + 2) = new Vertex2D(rx2, ry2, 1, 1, 0xFFFFFFFF);
                *(vertex_ptr + 3) = new Vertex2D(rx, ry2, 0, 1, 0xFFFFFFFF);
            }

            var vertex_buffer = gfx.StreamVertices2D(render_surface_vertex_array, 4);

            gfx.Submit(1, 4, current_shader, index_buffer, vertex_buffer, renderer_surface.Texture,
                RenderState.None | RenderState.WriteRGB);
        }
    }
}