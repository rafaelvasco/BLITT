using System;
using System.Numerics;
using System.Runtime.InteropServices;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Platform
{
    internal class Texture
    {
        public IntPtr Handle;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Texture(IntPtr handle, int width, int height)
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
        void Init(int width, int height);
        Texture CreateTexture(Pixmap pixmap, bool repeat, bool smooth, bool is_render_target);
        Texture CreateTexture(int width, int height, bool repeat, bool smooth, bool is_render_target);
        void UpdateTexture(Texture texture, Pixmap pixmap);
        void ConfigureTexture(Texture texture, bool repeat, bool smooth);
        void DestroyTexture(Texture texture);
        void Clear(ref Color color);
        void Clear();
        void Resize(int w, int h);
        void SetTexture(Texture texture);
        void Submit();
    }
}