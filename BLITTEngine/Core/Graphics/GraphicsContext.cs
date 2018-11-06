using BLITTEngine.Core.Foundation;
using System;

namespace BLITTEngine.Core.Graphics
{
    public struct GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;

        public GraphicsInfo(RendererBackend backend, int max_tex_size)
        {
            this.RendererBackend = backend;
            this.MaxTextureSize = max_tex_size;
        }
    }

    internal class GraphicsContext
    {
        // PASS 0 -> RENDER TO RENDERTARGET
        // PASS 1 -> RENDER RENDERTARGET TEXTURE TO BACKBUFFER

        public readonly GraphicsInfo Info;

        private ShaderProgram current_shader;

        private ShaderProgram base_2d_shader;

        private Texture current_texture;

        public GraphicsContext(IntPtr graphics_surface_ptr, int width, int height)
        {
            Bgfx.SetPlatformData(new PlatformData() { WindowHandle = graphics_surface_ptr });

            Bgfx.Init();

            Capabilities caps = Bgfx.GetCaps();
            Info = new GraphicsInfo(caps.Backend, caps.MaxTextureSize);

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            // RENDERTARGET CLEAR
            Bgfx.SetViewClear(0, ClearTargets.Color, 0x0000FF);

            // BACKBUFFER CLEAR
            Bgfx.SetViewClear(1, ClearTargets.Color,0x000000FF);


        }

        public Texture2D CreateTexture2D(byte[] data, int width, int height, bool tiled=false, bool filtered=false)
        {
            MemoryBlock image_memory = MemoryBlock.FromArray(data);

            TextureFlags tex_flags;

            if(!tiled)
            {
                tex_flags = TextureFlags.ClampU | TextureFlags.ClampV;
            }
            else
            {
                tex_flags = TextureFlags.MirrorU | TextureFlags.MirrorV;
            }

            if(!filtered)
            {
                tex_flags |= TextureFlags.MinFilterPoint | TextureFlags.MagFilterPoint;
            }
            else
            {
                tex_flags |= TextureFlags.MinFilterAnisotropic | TextureFlags.MagFilterAnisotropic;
            }

            var tex_object = Texture.Create2D(
                width: width,
                height: height,
                hasMips: false,
                arrayLayers: 0,
                format: TextureFormat.BGRA8,
                flags: tex_flags,
                memory: image_memory
            );



            var texture_2d = new Tex
        }

        public GraphicsContext()
        {

        }
    }
}
