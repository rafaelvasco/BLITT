using System;
using System.Numerics;
using System.Runtime.InteropServices;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Platform
{
    internal class Texture
    {
        public uint Handle;
        public int Width { get; }
        public int Height { get; }

        public Texture(uint handle, int width, int height)
        {
            this.Handle = handle;
            this.Width = width;
            this.Height = height;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Vertex2D
    {
        public Vector3 Pos;
        public Color Col;
        public Vector2 Tex;
    }

    internal class VertexBuffer : IDisposable
    {
        public IntPtr BufferPtr => buffer_ptr;
        public Vertex2D[] Data => data;

        private IntPtr buffer_ptr;
        private GCHandle gc_handle;
        private Vertex2D[] data;

        public VertexBuffer(int length)
        {
            data = new Vertex2D[length];
            gc_handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        }

        public void Dispose()
        {
            gc_handle.Free();
            data = null;
            GC.SuppressFinalize(this);
        }
    }


    
    internal interface BLITTGraphics
    {
        void Init(int width, int height);
        Texture AddTexture(IntPtr data_ptr, int width, int height, bool repeat, bool smooth, bool is_render_target);
        Texture AddTexture(int width, int height, bool repeat, bool smooth, bool is_render_target);
        void Cleanup();
        void ConfigureTexture(Texture texture, bool repeat, bool smooth);
        void Clear(ref Color color);
        void Clear();
        void Resize(int w, int h);
        void SetTexture(Texture texture);
        void SubmitVertices(Buffer buffer,)
        void Present();
    }
}