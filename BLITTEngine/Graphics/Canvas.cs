using BLITTEngine.Numerics;
using BLITTEngine.Platform;
using BLITTEngine.Resources;

namespace BLITTEngine.Graphics
{
    public static class Canvas
    {
        public static Color BackgroundColor
        {
            get => background_color;
            set
            {
                background_color = value;
                
                gfx.SetClearColor(ref background_color);
            }
        }
        
        private static GraphicsModule gfx;
        private static Color background_color;
        private static BlitSource blit_source;
        
        internal static void Init(GraphicsModule graphicsModule)
        {
            gfx = graphicsModule;
            
            BackgroundColor = Color.Black;
        }

        

        public static void SetSurface(Image image)
        {
            
        }

        public static void SetBlitSource(BlitSource source)
        {
            blit_source = source;
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

        public static void Blit(float x, float y)
        {
            gfx.DrawTexture(blit_source.SourceImage.Texture, x, y);
        }

        public static void Blit(float x, float y, int tile)
        {
            ref RectangleI src_rect = ref blit_source[tile];
            
            gfx.DrawTexture(blit_source.SourceImage.Texture, x, y, ref src_rect);
        }
    }
}