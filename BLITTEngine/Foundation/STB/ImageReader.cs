using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace BLITTEngine.Foundation.STB
{
    public unsafe class ImageReader
    {
        public class AnimatedGifFrame
        {
            public byte[] Data;
            public int Delay;
        }

        private Stream _stream;
        private byte[] _buffer = new byte[1024];

        private readonly STBImage.stbi_io_callbacks _callbacks;

        public ImageReader()
        {
            _callbacks = new STBImage.stbi_io_callbacks
            {
                read = ReadCallback,
                skip = SkipCallback,
                eof = Eof
            };
        }

        private int SkipCallback(void* user, int i)
        {
            return (int)_stream.Seek(i, SeekOrigin.Current);
        }

        private int Eof(void* user)
        {
            return _stream.CanRead ? 1 : 0;
        }

        private int ReadCallback(void* user, sbyte* data, int size)
        {
            if (size > _buffer.Length)
            {
                _buffer = new byte[size * 2];
            }

            var res = _stream.Read(_buffer, 0, size);
            Marshal.Copy(_buffer, 0, new IntPtr(data), size);
            return res;
        }

        public Image Read(Stream stream, int req_comp = STBImage.STBI_rgb_alpha)
        {
            _stream = stream;

            try
            {
                int x, y, comp;
                var result = STBImage.stbi_load_from_callbacks(_callbacks, null, &x, &y, &comp, req_comp);

                var image = new Image
                {
                    Width = x,
                    Height = y
                    
                };

                if (result == null)
                {
                    throw new Exception(STBImage.LastError);
                }

                var c = req_comp != 0 ? req_comp : comp;
                var data = new byte[x * y * c];
                Marshal.Copy(new IntPtr(result), data, 0, data.Length);
                CRuntime.free(result);

                image.Data = data;

                return image;
            }
            finally
            {
                _stream = null;
            }
        }

        public AnimatedGifFrame[] ReadAnimatedGif(Stream stream, out int w, out int h, int req_comp = STBImage.STBI_default)
        {
            try
            {
                w = h = 0;

                var res = new List<AnimatedGifFrame>();
                _stream = stream;

                var context = new STBImage.stbi__context();
                STBImage.stbi__start_callbacks(context, _callbacks, null);

                if (STBImage.stbi__gif_test(context) == 0)
                {
                    throw new Exception("Input stream is not GIF file.");
                }

                var g = new STBImage.stbi__gif();

                do
                {
                    int ccomp;
                    var result = STBImage.stbi__gif_load_next(context, g, &ccomp, req_comp);
                    if (result == null)
                    {
                        break;
                    }

                    var c = req_comp != 0 ? req_comp : ccomp;
                    var data = new byte[g.w * g.h * c];
                    Marshal.Copy(new IntPtr(result), data, 0, data.Length);
                    CRuntime.free(result);

                    var frame = new AnimatedGifFrame
                    {
                        Data = data,
                        Delay = g.delay
                    };
                    res.Add(frame);
                } while (true);

                CRuntime.free(g._out_);

                if (res.Count > 0)
                {
                    w = g.w;
                    h = g.h;
                }

                return res.ToArray();
            }
            finally
            {
                _stream = null;
            }
        }
    }
}
