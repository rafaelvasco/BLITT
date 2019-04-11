using System;
using System.Numerics;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Foundation;
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

    public unsafe class Canvas
    {
        public Font DefaultFont => default_font;

        public ShaderProgram ScreenShader
        {
            get => screen_shader;
            set => screen_shader = value;
        }

        public Font DefaultFont2 => default_font2;
        
        private readonly GraphicsContext gfx;

        private readonly RenderTarget renderer_surface;

        private BlendMode current_blend_mode;

        private ShaderProgram current_shader;

        private ShaderProgram screen_shader;
        
        private readonly ShaderProgram default_shader;

        private Texture2D current_texture;

        private readonly Font default_font;

        private readonly Font default_font2;

        private ushort[] index_array;

        private IndexBuffer index_buffer;

        private readonly Texture2D prim_texture;

        private readonly BlendMode primitives_blend_mode;

        private uint tint_color = 0xFFFFFFFF;

        private bool ready_to_draw;

        private IntRect render_area;

        private RenderState render_state;

        private Vertex2D[] render_surface_vertex_array;

        private readonly CanvasFullscreenStretchMode stretch_mode = CanvasFullscreenStretchMode.LetterBox;

        private Vertex2D[] vertex_array;

        private int vertex_index;

        private readonly int vertex_max_count;

        private Matrix4x4 render_surface_proj_matrix;

        private Matrix4x4 screen_proj_matrix;

        private byte current_render_pass;


        internal Canvas(GraphicsContext graphics_context, int width, int height, int max_vertex_count)
        {
            gfx = graphics_context;

            vertex_max_count = max_vertex_count;

            Width = width;

            Height = height;

            renderer_surface = Game.Instance.ContentManager.CreateRenderTarget(width, height);

            default_shader = Game.Instance.ContentManager.Get<ShaderProgram>("base_2d");

            default_font = Game.Instance.ContentManager.Get<Font>("default_font");
            default_font2 = Game.Instance.ContentManager.Get<Font>("default_font2");            

            current_shader = default_shader;

            OnScreenResized(width, height);

            SetBlendMode(BlendMode.AlphaBlend);

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

        public void SetShader(ShaderProgram shader)
        {
            _RenderBatch();
            
            current_shader = shader ?? default_shader;
        }
        
        public void Clear(Color color)
        {
            gfx.SetClearColor(current_render_pass, color.RGBA);
        }

        public void Begin(RenderTarget target = null)
        {
            ready_to_draw = true;

            var current_target = target ?? renderer_surface;
            
            Bgfx.SetViewMode(current_render_pass, ViewMode.Sequential);

            gfx.SetRenderTarget(current_render_pass, current_target);

            gfx.Touch(current_render_pass);

            Matrix4x4 proj;

            if (target == null)
            {
                gfx.SetViewport(current_render_pass, 0, 0, render_area.Width, render_area.Height);

                proj = render_surface_proj_matrix;
            }
            else
            {
                gfx.SetViewport(current_render_pass, 0, 0, target.Width, target.Height);

                proj = Matrix4x4.CreateOrthographicOffCenter(
                    0,
                    target.Width,
                    target.Height,
                    0,
                    0.0f,
                    1000.0f
                );
            }
            
            gfx.SetProjection(current_render_pass, &proj.M11);

        }

        public void End()
        {
            _RenderBatch();
            
            SetShader(null);

            tint_color = 0xFFFFFFFF;

            current_render_pass++;

            ready_to_draw = false;
        }

        internal void EndRender()
        {
            _DrawScreenQuad();
            
            current_render_pass = 0;
        }

        public void SetColor(Color color)
        {
            //TODO: Cleanup SETCOLOR
            tint_color = color.ABGR;
        }

        public void DrawQuad(Texture2D texture, ref Quad quad)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != texture ||
                current_blend_mode != quad.Blend)
            {
                _RenderBatch();

                if (current_blend_mode != quad.Blend) SetBlendMode(quad.Blend);

                current_texture = texture;
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
            }
        }

        public void DrawRect(float x, float y, float w, float h)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                _RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);

                current_texture = prim_texture;
            }

            var vidx = vertex_index;

            var col = tint_color;

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
            }
        }

        public void FillRect(float x, float y, float w, float h)
        {
            if (!ready_to_draw) return;

            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                _RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);

                current_texture = prim_texture;
            }

            var vidx = vertex_index;

            fixed (Vertex2D* vertex_ptr = vertex_array)
            {
                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, tint_color);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, tint_color);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, tint_color);
                *(vertex_ptr + vidx) = new Vertex2D(x, y + h, 0, 0, tint_color);
            }

            unchecked
            {
                vertex_index += 4;
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
                    letters[ch_idx].SetColor(tint_color);
                    letters[ch_idx].DrawEx(this, dx, y, 0.0f, scale , scale);
                    dx += (letters[ch_idx].Width + post_spacings[ch_idx] ) * scale ;
                }
            }
        }

        public void SaveScreenShot(string path)
        {
            gfx.TakeScreenShot(path);
        }

        internal void OnScreenResized(int width, int height)
        {
            Console.WriteLine($"CANVAS : ON SCREEN RESIZED: {width.ToString()}, {height.ToString()}");

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
                $"Render Area: {render_area.X1.ToString()}, {render_area.Y1.ToString()}, {render_area.Width.ToString()}, {render_area.Height.ToString()}");

            render_surface_proj_matrix = Matrix4x4.CreateOrthographicOffCenter(
                0,
                render_area.Width,
                render_area.Height,
                0,
                0.0f,
                1000.0f
            );

            screen_proj_matrix = Matrix4x4.CreateOrthographicOffCenter(
                0,
                width,
                height,
                0,
                0.0f,
                1.0f
            );

           
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

        private void _RenderBatch()
        {
            if (vertex_index == 0) return;

            var vertex_buffer = gfx.StreamVertices2D(vertex_array, vertex_index);
            
            gfx.Submit(current_render_pass, vertex_index, current_shader, index_buffer, vertex_buffer, current_texture, render_state);

            vertex_index = 0;
        }

        private void _DrawScreenQuad()
        {

            if (screen_shader != null)
            {
                SetShader(screen_shader);
            }
            
            gfx.SetClearColor(current_render_pass, 0x000000FF);
            
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

            gfx.SetViewport(current_render_pass, 0, 0, Game.Instance.ScreenSize.W, Game.Instance.ScreenSize.H);

            var projection = screen_proj_matrix;

            gfx.SetProjection(current_render_pass, &projection.M11);
            
            gfx.Submit(current_render_pass, 4, current_shader, index_buffer, vertex_buffer, renderer_surface,
               RenderState.WriteRGB);
        }
    }
}