using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    public unsafe class Pixmap : Resource
    {
        public int Width { get; }

        public int Height { get; }

        public byte[] PixelData { get; }

        internal IntPtr PixelDataPtr { get; }

        public int SizeBytes { get; }

        public int Stride => Width * 4;

        private readonly GCHandle gc_handle;

        public Pixmap(byte[] src_data, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.SizeBytes = src_data.Length;

            this.PixelData = new byte[src_data.Length];
            Buffer.BlockCopy(src_data, 0, this.PixelData, 0, src_data.Length);

            SwizzleToBGRA();

            gc_handle = GCHandle.Alloc(this.PixelData, GCHandleType.Pinned);
            PixelDataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(this.PixelData, 0);
        }

        public Pixmap(IntPtr data, int width, int height)
        {
            this.Width = width;
            this.Height = height;

            var length = width * height * 4;

            this.SizeBytes = length;
            this.PixelData = new byte[length];

            gc_handle = GCHandle.Alloc(this.PixelData, GCHandleType.Pinned);
            PixelDataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(this.PixelData, 0);

            Unsafe.CopyBlock((void*) PixelDataPtr, (void*) data, (uint) length);
        }

        public Pixmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.SizeBytes = width * height;

            int length = width * height * 4;

            PixelData = new byte[length];
            gc_handle = GCHandle.Alloc(PixelData, GCHandleType.Pinned);
            PixelDataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(PixelData, 0);
        }

        public void Fill(Color color)
        {
            var pd = PixelData;
            byte r = (byte) (color.R * 255);
            byte g = (byte) (color.G * 255);
            byte b = (byte) (color.B * 255);
            byte a = (byte) (color.A * 255);

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

        public void SaveToFile(string path)
        {
            var image_writer = new ImageWriter();

            SwizzleToRGBA();

            image_writer.WritePng(this, path);

            SwizzleToBGRA();
        }

        public void FlipY()
        {
            STBImage.stbi__vertical_flip((void*) PixelDataPtr, Width, Height, 4);
        }

        private void SwizzleToRGBA()
        {
            var pd = PixelData;

            fixed (byte* p = pd)
            {
                var len = pd.Length - 4;
                for (int i = 0; i <= len; i += 4)
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
        }

        private void SwizzleToBGRA()
        {
            var pd = PixelData;

            fixed (byte* p = pd)
            {
                var len = pd.Length - 4;
                for (int i = 0; i <= len; i += 4)
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

        internal override void Dispose()
        {
            gc_handle.Free();
        }
    }
}