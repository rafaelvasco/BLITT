using BLITTEngine.Core.Foundation;
using BLITTEngine.Resources;
using System;

namespace BLITTEngine.Core.Graphics
{
    public class Texture2D : Resource
    {
        internal static GraphicsContext GraphicsContext;

        internal Texture Texture;

        public readonly int Width;

        public readonly int Height;

        public readonly bool RenderTarget;

        public bool Tiled
        {
            get => tiled;
            set
            {
                if (tiled != value)
                {
                    tiled = value;

                    GraphicsContext.UpdateTextureAttributes(this);
                }
            }
        }

        public bool Filtered
        {
            get => filtered;
            set
            {
                if (filtered != value)
                {
                    filtered = value;

                    GraphicsContext.UpdateTextureAttributes(this);
                }
            }
        }

        private bool tiled;

        private bool filtered;

        internal Texture2D(Texture texture, bool render_target)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;
            this.RenderTarget = render_target;

        }

        public void SetData(Pixmap pixmap)
        {
            GraphicsContext.UpdateTextureData(this, pixmap);
        }

        internal override void Dispose()
        {
            GraphicsContext.FreeTexture(this);

            GC.SuppressFinalize(this);
        }
    }
}