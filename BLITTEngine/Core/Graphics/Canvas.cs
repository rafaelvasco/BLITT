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
        
        public int MaxDrawCalls { get; private set; }

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

            var prim_pixmap = new Pixmap(10, 10);
            
            prim_pixmap.Fill(Color.White);

            primitives_blend_mode = BlendMode.AlphaBlend;

            prim_texture = Game.Instance.ContentManager.CreateTexture(prim_pixmap, false, false);

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

                    render_state = RenderState.WriteRGB | RenderState.WriteA |
                                   RenderState.BlendFunction(RenderState.BlendSourceAlpha,
                                       RenderState.BlendInverseSourceAlpha);
                    break;

                case BlendMode.AlphaAdd:

                    render_state = RenderState.WriteRGB | RenderState.WriteA |
                                   RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);
                    break;

                case BlendMode.ColorMul:

                    render_state = RenderState.WriteRGB | RenderState.WriteA | 
                                   RenderState.BlendMultiply;
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

        internal void BeginRendering()
        {
            draw_calls = 0;
            
            current_render_pass = 0;
            
            SetRenderTarget();
        }

        public void SetRenderTarget(RenderTarget target=null)
        {
            _RenderBatch();
            
            if (target != null)
            {
                current_render_pass++;

                if (current_render_pass > max_render_pass)
                {
                    max_render_pass = current_render_pass;
                }
            }
            else
            {
                target = renderer_surface;
                current_render_pass = 0;
            }
            
            gfx.SetRenderTarget(current_render_pass, target);
            gfx.Touch(current_render_pass);
            Bgfx.SetViewMode(current_render_pass, ViewMode.Sequential);
            gfx.SetClearColor(current_render_pass, 0x000000FF);
            
            gfx.SetViewport(current_render_pass, 0, 0, target.Width, target.Height);

            Matrix4x4 projection;
            
            if (target != renderer_surface)
            {
                projection = Matrix4x4.CreateOrthographicOffCenter(
                    0,
                    target.Width,
                    target.Height,
                    0,
                    0.0f,
                    1000.0f
                );
                
            }
            else
            {
                projection = render_surface_proj_matrix;
            }
            
            gfx.SetProjection(current_render_pass, &projection.M11);
        }
        
        

        internal void EndRendering()
        {
            _RenderBatch();
            
            ResetRenderState();
            
            _DrawScreenQuad();
        }

        public void BeginClip(int x, int y, int w, int h)
        {
            gfx.SetScissor(current_render_pass, x, y, w, h);
        }

        public void EndClip()
        {
            gfx.SetScissor(current_render_pass, 0, 0, 0, 0);
        }
        
        public void DrawQuad(Texture2D texture, ref Quad quad)
        {
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

        public void DrawRect(float x, float y, float w, float h, Color color)
        {
            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                _RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);

                current_texture = prim_texture;
            }

            var vidx = vertex_index;

            var col = color.ABGR;

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
        
        public void FillRect(float x, float y, float w, float h, Color color)
        {
            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture ||
                current_blend_mode != primitives_blend_mode)
            {
                _RenderBatch();

                if (current_blend_mode != primitives_blend_mode) SetBlendMode(primitives_blend_mode);
                
                current_texture = prim_texture;
            }

            
            var vidx = vertex_index;

            var col = color.ABGR;
            
            fixed (Vertex2D* vertex_ptr = vertex_array)
            {
                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, col);
                *(vertex_ptr + vidx) = new Vertex2D(x, y + h, 0, 0, col);
            }

            unchecked
            {
                vertex_index += 4;
            }
        }

        public void DrawText(float x, float y, string text, Color color, float scale = 1.0f)
        {
            DrawText(default_font, x, y, text, color, scale);
        }
        
        public void DrawText(Font font, float x, float y, string text, Color color, float scale = 1.0f)
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
                    letters[ch_idx].SetColor(color);
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

            draw_calls++;

            if (draw_calls > MaxDrawCalls)
            {
                MaxDrawCalls = draw_calls;
            }

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
            
            //gfx.SetClearColor(current_render_pass, 0x000000FF);
            
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

            gfx.SetViewport((byte) (max_render_pass+1), 0, 0, Game.Instance.ScreenSize.W, Game.Instance.ScreenSize.H);

            var projection = screen_proj_matrix;

            gfx.SetProjection((byte) (max_render_pass+1), &projection.M11);
            
            gfx.Submit((byte) (max_render_pass+1), 4, current_shader, index_buffer, vertex_buffer, renderer_surface,
               RenderState.WriteRGB | RenderState.WriteA);
        }

        private void ResetRenderState()
        {
            render_state = RenderState.WriteRGB | RenderState.WriteA |
                           RenderState.BlendFunction(RenderState.BlendSourceAlpha,
                               RenderState.BlendInverseSourceAlpha);
            
            
            SetShader(null);
        }
        
        
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

        private byte max_render_pass;

        private int draw_calls;

    }
}