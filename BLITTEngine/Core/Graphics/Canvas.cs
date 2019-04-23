using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    public enum CanvasStretchMode : byte
    {
        PixelPerfect = 0,
        LetterBox,
        Stretch,
        Resize
    }

    public unsafe class Canvas
    {
        public Font DefaultFont => default_font;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public CanvasStretchMode StretchMode
        {
            get => stretch_mode;
            set => stretch_mode = value;
        }

        public Point2 RenderAreaTopLeft => render_area.TopLeft;
        
        public int RenderWidth => render_area.Width;
        public int RenderHeight => render_area.Height;

        public float RenderScaleX => render_scale_x;

        public float RenderScaleY => render_scale_y;

        public Font DefaultFont2 => default_font2;
        
        public int MaxDrawCalls { get; private set; }

        internal Canvas(GraphicsContext graphics_context, int width, int height, int max_vertex_count)
        {
            gfx = graphics_context;

            vertex_max_count = max_vertex_count;
            
            Width = width;

            Height = height;

            render_area = Rect.FromBox(0, 0, width, height);
            
            main_surface = new CanvasSurface(render_area);

            default_shader = Game.Instance.ContentManager.Get<ShaderProgram>("base_2d");

            default_font = Game.Instance.ContentManager.Get<Font>("default_font");
            default_font2 = Game.Instance.ContentManager.Get<Font>("default_font2");            

            current_shader = default_shader;
            
            aditional_surfaces = new CanvasSurface[2];

            OnScreenResized(Game.Instance.ScreenSize.W, Game.Instance.ScreenSize.H);
            
            SetBlendMode(BlendMode.AlphaBlend);

            var prim_pixmap = new Pixmap(10, 10);
            
            prim_pixmap.Fill(Color.White);

            prim_texture = Game.Instance.ContentManager.CreateTexture(prim_pixmap, false, false);

            prim_pixmap.Dispose();

            InitRenderBuffers();
            
        }

        
        private void SetBlendMode(BlendMode blend_mode)
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
            RenderBatch();
            
            current_shader = shader ?? default_shader;
        }

        internal void BeginRendering()
        {
            draw_calls = 0;
            
            current_render_pass = 0;
            
            SetSurface();
        }

        internal void EndRendering()
        {
            RenderBatch();
            
            ResetRenderState();
            
            RenderSurface(main_surface, (byte) (max_render_pass+1));

            if (aditional_surface_idx == 0)
            {
                return;
            }
            
            RenderAditionalSurfaces();
        }

        public void SetSurface(CanvasSurface surface=null)
        {
            RenderBatch();
            
            if (surface != null)
            {
                current_render_pass++;

                if (current_render_pass > max_render_pass)
                {
                    max_render_pass = current_render_pass;
                }
            }
            else
            {
                surface = main_surface;
                current_render_pass = 0;
            }
            
            Matrix4x4 projection = surface.Projection;
            
            gfx.SetRenderTarget(current_render_pass, surface.RenderTarget);
            gfx.Touch(current_render_pass);
            Bgfx.SetViewMode(current_render_pass, ViewMode.Sequential);
            gfx.SetClearColor(current_render_pass, surface != main_surface ? 0x00000000 : 0x000000FF);
            gfx.SetViewport(current_render_pass, 0, 0, surface.Width, surface.Height);
            gfx.SetProjection(current_render_pass, &projection.M11);
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
                current_texture != texture || current_blend_mode != quad.Blend)
            {
                RenderBatch();

                if (current_blend_mode != quad.Blend)
                {
                    SetBlendMode(quad.Blend);
                }

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
                current_texture != prim_texture || current_blend_mode != BlendMode.AlphaBlend)
            {
                RenderBatch();

                if (current_blend_mode != BlendMode.AlphaBlend)
                {
                    SetBlendMode(BlendMode.AlphaBlend);
                }

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
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h-1, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h-1, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h, 0, 0, col);

                // Left Edge
                *(vertex_ptr + vidx++) = new Vertex2D(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + 1, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + 1, y + h, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x, y + h, 0, 0, col);

                // Right Edge
                *(vertex_ptr + vidx++) = new Vertex2D(x + w-1, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new Vertex2D(x + w, y + h, 0, 0, col);
                *(vertex_ptr + vidx) = new Vertex2D(x + w-1, y + h, 0, 0, col);
            }

            unchecked
            {
                vertex_index += 16;
            }
        }
        
        public void FillRect(float x, float y, float w, float h, Color color)
        {
            if (vertex_index >= vertex_max_count ||
                current_texture != prim_texture || current_blend_mode != BlendMode.AlphaBlend)
            {
                RenderBatch();
                
                if (current_blend_mode != BlendMode.AlphaBlend)
                {
                    SetBlendMode(BlendMode.AlphaBlend);
                }

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

        public void AddSurface(CanvasSurface surface)
        {
            if (aditional_surface_idx >= aditional_surfaces.Length)
            {
                Array.Resize(ref aditional_surfaces, aditional_surface_idx+1);
            }
            
            aditional_surfaces[aditional_surface_idx++] = surface;
        }
        
        internal void OnScreenResized(int width, int height)
        {
            gfx.ResizeBackBuffer(width, height);
            
            var canvas_w = this.Width;
            var canvas_h = this.Height;

            switch (stretch_mode)
            {
                case CanvasStretchMode.PixelPerfect:

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
                        
                        render_scale_x = scale_w;
                        render_scale_y = scale_h;

                        render_area = Rect.FromBox(margin_x, margin_y, canvas_w * scale_w, canvas_h * scale_h);
                    }
                    else
                    {
                        render_area = Rect.FromBox(0, 0, canvas_w, canvas_h);
                    }

                    break;
                case CanvasStretchMode.LetterBox:

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
                        
                        render_scale_x = scale_w;
                        render_scale_y = scale_h;

                        render_area = Rect.FromBox(margin_x, margin_y, (int) (canvas_w * scale_w),
                            (int) (canvas_h * scale_h));
                    }
                    else
                    {
                        render_scale_x = 1.0f;
                        render_scale_y = 1.0f;
                        render_area = Rect.FromBox(0, 0, canvas_w, canvas_h);
                    }

                    break;
                case CanvasStretchMode.Stretch:

                    render_scale_x = (float)width / canvas_w;
                    render_scale_y = (float)height / canvas_h;;
                    render_area = Rect.FromBox(0, 0, width, height);

                    break;
                case CanvasStretchMode.Resize:

                    if (width == canvas_w && height == canvas_h)
                    {
                        break;
                    }
                    
                    render_scale_x = 1.0f;
                    render_scale_y = 1.0f;
                    
                    render_area = Rect.FromBox(0, 0, width, height);
                    
                    Console.WriteLine($"Stretch Resize: {width}, {height}");
                    
                    main_surface.ResizeSurface(width, height);
                    
                    for (int i = 0; i < aditional_surface_idx; ++i)
                    {
                        aditional_surfaces[i].ResizeSurface(width, height);
                    }

                    this.Width = width;
                    this.Height = height;

                    break;
            }

            Console.WriteLine(
                $"Render Area: {render_area.X1.ToString()}, {render_area.Y1.ToString()}, {render_area.Width.ToString()}, {render_area.Height.ToString()}");

            screen_proj_matrix = Matrix4x4.CreateOrthographicOffCenter(
                0,
                width,
                height,
                0,
                0.0f,
                1.0f
            );

            main_surface.SetArea(render_area);

            if (aditional_surface_idx == 0)
            {
                return;
            }

            for (int i = 0; i < aditional_surface_idx; ++i)
            {
                aditional_surfaces[i].SetArea(render_area);
            }
           
        }

        private void InitRenderBuffers()
        {
            vertex_array = new Vertex2D[vertex_max_count];

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

        private void RenderBatch()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RenderAditionalSurfaces()
        {
            // Render Main Surface
            
            for (int i = 0; i < aditional_surface_idx; ++i)
            {
                RenderSurface(aditional_surfaces[i], (byte) (max_render_pass+i+1));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RenderSurface(CanvasSurface surface, byte render_pass)
        {
            var vertex_buffer = gfx.StreamVertices2D(surface.Vertices, 4);
                
            gfx.SetViewport(render_pass, 0, 0, Game.Instance.ScreenSize.W, Game.Instance.ScreenSize.H);

            var projection = screen_proj_matrix;

            gfx.SetProjection(render_pass, &projection.M11);
            
            gfx.Submit(render_pass, 4, surface.Shader ?? default_shader, index_buffer, vertex_buffer, surface.RenderTarget,
                RenderState.WriteRGB | RenderState.WriteA | RenderState.BlendAlpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResetRenderState()
        {
            SetShader(null);
        }
        
        
        private readonly GraphicsContext gfx;

        private BlendMode current_blend_mode = BlendMode.None;

        private ShaderProgram current_shader;

        private readonly ShaderProgram default_shader;

        private readonly CanvasSurface main_surface;
        
        private CanvasSurface[] aditional_surfaces;

        private int aditional_surface_idx;

        private Texture2D current_texture;

        private readonly Font default_font;

        private readonly Font default_font2;

        private ushort[] index_array;

        private IndexBuffer index_buffer;

        private readonly Texture2D prim_texture;

        private Rect render_area;

        private RenderState render_state;

        private CanvasStretchMode stretch_mode = CanvasStretchMode.PixelPerfect;

        private Vertex2D[] vertex_array;

        private int vertex_index;

        private readonly int vertex_max_count;

        private Matrix4x4 screen_proj_matrix;

        private byte current_render_pass;

        private byte max_render_pass;

        private float render_scale_x = 1.0f;

        private float render_scale_y = 1.0f;

        private int draw_calls;

    }
}