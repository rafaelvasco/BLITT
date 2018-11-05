using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    public class RenderTarget
    {
        internal FrameBuffer Handle => handle;
        internal Texture2D Texture => texture;

        private Texture2D texture;
        private FrameBuffer handle;

        internal RenderTarget(int width, int height)
        {

            this.texture = new Texture2D(width, height, render_target: true);

            Attachment[] attachments = { new Attachment() { Texture = this.texture.InternalTexture, Mip = 0, Layer = 0 } };

            this.handle = new FrameBuffer(attachments, destroyTextures: true);
        }
    }
}
