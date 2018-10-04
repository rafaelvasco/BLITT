using System;
using BLITTEngine.Foundation;
using BLITTEngine.Resources;

namespace BLITTEngine.Core.Graphics
{
    public class Texture2D : Resource
    {
        public int Width => InternalTexture.Width;

        public int Height => InternalTexture.Height;

        internal readonly Texture InternalTexture;

        internal TextureFlags Flags { get; private set; }

        public bool Tiled
        {
            get => tiled;
            set
            {
                if (tiled != value)
                {
                    tiled = value;

                    UpdateTextureFlags();
                }
            }
        }

        public bool Smooth
        {
            get => smooth;
            set
            {
                if (smooth != value)
                {
                    smooth = value;

                    UpdateTextureFlags();
                }
            }
        }

        private bool tiled;

        private bool smooth;


        internal Texture2D(byte[] pixel_data, int width, int height)
        {
            UpdateTextureFlags();

            MemoryBlock image_memory = MemoryBlock.FromArray(pixel_data);

            InternalTexture = Texture.Create2D(
               width: width,
                height: height,
                hasMips: false,
                arrayLayers: 0,
                format: TextureFormat.BGRA8,
                flags: Flags,
                memory: image_memory
            );

        }

        internal Texture2D(int width, int height)
        {
            UpdateTextureFlags();

            InternalTexture = Texture.Create2D(
               width: width,
                height: height,
                hasMips: false,
                arrayLayers: 0,

                format: TextureFormat.BGRA8,
                flags: Flags,
                memory: null
            );
        }

        public void SetData(Pixmap pixmap)
        {
            var memory = MemoryBlock.MakeRef(pixmap.PixelDataPtr, pixmap.SizeBytes, IntPtr.Zero);

            InternalTexture.Update2D(0, 0, 0, 0, pixmap.Width, pixmap.Height, memory, pixmap.Stride);
        }

        private void UpdateTextureFlags()
        {
            if (!tiled)
            {
                Flags = TextureFlags.ClampU | TextureFlags.ClampV;
            }
            else
            {
                Flags = TextureFlags.MirrorU | TextureFlags.MirrorV;
            }

            if (!smooth)
            {
                Flags |= TextureFlags.MinFilterPoint | TextureFlags.MagFilterPoint;
            }
            else
            {
                Flags |= TextureFlags.MinFilterAnisotropic | TextureFlags.MagFilterAnisotropic;
            }

            if (InternalTexture != null)
            {
                InternalTexture.OverrideInternal(Width, Height, InternalTexture.MipLevels, InternalTexture.Format, Flags);
            }
        }

        internal override void Dispose()
        {
            InternalTexture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}