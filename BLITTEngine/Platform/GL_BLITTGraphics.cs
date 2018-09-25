using System;
using System.Collections.Generic;
using BLITTEngine.Foundation;
using BLITTEngine.Numerics;

namespace BLITTEngine.Platform
{
    internal class GL_BLITTGraphics : BLITTGraphics
    {
       

        private Dictionary<uint, Texture> textures;

        public void Init(int width, int height)
        {
            textures = new Dictionary<uint, Texture>();

            GL.Disable(EnableCap.DepthTest);
            GL.Viewport(0, 0, width, height);
            GL.ClearColor(255, 0, 0, 255);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }


        public void Clear(ref Color color)
        {
            GL.ClearColor(color.R, color.G, color.B, 255);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Clear()
        {
            GL.ClearColor(0, 0, 0, 255);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void ConfigureTexture(Texture texture, bool repeat, bool smooth)
        {
            throw new NotImplementedException();
        }

        public Texture AddTexture(IntPtr data_ptr, int width, int height, bool repeat, bool smooth, bool is_render_target)
                 {
            GL.GenTextures(1, out var handle);

            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMinFilter.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                GLPixelFormat.Rgba, GLPixelType.UnsignedByte, data_ptr);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            var texture = new Texture(handle, width, height);

            textures.Add(handle, texture);

            return texture;
        }

        public Texture AddTexture(int width, int height, bool repeat, bool smooth, bool is_render_target)
        {
            GL.GenTextures(1, out var handle);

            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMinFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                GLPixelFormat.Rgba, GLPixelType.UnsignedByte, IntPtr.Zero);

            var texture = new Texture(handle, width, height);

            textures.Add(handle, texture);

            return texture;
        }

        public void Cleanup()
        {
            foreach (var texture in textures)
            {
                GL.DeleteTextures(1, ref texture.Value.Handle);
            }

            textures.Clear();
        }

        public void Resize(int w, int h)
        {
            GL.Viewport(0, 0, w, h);
        }

        public void SetTexture(Texture texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.Handle);
        }

        public void Submit()
        {
        }

        
    }
}