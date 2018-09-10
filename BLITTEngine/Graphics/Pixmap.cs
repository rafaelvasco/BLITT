using System;
using System.Runtime.InteropServices;
using BLITTEngine.Platform;

namespace BLITTEngine.Graphics
{
    public class Pixmap : IDisposable
    {
        public int Width => width;
        public int Height => height;
        public byte[] PixelData => data;
        internal IntPtr PixelDataPtr => data_ptr;
        
        private int width;
        private int height;

        private byte[] data;
        private GCHandle gc_handle;
        private IntPtr data_ptr;
        

        internal Pixmap(byte[] src_data, int width, int height)
        {
            this.width = width;
            this.height = height;
            
            this.data = new byte[src_data.Length];
            Buffer.BlockCopy(src_data, 0, this.data, 0, src_data.Length);

            gc_handle = GCHandle.Alloc(this.data, GCHandleType.Pinned);
            data_ptr = Marshal.UnsafeAddrOfPinnedArrayElement(this.data, 0);
        }

        internal Pixmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            int length = width * height * 4;
            
            data = new byte[length];
            gc_handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            data_ptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
        }
        
        public byte[] GetRgbaBytes()
        {
            var pixels = new byte[this.PixelData.Length];

            Buffer.BlockCopy(this.PixelData, 0, pixels, 0, this.PixelData.Length);

            var pd = pixels;

            // Return to RGBA Format
            for (int i = 0; i < Width * Height; ++i)
            {
                var idx = i * 4;
                byte b = pd[idx];
                byte g = pd[idx + 1];
                byte r = pd[idx + 2];
                byte a = pd[idx + 3];

                pd[idx] = r;
                pd[idx + 1] = g;
                pd[idx + 2] = b;
                pd[idx + 3] = a;
            }

            return pixels;
        }

        public void Dispose()
        {
            gc_handle.Free();
        }
    }
}