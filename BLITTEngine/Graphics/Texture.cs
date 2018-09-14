using System;

namespace BLITTEngine.Graphics
{
    public class Texture
    {
        public IntPtr TextureHandle { get; internal set; }
        public IntPtr RenderTargetHandle { get; internal set; }
        public int Width { get; }
        public int Height { get; }

        public bool IsRenderTarget => RenderTargetHandle != IntPtr.Zero;
        
        public Texture(IntPtr tex_handle, IntPtr render_target_handle, int width, int height)
        {
            this.TextureHandle = tex_handle;
            this.RenderTargetHandle = render_target_handle;
            this.Width = width;
            this.Height = height;
        }
    }
}