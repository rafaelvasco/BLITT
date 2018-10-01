using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Draw
{
   
    public sealed class Canvas
    {
        private GraphicsDevice gfx;

        public Canvas(GraphicsDevice graphics_device)
        {
            gfx = graphics_device;
        }
    }
}