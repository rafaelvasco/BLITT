using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;

namespace BLITTEngine.Core.Graphics
{
    internal class GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;
    }

    internal static unsafe partial class Renderer
    {
        public static GraphicsInfo Info { get; internal set; }

        private static int backbuffer_width;

        private static int backbuffer_height;

        private static RenderGroup cur_render_group;

        internal static void Init(IntPtr surface_handle, int width, int height)
        {
            backbuffer_width = width;
            backbuffer_height = height;

            Bgfx.SetPlatformData(new PlatformData() { WindowHandle = surface_handle });

            Bgfx.Init();

            Info = new GraphicsInfo();

            Capabilities caps = Bgfx.GetCaps();

            Info.RendererBackend = caps.Backend;
            Info.MaxTextureSize = caps.MaxTextureSize;

            Content.LoadEmbededShaders(Info.RendererBackend);
            //Content.LoadEmbededTextures();

            texq_base_shader = Content.GetBuiltinShader("base_2d");
            texq_base_shader.AddTextureUniform("texture_2d");

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

            Bgfx.SetViewClear(0, ClearTargets.Color, Color.CornflowerBlue.RGBAI);

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            ResizeBackbuffer(backbuffer_width, backbuffer_height);

            InitTexQuadRenderResources();

            InitShapeRenderResources();

        }

        internal static void Terminate()
        {
            Console.WriteLine(" > Disposing Graphics Device");

            texq_idx_buffer.Dispose();

            Bgfx.Shutdown();
        }

        public static void Clear(in Color color)
        {
            Bgfx.SetViewClear(0, ClearTargets.Color, color.RGBAI);
        }



        public static void SetupRenderGroup(RenderGroup render_group)
        {
            var proj_matrix = render_group.ProjectionMatrix;
            var viewport = render_group.Viewport;

            Bgfx.SetViewTransform(render_group.Id, null, &proj_matrix.M11);
            Bgfx.SetViewRect(render_group.Id, viewport.X, viewport.Y, viewport.W, viewport.H);
        }

        public static void Begin(RenderGroup render_group)
        {
            Bgfx.Touch(render_group.Id);

            cur_render_group = render_group;
        }

        public static void Flip()
        {
            Bgfx.Frame();
        }

        public static void ResizeBackbuffer(int width, int height)
        {
            Bgfx.Reset(width, height, ResetFlags.Vsync);
        }

        internal static ShaderProgram CreateShaderProgram(string name, byte[] vertex_shader_src, byte[] frag_shader_src)
        {
            if (vertex_shader_src.Length == 0 || frag_shader_src.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = new Shader(MemoryBlock.FromArray(vertex_shader_src));
            var frag_shader = new Shader(MemoryBlock.FromArray(frag_shader_src));

            return new ShaderProgram(vertex_shader, frag_shader);
        }

        public static void Flush()
        {
            if(texq_vertex_idx > 0)
            {
                FlushTexturedQuads();
            }

            if(shape_vertex_idx > 0)
            {
                FlushShapes();
            }

        }
    }
}