using BLITTEngine.Core.Foundation;
using System;
using System.Runtime.CompilerServices;

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


    internal unsafe class GraphicsContext
    {
        public readonly GraphicsInfo Info;


        private RenderState render_state;

        private RenderState base_render_state;


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

            base_render_state = RenderState.WriteRGB;
        }

        public void SetBlendMode(BlendMode blend_mode)
        {
            switch (blend_mode)
            {
                case BlendMode.AlphaBlend:

                    render_state = base_render_state | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInverseSourceAlpha);

                    break;

                case BlendMode.AlphaAdd:

                    render_state = base_render_state | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);

                    break;

                case BlendMode.ColorMul:

                    render_state = base_render_state | RenderState.BlendDarken;

                    break;
            }
        }


        public void Submit(ShaderProgram shader)
        {

        }

        public void SwapBuffers()
        {
            Bgfx.Frame();
        }


        public void StreamVertices2D(Vertex2D[] vertices, int count)
        {
            var vertex_buffer = new TransientVertexBuffer(count, Vertex2D.Layout);

            fixed (void* v = vertices)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)(count * Vertex2D.Stride));
            }
        }

        public void ResizeBackBuffer(int width, int height)
        {
            Bgfx.Reset(width, height, ResetFlags.Vsync);
        }

        public Texture2D CreateTexture(byte[] data, int width, int height, bool tiled=false, bool filtered=false, bool render_target=false)
        {
            MemoryBlock image_memory = MemoryBlock.FromArray(data);

            TextureFlags tex_flags = BuildTexFlags(tiled, filtered, render_target);

            var tex_object = Texture.Create2D(
                width: width,
                height: height,
                hasMips: false,
                arrayLayers: 0,
                format: TextureFormat.BGRA8,
                flags: tex_flags,
                memory: image_memory
            );

            return new Texture2D(tex_object, render_target);
        }

        public ShaderProgram CreateShader(byte[] vertex_src, byte[] frag_src)
        {
            if (vertex_src.Length == 0 || frag_src.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = new Shader(MemoryBlock.FromArray(vertex_src));
            var frag_shader = new Shader(MemoryBlock.FromArray(frag_src));

            var program = new Program(vertex_shader, frag_shader, destroyShaders: true);

            var shader_program = new ShaderProgram(program);

            return shader_program;

        }

        public void FreeShaderProgram(ShaderProgram shader)
        {
            foreach(var shader_param in shader.Parameters)
            {
                shader_param.Uniform.Dispose();
            }

            shader.Program.Dispose();
        }

        public void SubmitShaderAttributes(ShaderProgram program)
        {

        }


        public void UpdateTextureAttributes(Texture2D texture)
        {
            TextureFlags tex_flags = BuildTexFlags(texture.Tiled, texture.Filtered, texture.RenderTarget);

            var tex = textures[texture.TextureHandle];

            tex.OverrideInternal(texture.Width, texture.Height, 0, TextureFormat.BGRA8, tex_flags);
        }

        public void UpdateTextureData(Texture2D texture, Pixmap pixmap)
        {
            var memory = MemoryBlock.MakeRef(pixmap.PixelDataPtr, pixmap.SizeBytes, IntPtr.Zero);

            var tex = textures[texture.TextureHandle];

            tex.Update2D(0, 0, 0, 0, pixmap.Width, pixmap.Height, memory, pixmap.Stride);
        }

        public void FreeTexture(Texture2D texture)
        {
            var tex = textures[texture.TextureHandle];

            tex.Dispose();

            textures[texture.TextureHandle] = null;
        }

        private TextureFlags BuildTexFlags(bool tiled, bool filtered, bool render_target)
        {
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

            if(render_target)
            {
                tex_flags |= TextureFlags.RenderTarget;
            }

            return tex_flags;
        }


    }
}
