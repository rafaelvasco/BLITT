using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    public class Texture2D : Resource
    {
        internal static GraphicsContext GraphicsContext;

        internal Texture Texture;

        internal TextureFlags TexFlags;

        internal string Name;

        public readonly int Width;

        public readonly int Height;

        public readonly bool RenderTarget;

        public bool Tiled
        {
            get => tiled;
            set
            {
                if (tiled == value) return;

                tiled = value;

                GraphicsContext.UpdateTexFlags(this);
            }
        }

        public bool Filtered
        {
            get => filtered;
            set
            {
                if (filtered == value) return;

                filtered = value;

                GraphicsContext.UpdateTexFlags(this);
            }
        }

        private bool tiled;

        private bool filtered;

        internal Texture2D(Texture texture, bool render_target, bool filtered, bool tiled)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;
            this.RenderTarget = render_target;
            this.filtered = filtered;
            this.tiled = tiled;

        }

        public void SetData(Pixmap pixmap)
        {
            GraphicsContext.UpdateTextureData(this, pixmap);
        }

        internal override void Dispose()
        {
            this.Texture.Dispose();
        }
    }
}