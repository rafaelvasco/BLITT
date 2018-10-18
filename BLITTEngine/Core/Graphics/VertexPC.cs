using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Explicit)]
    public struct VertexPC
    {
        [FieldOffset(0)]
        public float X;

        [FieldOffset(4)]
        public float Y;

        [FieldOffset(8)]
        public uint Abgr;

        public VertexPC(float x, float y, uint abgr)
        {
            this.X = x;
            this.Y = y;
            this.Abgr = abgr;
        }
    }
}
