using BLITTEngine.Core.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Draw
{
    public sealed class Canvas
    {
        private GraphicsDevice gfx;

        public Canvas(GraphicsDevice graphics_device)
        {
            gfx = graphics_device;
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