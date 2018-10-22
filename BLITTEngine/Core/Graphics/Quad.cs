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

        public Quad(Texture2D texture, float x, float y, float w, float h)
        {
            float tw = texture.Width;
            float th = texture.Height;

            this.U = x/tw;
            this.V = y/th;
            this.U2 = (x+w)/tw;
            this.V2 = (y+h)/th;
            this.W = w;
            this.H = h;
            this.Col = 0xFFFFFFFF;
        }
    }
}