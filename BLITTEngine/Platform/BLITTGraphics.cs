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
    
    internal interface BLITTGraphics
    {
        Texture AddTexture(IntPtr data_ptr, int width, int height, bool repeat, bool smooth, bool is_render_target);
        Texture AddTexture(int width, int height, bool repeat, bool smooth, bool is_render_target);
        void Cleanup();
        void ConfigureTexture(Texture texture, bool repeat, bool smooth);
        void Clear(ref Color color);
        void Clear();
        void Resize(int w, int h);
        void SetTexture(Texture texture);
        void Submit();
    }
}