using BLITTEngine.Core.Foundation;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using BLITTEngine.Core.Resources;

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
        class BgfxCallbackHandler : ICallbackHandler
        {
            public void ReportError(string fileName, int line, ErrorType errorType, string message)
            {
            }

            public void ReportDebug(string fileName, int line, string format, IntPtr args)
            {
            }

            public void ProfilerBegin(string name, int color, string filePath, int line)
            {
            }

            public void ProfilerEnd()
            {
            }

            public int GetCachedSize(long id)
            {
                return 0;
            }

            public bool GetCacheEntry(long id, IntPtr data, int size)
            {
                return false;
            }

            public void SetCacheEntry(long id, IntPtr data, int size)
            {
            }

            public void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical)
            {
                var pixmap = new Pixmap(data, width, height);

                pixmap.SaveToFile(path);

                pixmap.Dispose();
            }

            public void CaptureStarted(int width, int height, int pitch, TextureFormat format, bool flipVertical)
            {
            }

            public void CaptureFinished()
            {
            }

            public void CaptureFrame(IntPtr data, int size)
            {
            }
        }

        private readonly IndexBuffer[] index_buffers;

        private int index_buffers_idx;

        public readonly GraphicsInfo Info;

        internal GraphicsContext(IntPtr graphics_surface_ptr, int width, int height)
        {
            Stopwatch timer = Stopwatch.StartNew();

            Bgfx.SetPlatformData(new PlatformData()
            {
                WindowHandle = graphics_surface_ptr
            });

            var bgfx_callback_handler = new BgfxCallbackHandler();

            var settings = new InitSettings() {
                Backend = RendererBackend.Default,
                ResetFlags = ResetFlags.Vsync,
                Width = width,
                Height = height,
                CallbackHandler = bgfx_callback_handler
            };

            Bgfx.Init(settings);

            Console.WriteLine($" > GFX INIT : {timer.Elapsed.TotalSeconds}");

            Capabilities caps = Bgfx.GetCaps();
            Info = new GraphicsInfo(caps.Backend, caps.MaxTextureSize);

            Console.WriteLine($"GRAPHICS BACKEND : {Info.RendererBackend}");

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

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
            Bgfx.SetTexture(0, shader.Samplers[0], texture.Texture, texture.TexFlags);

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

        internal void UpdateTextureData(Texture2D texture, Pixmap pixmap)
        {
            MemoryBlock memory = MemoryBlock.MakeRef(pixmap.PixelDataPtr, pixmap.SizeBytes, IntPtr.Zero);

            texture.Texture.Update2D(0, 0, 0, 0, pixmap.Width, pixmap.Height, memory, pixmap.Stride);

        }

        internal Texture2D CreateTexture(Pixmap pixmap, bool tiled = true, bool filtered = false,
                                       bool render_target = false)
        {

            TextureFlags tex_flags = BuildTexFlags(tiled, filtered, render_target);

            var tex_object = Texture.Create2D(
                width: pixmap.Width,
                height: pixmap.Height,
                hasMips: false,
                arrayLayers: 0,
                format: TextureFormat.BGRA8,
                flags: tex_flags
            );
            

            var tex_2d = new Texture2D(tex_object, render_target, filtered, tiled)
            {
                TexFlags = tex_flags
            };

            UpdateTextureData(tex_2d, pixmap);

            return tex_2d;
        }

        internal Texture2D CreateTexture(int width, int height, bool tiled = true, bool filtered = false,
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

            var tex = new Texture2D(tex_object, render_target, filtered, tiled) {TexFlags = tex_flags};


            return tex;
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

        internal static void UpdateTexFlags(Texture2D tex)
        {
            TextureFlags flags = BuildTexFlags(tex.Tiled, tex.Filtered, tex.RenderTarget);

            tex.TexFlags = flags;
        } 

        private static TextureFlags BuildTexFlags(bool tiled, bool filtered, bool render_target)
        {
            TextureFlags tex_flags = TextureFlags.None;

            if (!tiled)
            {
                tex_flags = TextureFlags.ClampU | TextureFlags.ClampV;
            }

            if (!filtered)
            {
                tex_flags |= TextureFlags.MinFilterPoint | TextureFlags.MagFilterPoint;
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