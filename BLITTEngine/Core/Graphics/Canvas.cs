using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

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

        public int Width => canvas_width;

        public int Height => canvas_height;

        private int vertex_max_count;

        private BlendMode current_blend_mode;

        private Vertex2D[] vertex_array;

        private ushort[] index_array;

        private IndexBuffer index_buffer;

        private int vertex_index;

        private int quad_count;

        private int canvas_width;

        private int canvas_height;

        private IntRect render_area;

        private bool ready_to_draw = false;

        private RenderState render_state;

        private RenderTarget renderer_surface;

        private RenderTarget current_target;

        private List<RenderTarget> render_targets;

        private CanvasFullscreenStretchMode stretch_mode = CanvasFullscreenStretchMode.PixelPerfect;

        private GraphicsContext gfx;

        internal Canvas(GraphicsContext graphics_context, int width, int height, int max_vertex_count)
        {
            gfx = graphics_context;

            render_targets = new List<RenderTarget>();

            vertex_max_count = max_vertex_count;


            canvas_width = width;

            canvas_height = height;

            renderer_surface = CreateTarget(canvas_width, canvas_height);

            OnScreenResized(width, height);

            Content.LoadEmbededShaders(graphics_context.Info.RendererBackend);
            base_shader = Content.GetBuiltinShader("base_2d");
            base_shader.AddTextureUniform("texture_2d");

            _InitRenderBuffers();

            _SetBlendMode(BlendMode.AlphaBlend);

            Bgfx.Touch(0);
            Bgfx.Frame();

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

        }

        internal void Free()
        {
            Console.WriteLine(" > Disposing Graphics Device");

            index_buffer.Dispose();

            foreach (var target in render_targets)
            {
                target.Handle.Dispose();
            }

            render_targets.Clear();

            Bgfx.Shutdown();
        }

        public RenderTarget CreateTarget(int width, int height)
        {

            var render_target = new RenderTarget(width, height);

            render_targets.Add(render_target);

            return render_target;
        }

        public void FreeTarget(RenderTarget target)
        {
            target.Handle.Dispose();

            render_targets.Remove(target);
        }

        public void Clear(int color)
        {
            Bgfx.SetViewClear(0, ClearTargets.Color, color);
        }

        public void Begin(RenderTarget target = null)
        {
            ready_to_draw = true;

            current_target = target ?? renderer_surface;

            Bgfx.SetViewFrameBuffer(0, current_target.Handle);
        }

        public void End()
        {
            RenderBatch();

            _DrawScreenSurface();

            ready_to_draw = false;


        }

        public void RenderQuad(ref Quad quad)
        {
            if (ready_to_draw)
            {
                if (vertex_index >= vertex_max_count ||
                    current_texture != quad.Tex ||
                    current_blend_mode != quad.Blend)
                {
                    RenderBatch();

                    if (current_blend_mode != quad.Blend)
                    {
                        _SetBlendMode(quad.Blend);
                    }

                    if (quad.Tex != current_texture)
                    {
                        current_texture = quad.Tex;
                    }
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
                    *(vertex_ptr + vidx++) = new Vertex2D(v3.X, v3.Y, v3.Tx, v3.Ty, v3.Col);
                }

                unchecked
                {
                    vertex_index += 4;
                    quad_count++;
                }
            }
        }

        public void RenderBatch()
        {
            if (vertex_index == 0)
            {
                return;
            }

            var vertex_buffer = new TransientVertexBuffer(vertex_index, Vertex2D.Layout);

            fixed (void* v = vertex_array)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)vertex_index * 20);
            }

            base_shader.SetTexture(current_texture, "texture_2d");

            Bgfx.SetRenderState(render_state);

            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count * 6);

            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, vertex_index);

            Bgfx.Submit(0, base_shader.Program);

            vertex_index = 0;
            quad_count = 0;
        }

        public void SaveScreenShot(string path)
        {
            Bgfx.RequestScreenShot(path);
        }

        internal void Flip()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }

        internal void OnScreenResized(int width, int height)
        {
            Console.WriteLine($"CANVAS : ON SCREEN RESIZED: {width}, {height}");

            Bgfx.Reset(width, height, ResetFlags.Vsync);

            var canvas_w = canvas_width;
            var canvas_h = canvas_height;

            switch (stretch_mode)
            {
                case CanvasFullscreenStretchMode.PixelPerfect:

                    if (width > canvas_w || height > canvas_h)
                    {
                        var ar_origin = (float)canvas_w / canvas_h;
                        var ar_new = (float)width / height;

                        var scale_w = Calc.FloorToInt((float)width / canvas_w);
                        var scale_h = Calc.FloorToInt((float)height / canvas_h);

                        if (ar_new > ar_origin)
                        {
                            scale_w = scale_h;
                        }
                        else
                        {
                            scale_h = scale_w;
                        }

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
                        var ar_origin = (float)canvas_w / canvas_h;
                        var ar_new = (float)width / height;

                        var scale_w = ((float)width / canvas_w);
                        var scale_h = ((float)height / canvas_h);

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

            Bgfx.SetViewRect(0, 0, 0, render_area.Width, render_area.Height);
            Bgfx.SetViewRect(1, 0, 0, width, height);

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

            Bgfx.SetViewTransform(0, null, &projection_matrix.M11);
            Bgfx.SetViewTransform(1, null, &projection_matrix2.M11);

        }

        internal ShaderProgram CreateShaderProgram(string name, byte[] vertex_shader_src, byte[] frag_shader_src)
        {
            if (vertex_shader_src.Length == 0 || frag_shader_src.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = new Shader(MemoryBlock.FromArray(vertex_shader_src));
            var frag_shader = new Shader(MemoryBlock.FromArray(frag_shader_src));

            return new ShaderProgram(vertex_shader, frag_shader);
        }

        private void _SetBlendMode(BlendMode mode)
        {
            current_blend_mode = mode;

            switch (mode)
            {
                case BlendMode.AlphaBlend:

                    render_state = RenderState.WriteRGB | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInverseSourceAlpha);

                    break;

                case BlendMode.AlphaAdd:

                    render_state = RenderState.WriteRGB | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);

                    break;

                case BlendMode.ColorMul:

                    render_state = RenderState.BlendDarken;

                    break;
            }
        }

        private void _InitRenderBuffers()
        {
            vertex_array = new Vertex2D[vertex_max_count];

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

            index_buffer = new IndexBuffer(MemoryBlock.FromArray(index_array));
        }

        private void _DrawScreenSurface()
        {
            var rx = render_area.X1;
            var ry = render_area.Y1;
            var rx2 = render_area.X2;
            var ry2 = render_area.Y2;

            var vertex_buffer = new TransientVertexBuffer(4, Vertex2D.Layout);

            Vertex2D* v = (Vertex2D*) vertex_buffer.Data;

            *(v + 0) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
            *(v + 1) = new Vertex2D(rx2, ry, 1, 0, 0xFFFFFFFF);
            *(v + 2) = new Vertex2D(rx2, ry2, 1, 1, 0xFFFFFFFF);
            *(v + 3) = new Vertex2D(rx, ry2, 0, 1, 0xFFFFFFFF);

            base_shader.SetTexture(renderer_surface.Texture, "texture_2d");

            Bgfx.SetRenderState(canvas_target_state);

            Bgfx.SetIndexBuffer(index_buffer, 0, 6);

            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, 4);

            Bgfx.Submit(1, base_shader.Program);
        }
    }
}