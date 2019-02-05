using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    public class RenderTarget : Resource
    {
        internal FrameBuffer FrameBuffer { get; }

        internal Texture2D Texture { get; }

        internal RenderTarget(FrameBuffer frame_buffer, Texture2D texture)
        {
            FrameBuffer = frame_buffer;
            this.Texture = texture;
        }

        internal override void Dispose()
        {
            FrameBuffer.Dispose();
        }
    }
}