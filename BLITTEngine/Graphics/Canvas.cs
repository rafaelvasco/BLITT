using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using BLITTEngine.Numerics;
using BLITTEngine.Platform;
using BLITTEngine.Resources;

namespace BLITTEngine.Graphics
{
    public enum FullScreenMode
    {
        StretchPixelPerfect,
        StretchLetterBox
    }

    public static class Canvas
    {
        private static int MAX_TRANSFORM_STACK = 32;

        private static Color default_bg_color = Color.Black;

        private static Color bg_color = Color.CornflowerBlue;
        
        private static GraphicsModule gfx;

        private static Vector2[] translate_stack;

        private static int translate_stack_idx;

        private static Vector2 current_translate;

        private static Texture render_target;

        private static int canvas_width;

        private static int canvas_height;

        private static Rectangle render_area;

        private static bool can_draw;

        private static FullScreenMode fullscreen_mode;

        internal static bool SizeChanged;
             

        public static Color BackColor
        {
            get => bg_color;
            set => bg_color = value;
        }


        public static int Width => canvas_width;

        public static int Height => canvas_height;

        public static float CenterX => canvas_width * 0.5f;

        public static float CenterY => canvas_height * 0.5f;

        public static Vector2 Center => new Vector2(CenterX, CenterY);

        public static FullScreenMode FullScreenBehavior
        {
            get => fullscreen_mode;
            set
            {
                if (fullscreen_mode != value)
                {
                    fullscreen_mode = value;

                    Game.Platform.GetScreenSize(out int screen_w, out int screen_h);

                    RecalculateRenderArea(screen_w, screen_h);
                }
            }
        }


        internal static void Init(GamePlatform platform, int width, int height)
        {
            canvas_width = width;
            canvas_height = height;

            gfx = platform.Graphics;

            fullscreen_mode = FullScreenMode.StretchLetterBox;

            render_target = gfx.CreateTexture(
                width, height, 
                wrap_mode: ImageWrapMode.None, 
                filter_mode: ImageFilterMode.Crisp, 
                is_render_target: true);


            translate_stack = new Vector2[MAX_TRANSFORM_STACK];

            Game.Platform.GetScreenSize(out int screen_w, out int screen_h);

            RecalculateRenderArea(screen_w, screen_h);
        }
        
        public static void BeginTarget(Image target)
        {
            if (!target.DataChanged && !target.ConfigChanged)
            {
                gfx.Begin(target.Texture);
                gfx.Clear(ref default_bg_color);
            }
            else
            {
                SyncImageGpuState(target);
            }
            
        }

        public static void EndTarget()
        {
           gfx.Submit();
        }

        internal static void Begin()
        {
            can_draw = true;

            gfx.Begin(render_target);
            gfx.Clear(ref bg_color);
        }

        internal static void End()
        {
            can_draw = false;

            gfx.Submit();

            gfx.Begin();
            gfx.Clear(ref default_bg_color);

            gfx.DrawQuad(render_target, ref render_area);

            gfx.Submit();
        }

        public static void SetTint(Color color)
        {
            gfx.SetColor(ref color);
        }

        public static void DrawRect(float x, float y, float w, float h)
        {
            if(can_draw)
            {
                gfx.DrawRect(x, y, w, h);
            }
            
            gfx.DrawRect(x, y, w, h);
            gfx.DrawRect(x, y, w, h);

        }

        public static void FillRect(float x, float y, float w, float h)
        {
            ref Vector2 ct = ref current_translate;

            gfx.FillRect(x + ct.X, y + ct.Y, w, h);
        }

        public static void DrawCircle(float x, float y, float radius)
        {
            gfx.DrawCircle(x, y, radius);
        }

        public static void FillCircle(float x, float y, float radius)
        {
            gfx.FillCircle(x, y, radius);
        }

        public static void Draw(Image image, float x, float y)
        {
            if (!image.DataChanged && !image.ConfigChanged)
            {
                ref Vector2 ct = ref current_translate;
                gfx.DrawQuad(image.Texture, x + ct.X, y + ct.Y);
            }
            else
            {
                SyncImageGpuState(image);
            }
            
        }

        public static void Draw(Image image, float x, float y, RectangleI srcRect)
        {
            if (!image.DataChanged && !image.ConfigChanged)
            {
                gfx.DrawQuad(image.Texture, x, y, ref srcRect);
            }
            else
            {
                SyncImageGpuState(image);
            }
        }

        public static void Draw(Image image, Rectangle dstRect)
        {
            if(!image.DataChanged && !image.ConfigChanged)
            {
                gfx.DrawQuad(image.Texture, ref dstRect);
            }
            else
            {
                SyncImageGpuState(image);
            }
           
        }
            
        public static void Draw(Image image, RectangleI srcRect, Rectangle dstRect)
        {
            if (!image.DataChanged && !image.ConfigChanged)
            {
                gfx.DrawQuad(image.Texture, ref srcRect, ref dstRect);
            }
            else
            {
                SyncImageGpuState(image);
            }

        }

        public static void Translate(float x, float y)
        {
            current_translate.X += Calc.Round(x);
            current_translate.Y += Calc.Round(y);
        }

        public static void PushTransform()
        {
            if(translate_stack_idx >= translate_stack.Length)
            {
                return;
            }

            translate_stack[translate_stack_idx++] = current_translate;
        }

        public static void PopTransform()
        {
            if(translate_stack_idx >= 0)
            {
                current_translate = translate_stack[--translate_stack_idx];
            }
        }

        public static void Resize(int width, int height)
        {
            canvas_width = width;
            canvas_height = height;

            gfx.DestroyTexture(render_target);

            render_target = gfx.CreateTexture(
                width, height,
                wrap_mode: ImageWrapMode.None,
                filter_mode: ImageFilterMode.Crisp,
                is_render_target: true);

            SizeChanged = true;
        }

        private static void RecalculateRenderArea(int screen_w, int screen_h)
        {
            

            if (Game.Platform.IsFullscreen)
            {
                

                switch (fullscreen_mode)
                {
                    case FullScreenMode.StretchPixelPerfect:

                        var canvas_w = canvas_width;
                        var canvas_h = canvas_height;

                        if (screen_w > canvas_w || screen_h > canvas_h)
                        {
                            var asp_ratio_canvas = (float)canvas_w / canvas_h;
                            var asp_ratio_screen = (float)screen_w / screen_h;

                            var scale_w = Calc.FloorToInt((float)screen_w / canvas_w);
                            var scale_h = Calc.FloorToInt((float)screen_h / canvas_h);

                            if (asp_ratio_screen > asp_ratio_canvas)
                            {
                                scale_w = scale_h;
                            }
                            else
                            {
                                scale_h = scale_w;
                            }

                            var margin_x = (int)((screen_w - canvas_w * scale_w) / 2);
                            var margin_y = (int)((screen_h - canvas_h * scale_h) / 2);

                            render_area = new RectangleI(margin_x, margin_y, canvas_w * scale_w, canvas_h * scale_h);
                            
                        }
                        else
                        {
                            render_area = new RectangleI(0, 0, screen_w, screen_h);
                        }

                        break;

                    case FullScreenMode.StretchLetterBox:

                        var canvas_w2 = canvas_width;
                        var canvas_h2 = canvas_height;

                        var target_ar = canvas_w2 / (float)canvas_h2;

                        var width = screen_w;

                        var height = (int)(width / target_ar + 0.5f);

                        if (height > screen_h)
                        {
                            height = screen_h;

                            width = (int)(height * target_ar + 0.5f);
                        }
                        
                        render_area = new RectangleI(
                            (screen_w/2) - (width/2),
                            (screen_h/2) - (height/2), 
                            width, 
                            height);
                        
                        Console.WriteLine($"Render Area: {render_area.X}, {render_area.Y}, {render_area.W}, {render_area.H}");

                        
                        break;
                }
            }
            else
            {
                render_area = new RectangleI(0, 0, canvas_width, canvas_height);
            }
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SyncImageGpuState(Image image)
        {

            if (image.DataChanged)
            {
                gfx.UpdateTexture(image.Texture, image.Pixmap);
                image.DataChanged = false;
            }

            if(image.ConfigChanged)
            {
                gfx.ConfigureTexture(image.Texture, image.WrapMode, image.FilterMode);
                image.ConfigChanged = false;
            }
        }
       

        internal static void OnScreenResized(int screen_w, int screen_h)
        {
            RecalculateRenderArea(screen_w, screen_h);
        }
    }
}