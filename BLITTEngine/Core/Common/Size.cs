using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Size
    {
        public int W;
        public int H;

        public Size(int w, int h)
        {
            this.W = w;
            this.H = h;
        }

        public Size(float w, float h)
        {
            this.W = (int) w;
            this.H = (int) h;
        }
    }
}