using System;
using System.IO;
using System.Runtime.InteropServices;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Foundation.STB
{
    public unsafe class ImageWriter
    {
        private Stream _stream;
        private byte[] _buffer = new byte[1024];

        private int WriteCallback(void* context, void* data, int size)
        {
            if (data == null || size <= 0)
            {
                return 0;
            }

            if (_buffer.Length < size)
            {
                _buffer = new byte[size * 2];
            }

            var bptr = (byte*) data;

            Marshal.Copy(new IntPtr(bptr), _buffer, 0, size);

            _stream.Write(_buffer, 0, size);

            return size;
        }

        public void WritePng(Pixmap image, string output)
        {
            try
            {
                _stream = new FileStream(output, FileMode.Create, FileAccess.Write);

                fixed (byte* b = &image.PixelData[0])
                {
                    STBImageWrite.stbi_write_png_to_func(WriteCallback, null, image.Width, image.Height, 4, b,
                        image.Width * 4);
                }
            }
            finally
            {
                _stream = null;
            }
        }

        public void WritePng(IntPtr data, int width, int height, string output)
        {
            try
            {
                _stream = new FileStream(output, FileMode.Create, FileAccess.Write);

                STBImageWrite.stbi_write_png_to_func(WriteCallback, null, width, height, 4, (void*) data,
                    width * 4);
            }
            finally
            {
                _stream = null;
            }
        }

        /// <summary>
        /// Writes JPG File
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dest"></param>
        /// <param name="quality">Should be beetween 1 & 100</param>
        public void WriteJpg(Pixmap image, int quality, string output)
        {
            try
            {
                _stream = new FileStream(output, FileMode.Create, FileAccess.Write);

                fixed (byte* b = &image.PixelData[0])
                {
                    STBImageWrite.stbi_write_jpg_to_func(WriteCallback, null, image.Width, image.Height, 4, b, quality);
                }
            }
            finally
            {
                _stream = null;
            }
        }
    }
}