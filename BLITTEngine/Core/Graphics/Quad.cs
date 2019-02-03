using System.Runtime.InteropServices;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Quad
    {
        public Vertex2D V0;
        public Vertex2D V1;
        public Vertex2D V2;
        public Vertex2D V3;

        //public Vertex2D[] Vertices;
        public Texture2D Texture;
        public BlendMode Blend;
    }
}