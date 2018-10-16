using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Quad
    {
        public float U;
        public float V;
        public float U2;
        public float V2;
        public float W;
        public float H;
        public uint Col;
    }
}