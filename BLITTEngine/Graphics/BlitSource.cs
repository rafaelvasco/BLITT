using BLITTEngine.Numerics;
using BLITTEngine.Resources;

namespace BLITTEngine.Graphics
{
    public interface BlitSource
    {
        ref RectangleI this[int index] { get; }
        Image SourceImage { get; }
    }
}