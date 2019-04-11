using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Resources
{
    public class RenderTarget : Texture2D
    {
        internal FrameBuffer FrameBuffer { get; }

        internal RenderTarget(FrameBuffer frame_buffer, int width, int height): base(frame_buffer.GetTexture(), true, false, false)
        {
            FrameBuffer = frame_buffer;
            Width = width;
            Height = height;
        }

        internal override void Dispose()
        {
            FrameBuffer.Dispose();
        }
    }
}