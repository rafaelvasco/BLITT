using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Resources
{
    public class Texture2D : Resource
    {
        internal readonly Texture Texture;

        internal TextureFlags TexFlags;

        public int Width { get; protected set; }

        public int Height  { get; protected set; }

        public bool RenderTarget { get; }

        public bool Tiled
        {
            get => tiled;
            set
            {
                if (tiled == value) return;

                tiled = value;

                UpdateTexFlags();
            }
        }

        public bool Filtered
        {
            get => filtered;
            set
            {
                if (filtered == value) return;

                filtered = value;

                UpdateTexFlags();
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

            this.UpdateTexFlags();

        }

        public void SetData(Pixmap pixmap)
        {
            Game.Instance.GraphicsContext.UpdateTextureData(this, pixmap);
        }

        public Pixmap GetData()
        {
            return new Pixmap(this);
        }

        public Pixmap GetData(int srcX, int srcY, int srcW, int srcH)
        {
            return new Pixmap(this, srcX, srcY, srcW, srcH);
        }

        public void BlitTo(Texture2D texture, int srcX, int srcY, int srcW, int srcH)
        {
            this.Texture.BlitTo(0, texture.Texture, 0, 0, srcX, srcY, srcW, srcH);
        }

        internal override void Dispose()
        {
            this.Texture.Dispose();
        }

        private void UpdateTexFlags()
        {
            var flags = BuildTexFlags(Tiled, Filtered, RenderTarget);

            this.TexFlags = flags;
        }

        internal static TextureFlags BuildTexFlags(bool tiled, bool filtered, bool render_target)
        {
            var tex_flags = TextureFlags.None;

            if (!tiled) tex_flags = TextureFlags.ClampU | TextureFlags.ClampV;

            if (!filtered) tex_flags |= TextureFlags.MinFilterPoint | TextureFlags.MagFilterPoint;

            if (render_target) tex_flags |= (TextureFlags.RenderTarget | TextureFlags.RenderTargetWriteOnly);

            return tex_flags;
        }
    }
}