using BLITTEngine.Core.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Draw
{
    public sealed class Canvas
    {
        private GraphicsDevice gfx;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Canvas(GraphicsDevice graphics_device, int width, int height)
        {
            gfx = graphics_device;

            this.Width = width;
            this.Height = height;
        }

        public void Clear(Color color)
        {
            gfx.Clear(in color);
        }

        public void Draw(Texture2D texture, float x, float y)
        {
            gfx.DrawTexture(texture, x, y);

        }
    }
}