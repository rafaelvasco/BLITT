using System.Numerics;
using BLITTEngine.Numerics;
using BLITTEngine.Platform;
using BLITTEngine.Resources;

namespace BLITTEngine.Graphics
{
    public static class Canvas
    {
        private static Color default_clear_col = Color.Black;
        
        private static GraphicsModule gfx;

        private static readonly Vector2[] translate_stack;

        private static int translate_stack_idx;

        private static Vector2 current_translate;

        private static Texture render_target;
       

        internal static void Init(GamePlatform platform, int virtual_width, int virtual_height)
        {
            gfx = platform.Graphics;

            render_target = gfx.CreateTexture(virtual_width, virtual_height, is_render_target: true);
        }

        public static void BeginTarget(Image target)
        {
            if (target != null && target.Invalidated)
            {
                gfx.UpdateTexture(target.Texture, target.Pixmap);
                target.Invalidated = false;
            }
            
            gfx.BeginDraw(target.Texture);
            
        }

        public static void EndTarget()
        {
           gfx. 
        }

        internal static void BeginDraw()
        {

        }

        internal static void EndDraw()
        {
            gfx.EndDraw();
        }

        public static void Clear()
        {
            gfx.Clear(ref default_clear_col);
        }

        public static void Clear(Color color)
        {
            gfx.Clear(ref color);
        }

       

        public static void SetTint(Color color)
        {
            gfx.SetColor(ref color);
        }

        public static void DrawRect(float x, float y, float w, float h)
        {
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
            if (image.Invalidated)
            {
                gfx.UpdateTexture(image.Texture, image.Pixmap);
                image.Invalidated = false;
            }

            ref Vector2 ct = ref current_translate;

            gfx.DrawTexture(image.Texture, x + ct.X, y + ct.Y);
        }

        public static void Draw(Image image, float x, float y, RectangleI srcRect)
        {
            if (image.Invalidated)
            {
                gfx.UpdateTexture(image.Texture, image.Pixmap);
                image.Invalidated = false;
            }
            
            gfx.DrawTexture(image.Texture, x, y, ref srcRect);
        }

        public static void Draw(Image image, Rectangle dstRect)
        {
            if (image.Invalidated)
            {
                gfx.UpdateTexture(image.Texture, image.Pixmap);
                image.Invalidated = false;
            }
            
            gfx.DrawTexture(image.Texture, ref dstRect);
        }
            

        public static void Draw(Image image, RectangleI srcRect, Rectangle dstRect)
        {
            if (image.Invalidated)
            {
                gfx.UpdateTexture(image.Texture, image.Pixmap);
                image.Invalidated = false;
            }
            
            gfx.DrawTexture(image.Texture, ref srcRect, ref dstRect);
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
    }
}