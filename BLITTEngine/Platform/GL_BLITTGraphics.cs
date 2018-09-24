using System;
using BLITTEngine.Foundation;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Platform
{
    internal class GL_BLITTGraphics : BLITTGraphics
    {
        private uint vertex_array;
        private uint vbo;


        public void Init(int width, int height)
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Viewport(0, 0, width, height);
            GL.ClearColor(255, 0, 0, 255);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.GenVertexArrays(1, out vertex_array);
            GL.GenBuffers(1, out vbo);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, )

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

        public Texture CreateTexture(Pixmap pixmap, bool repeat, bool smooth, bool is_render_target)
        {
            throw new NotImplementedException();
        }

        public Texture CreateTexture(int width, int height, bool repeat, bool smooth, bool is_render_target)
        {
            throw new NotImplementedException();
        }

        public void DestroyTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public void Resize(int w, int h)
        {
            GL.Viewport(0, 0, w, h);
        }

        public void SetTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }

        public void UpdateTexture(Texture texture, Pixmap pixmap)
        {
            throw new NotImplementedException();
        }
    }
}
