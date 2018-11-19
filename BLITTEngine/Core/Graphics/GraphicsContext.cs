using BLITTEngine.Core.Foundation;
using System;
using System.Runtime.CompilerServices;
using BLITTEngine.Resources;

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

    public unsafe class GraphicsContext
    {
        private readonly IndexBuffer[] index_buffers;

        private int index_buffers_idx;

        public readonly GraphicsInfo Info;

        internal GraphicsContext(IntPtr graphics_surface_ptr, int width, int height)
        {
            Bgfx.SetPlatformData(new PlatformData() {WindowHandle = graphics_surface_ptr});

            Bgfx.Init();

            Capabilities caps = Bgfx.GetCaps();
            Info = new GraphicsInfo(caps.Backend, caps.MaxTextureSize);

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            ResizeBackBuffer(width, height);

            Content.GraphicsContext = this;
            RenderTarget.GraphicsContext = this;
            Texture2D.GraphicsContext = this;
            ShaderProgram.GraphicsContext = this;

            Content.LoadEmbededShaders(Info.RendererBackend);

            index_buffers = new IndexBuffer[16];
        }

        public void SetClearColor(ushort view, int color)
        {
            Bgfx.SetViewClear(view, ClearTargets.Color, color);
        }

        public void Submit(
            ushort view,
            int vertex_count, 
            ShaderProgram shader, 
            IndexBuffer index_buffer, 
            TransientVertexBuffer vertex_buffer, 
            Texture2D texture, 
            RenderState render_state)
        {
            Bgfx.SetTexture(0, shader.Samplers[0], texture.Texture);

            Bgfx.SetRenderState(render_state);

            Bgfx.SetIndexBuffer(index_buffer, 0, (vertex_count/4) * 6);

            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, vertex_count);

            Bgfx.Submit(view, shader.Program);
        }

        public void SwapBuffers()
        {
            Bgfx.Frame();
        }

        public TransientVertexBuffer StreamVertices2D(Vertex2D[] vertices, int count)
        {
            var vertex_buffer = new TransientVertexBuffer(count, Vertex2D.Layout);

            fixed (void* v = vertices)
            {
                Unsafe.CopyBlock((void*) vertex_buffer.Data, v, (uint) (count * Vertex2D.Stride));
            }

            return vertex_buffer;
        }

        public void ResizeBackBuffer(int width, int height)
        {
            Bgfx.Reset(width, height, ResetFlags.Vsync);
        }

        public void SetRenderTarget(ushort view, RenderTarget render_target)
        {
            Bgfx.SetViewFrameBuffer(view, render_target.FrameBuffer);
        }

        public void SetViewport(ushort view, int x, int y, int w, int h)
        {
            Bgfx.SetViewRect(view, x, y, w, h);
        }

        public void SetTransform(ushort view, float* matrix)
        {
            Bgfx.SetViewTransform(view, null, matrix);
        }

        public void TakeScreenShot(string file_path)
        {
            Bgfx.RequestScreenShot(file_path);
        }

        internal void Shutdown()
        {
            FreeInternalResources();

            Bgfx.Shutdown();
        }

        internal void UpdateTextureAttributes(Texture2D texture)
        {
            TextureFlags tex_flags = BuildTexFlags(texture.Tiled, texture.Filtered, texture.RenderTarget);

            texture.Texture.OverrideInternal(texture.Width, texture.Height, 0, TextureFormat.BGRA8, tex_flags);
        }

        internal void UpdateTextureData(Texture2D texture, Pixmap pixmap)
        {
            MemoryBlock memory = MemoryBlock.MakeRef(pixmap.PixelDataPtr, pixmap.SizeBytes, IntPtr.Zero);

            texture.Texture.Update2D(0, 0, 0, 0, pixmap.Width, pixmap.Height, memory, pixmap.Stride);
        }

        internal Texture2D CreateTexture(byte[] data, int width, int height, bool tiled = false, bool filtered = false,
                                       bool render_target = false)
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

        internal Texture2D CreateTexture(int width, int height, bool tiled = false, bool filtered = false,
                                       bool render_target = false)
        {
            TextureFlags tex_flags = BuildTexFlags(tiled, filtered, render_target);

            var tex_object = Texture.Create2D(
                width: width,
                height: height,
                hasMips: false,
                arrayLayers: 0,
                format: TextureFormat.BGRA8,
                flags: tex_flags
            );

            return new Texture2D(tex_object, render_target);
        }

        internal RenderTarget CreateRenderTarget(int width, int height)
        {
            Texture2D texture = CreateTexture(width, height, filtered: false, tiled: false, render_target: true);

            Attachment[] attachments = { new Attachment() { Texture = texture.Texture, Mip = 0, Layer = 0 } };

            var frame_buffer = new FrameBuffer(attachments, destroyTextures: true);

            return new RenderTarget(frame_buffer, texture);
        }

        internal ShaderProgram CreateShader(byte[] vertex_src, byte[] frag_src)
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

        internal IndexBuffer CreateIndexBuffer(ushort[] indices) 
        {
            var index_buffer = new IndexBuffer(MemoryBlock.FromArray(indices));

            index_buffers[index_buffers_idx++] = index_buffer;

            return index_buffer;
        }

        private static TextureFlags BuildTexFlags(bool tiled, bool filtered, bool render_target)
        {
            TextureFlags tex_flags;

            if (!tiled)
            {
                tex_flags = TextureFlags.ClampU | TextureFlags.ClampV;
            }
            else
            {
                tex_flags = TextureFlags.MirrorU | TextureFlags.MirrorV;
            }

            if (!filtered)
            {
                tex_flags |= TextureFlags.MinFilterPoint | TextureFlags.MagFilterPoint;
            }
            else
            {
                tex_flags |= TextureFlags.MinFilterAnisotropic | TextureFlags.MagFilterAnisotropic;
            }

            if (render_target)
            {
                tex_flags |= TextureFlags.RenderTarget;
            }

            return tex_flags;
        }

        private void FreeInternalResources()
        {
            while (index_buffers_idx > 0)
            {
                index_buffers[--index_buffers_idx].Dispose();
            }
        }
    }
}