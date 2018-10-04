using System.Runtime.InteropServices;
using BLITTEngine.Foundation;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct VertexPCT
    {
        [FieldOffset(0)]
        public float X;

        [FieldOffset(4)]
        public float Y;

        [FieldOffset(8)]
        public float U;

        [FieldOffset(12)]
        public float V;

        [FieldOffset(16)]
        public uint Abgr;

        public VertexPCT(float x, float y, float u, float v, uint abgr)
        {
            this.X = x;
            this.Y = y;
            this.U = u;
            this.V = v;
            this.Abgr = abgr;
        }

        public static int Stride => 20;

        public static readonly VertexLayout Layout = new VertexLayout()
            .Begin()
            .Add(VertexAttributeUsage.Position, 2, VertexAttributeType.Float)
            .Add(VertexAttributeUsage.TexCoord0, 2, VertexAttributeType.Float)
            .Add(VertexAttributeUsage.Color0, 4, VertexAttributeType.UInt8, true)
            .End();
    }

}