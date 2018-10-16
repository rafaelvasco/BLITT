using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;
using System.Runtime.CompilerServices;

namespace BLITTEngine.Core.Graphics
{
    internal class GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;
    }

    internal static unsafe class Renderer
    {
        public static GraphicsInfo Info { get; internal set; }

        private const int MAX_QUADS = 10;

        private static ShaderProgram default_shader;
        private static Texture2D current_texture;
        private static Texture2D one_pix_texture;
        private static VertexPCT[] quad_vertices;
        private static ushort[] quad_indices;
        private static IndexBuffer index_buffer;
        private static int vertex_idx;
        private static int quad_count;
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

            one_pix_texture = Content.CreateTexture(1, 1, Color.White);

            default_shader = Content.GetBuiltinShader("base_2d");
            default_shader.AddTextureUniform("texture_2d");

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

            Bgfx.SetViewClear(0, ClearTargets.Color, Color.CornflowerBlue.RGBAI);

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            ResizeBackbuffer(backbuffer_width, backbuffer_height);

            InitializeRenderBuffers();
        }

        internal static void Terminate()
        {
            Console.WriteLine(" > Disposing Graphics Device");

            index_buffer.Dispose();

            Bgfx.Shutdown();
        }

        public static void Clear(in Color color)
        {
            Bgfx.SetViewClear(0, ClearTargets.Color, color.RGBAI);
        }

        public static void AddQuad(Texture2D texture, float x, float y, in Quad quad)
        {
            if (current_texture != texture)
            {
                Flush();

                current_texture = texture;
            }

            float w = quad.W;
            float h = quad.H;
            float u = quad.U;
            float v = quad.V;
            float u2 = quad.U2;
            float v2 = quad.V2;
            uint col = quad.Col;

            var vidx = vertex_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, u, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, u2, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, u2, v2, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, u, v2, col);
            }

            unchecked
            {
                vertex_idx += 4;
                quad_count++;
            }
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

        public static void End()
        {
            Flush();

            cur_render_group = null;
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

        private static void Flush()
        {
            if (vertex_idx == 0)
            {
                return;
            }

            var vertex_buffer = new TransientVertexBuffer(vertex_idx, VertexPCT.Layout);

            fixed (void* v = quad_vertices)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)vertex_idx * 20);
            }

            default_shader.SetTexture(current_texture, "texture_2d");

            Bgfx.SetRenderState(cur_render_group.RenderState);

            Bgfx.SetVertexBuffer(vertex_buffer, 0, vertex_idx);

            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count * 6);

            Bgfx.Submit(cur_render_group.Id, default_shader.Program);

            vertex_idx = 0;
            quad_count = 0;
        }

        private static void InitializeRenderBuffers()
        {
            quad_vertices = new VertexPCT[MAX_QUADS * 4];

            quad_indices = new ushort[MAX_QUADS * 6];

            ushort indice_i = 0;

            for (int i = 0; i < quad_indices.Length; i += 6, indice_i += 4)
            {
                quad_indices[i + 0] = (ushort)(indice_i + 0);
                quad_indices[i + 1] = (ushort)(indice_i + 1);
                quad_indices[i + 2] = (ushort)(indice_i + 2);
                quad_indices[i + 3] = (ushort)(indice_i + 0);
                quad_indices[i + 4] = (ushort)(indice_i + 2);
                quad_indices[i + 5] = (ushort)(indice_i + 3);
            }

            index_buffer = new IndexBuffer(MemoryBlock.FromArray(quad_indices));
        }
    }
}