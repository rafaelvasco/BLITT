using BLITTEngine.Foundation;
using BLITTEngine.Numerics;
using System.Numerics;

namespace BLITTEngine.Core.Graphics
{
    public class RenderGroup
    {
        public byte Id { get; internal set; }
        public Matrix4x4 ProjectionMatrix;
        public RectangleI Viewport;
        public RenderState RenderState;



    }
}
