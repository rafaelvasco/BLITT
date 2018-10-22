using BLITTEngine.Draw;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Graphics
{
    public unsafe class Pixmap : Resource
    {
        public int Width => width;
        public int Height => height;
        public byte[] PixelData => data;
        public int SizeBytes => size;
        public int Stride => width * 4;
        internal IntPtr PixelDataPtr => data_ptr;

        private readonly int width;
        private readonly int height;

        private readonly byte[] data;
        private readonly GCHandle gc_handle;
        private readonly IntPtr data_ptr;
        private int size;

        public Pixmap(byte[] src_data, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = src_data.Length;

            this.data = new byte[src_data.Length];
            Buffer.BlockCopy(src_data, 0, this.data, 0, src_data.Length);

            SwizzleToBGRA();

            gc_handle = GCHandle.Alloc(this.data, GCHandleType.Pinned);
            data_ptr = Marshal.UnsafeAddrOfPinnedArrayElement(this.data, 0);
        }

        public Pixmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = width * height;

            int length = width * height * 4;

            data = new byte[length];
            gc_handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            data_ptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
        }

        public void Fill(Color color)
        {
            var pd = data;
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;
            byte a = color.A;

            fixed (byte* p = pd)
            {
                var len = pd.Length - 4;
                for (int i = 0; i <= len; i += 4)
                {
                    *(p + i) = b;
                    *(p + i + 1) = g;
                    *(p + i + 2) = r;
                    *(p + i + 3) = a;
                }
            }
        }

        private void SwizzleToBGRA()
        {
            var pd = data;

            fixed (byte* p = pd)
            {
                var len = pd.Length - 4;
                for (int i = 0; i < len; i += 4)
                {
                    byte r = pd[i];
                    byte g = pd[i + 1];
                    byte b = pd[i + 2];
                    byte a = pd[i + 3];

                    *(p + i) = b;
                    *(p + i + 1) = g;
                    *(p + i + 2) = r;
                    *(p + i + 3) = a;
                }
            }
        }

        public byte[] GetRgbaBytes()
        {
            var pixels = new byte[this.PixelData.Length];

            Buffer.BlockCopy(this.PixelData, 0, pixels, 0, this.PixelData.Length);

            var pd = pixels;

            fixed (byte* p = pd)
            {
                var len = pd.Length - 4;
                for (int i = 0; i < len; i += 4)
                {
                    byte b = pd[i];
                    byte g = pd[i + 1];
                    byte r = pd[i + 2];
                    byte a = pd[i + 3];

                    *(p + i) = r;
                    *(p + i + 1) = g;
                    *(p + i + 2) = b;
                    *(p + i + 3) = a;
                }
            }

            return pixels;
        }

        internal override void Dispose()
        {
            gc_handle.Free();
        }
    }
}