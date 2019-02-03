using System.Runtime.InteropServices;
using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex2D
    {
        public float X;

        public float Y;

        public float Tx;

        public float Ty;

        public uint Col;

        public Vertex2D(float x, float y, float tx, float ty, uint abgr)
        {
            this.X = x;
            this.Y = y;
            this.Tx = tx;
            this.Ty = ty;
            this.Col = abgr;
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