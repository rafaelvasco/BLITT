using BLITTEngine.Numerics;
using BLITTEngine.Platform;
using BLITTEngine.Resources;

namespace BLITTEngine.Graphics
{
    public static class Canvas
    {
        private static Color default_clear_col = Color.Black;
        
        private static GraphicsModule gfx;
      

        internal static void Init(GamePlatform platform)
        {
            gfx = platform.Graphics;
        }

        public static void Begin(Image target = null)
        {
            if (target != null && target.Invalidated)
            {
                gfx.UpdateTexture(target.Texture, target.Pixmap);
                target.Invalidated = false;
            }
            
            gfx.BeginDraw(target?.Texture);
        }

        public static void Clear()
        {
            gfx.Clear(ref default_clear_col);
        }

        public static void Clear(Color color)
        {
            gfx.Clear(ref color);
        }

        public static void End()
        {
            gfx.EndDraw();
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
            gfx.FillRect(x, y, w, h);
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
            
            gfx.DrawTexture(image.Texture, x, y);
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
    }
}