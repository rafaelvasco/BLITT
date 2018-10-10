using BLITTEngine.Foundation;
using BLITTEngine.Foundation.STB;
using BLITTEngine.Numerics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BLITTEngine.Core.Graphics
{
    internal class GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;
    }

    internal class DrawPass
    {

    }

    internal class GraphicsDevice : IDisposable
    {
        private const string EMBEDED_SHADERS_PATH = "BLITTEngine.EngineResources.Shaders.bin";
        private const string EMBEDED_TEXTURES_PATH = "BLITTEngine.EngineResources.Images";

        public GraphicsInfo Info { get; }

        private const int MAX_QUADS = 10;

        private ShaderProgram default_shader;
        private Dictionary<string, ShaderProgram> shaders_catalog;
        private Texture2D current_texture;
        private Texture2D dummy_texture;
        private VertexPCT[] quad_vertices;
        private Matrix4x4 proj_matrix;
        private Matrix4x4 proj_matrix_gui;
        private ushort[] quad_indices;
        private IndexBuffer index_buffer;
        private int vertex_idx;
        private int quad_count;
        private int backbuffer_width;
        private int backbuffer_height;
        private byte cur_view;
        private float dummy_x;
        private float dummy_x2;

        public GraphicsDevice(IntPtr surface_handle, int backbuffer_width, int backbuffer_height)
        {
            this.backbuffer_width = backbuffer_width;
            this.backbuffer_height = backbuffer_height;

            proj_matrix = Matrix4x4.CreateOrthographicOffCenter(
                left: -backbuffer_width / 2,
                right: backbuffer_width / 2,
                bottom: backbuffer_height / 2,
                top: -backbuffer_height / 2,
                zNearPlane: 0.0f,
                zFarPlane: 1.0f);

            proj_matrix_gui = Matrix4x4.CreateOrthographicOffCenter(

                left: 0,
                right: backbuffer_width,
                bottom: backbuffer_height,
                top: 0,
                zNearPlane: 0.0f,
                zFarPlane: 1.0f

            );

            this.shaders_catalog = new Dictionary<string, ShaderProgram>();

            Bgfx.SetPlatformData(new PlatformData() { WindowHandle = surface_handle });

            Bgfx.Init();

            Info = new GraphicsInfo();

            Capabilities caps = Bgfx.GetCaps();

            Info.RendererBackend = caps.Backend;
            Info.MaxTextureSize = caps.MaxTextureSize;

            LoadEmbededShaders();

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

            Bgfx.SetViewClear(0, ClearTargets.Color, 0x171717);
            //Bgfx.SetViewClear(1, ClearTargets.Color, 0x171717);

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            ResizeBackbuffer(backbuffer_width, backbuffer_height);

            InitializeRenderBuffers();

            dummy_texture = LoadEmbededTexture("sprite");
        }

        public void Dispose()
        {
            Console.WriteLine(" > Disposing Graphics Device");

            foreach (var shader in shaders_catalog)
            {
                Console.WriteLine($" >> Disposing Shader: {shader.Key}");

                shader.Value.Dispose();
            }

            shaders_catalog.Clear();

            index_buffer.Dispose();

            dummy_texture.Dispose();

            Bgfx.Shutdown();
        }

        public void Clear(in Color color)
        {
            //Bgfx.SetViewClear(0, ClearTargets.Color, color.RGBAI);
            //Bgfx.SetViewClear(1, ClearTargets.Color, color.RGBAI);
        }

        public void LoadShaderProgram(string name, byte[] vertex_shader_src, byte[] frag_shader_src)
        {
            if (vertex_shader_src.Length == 0 || frag_shader_src.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = new Shader(MemoryBlock.FromArray(vertex_shader_src));
            var frag_shader = new Shader(MemoryBlock.FromArray(frag_shader_src));

            var shader_program = new ShaderProgram(vertex_shader, frag_shader);

            shaders_catalog.Add(name, shader_program);
        }

        public unsafe void AddQuad(Texture2D texture, float x, float y)
        {
            if (current_texture != texture)
            {
                Flush();

                current_texture = texture;
            }

            float w = texture.Width;
            float h = texture.Height;

            var vidx = vertex_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, 0f, 0f, 0xffffffff);
                *(vertex_ptr + vidx++) = new VertexPCT(x+w, y, 1f, 0f, 0xffffffff);
                *(vertex_ptr + vidx++) = new VertexPCT(x+w, y+h, 1f, 1f, 0xffffffff);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y+h, 0f, 1f, 0xffffffff);
            }

            unchecked
            {
                vertex_idx += 4;
                quad_count++;
            }
        }

        public unsafe void Begin()
        {
            var projMatrix = proj_matrix;
            var projMatrixGui = proj_matrix_gui;

            cur_view = 0;

            Bgfx.Touch(cur_view);


            Bgfx.SetRenderState(RenderState.BlendAlpha | RenderState.ColorWrite | RenderState.AlphaWrite);

            Bgfx.SetViewTransform(cur_view, null, &projMatrix.M11);

            Bgfx.SetViewRect(cur_view, 0, 0, backbuffer_width, backbuffer_height);

            dummy_x2 += 5.0f;

            if (dummy_x2 > 300)
            {
                dummy_x2 = 0;
            }

            AddQuad(dummy_texture, dummy_x2, 0);


            Flush();

            cur_view = 1;


            Bgfx.Touch(cur_view);

            Bgfx.SetRenderState(RenderState.BlendAlpha | RenderState.ColorWrite | RenderState.AlphaWrite);

            Bgfx.SetViewTransform(cur_view, null, &projMatrixGui.M11);

            Bgfx.SetViewRect(cur_view, 0, 0, backbuffer_width, backbuffer_height);

            dummy_x += 5.0f;

            if(dummy_x > 300)
            {
                dummy_x = 0;
            }

            AddQuad(dummy_texture, dummy_x, 0);

            Flush();


        }

        public void End()
        {
            Flush();

            Bgfx.Frame();
        }

        public unsafe void ResizeBackbuffer(int width, int height)
        {
            Bgfx.Reset(width, height, ResetFlags.Vsync);
        }

        private unsafe void Flush()
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

            Bgfx.SetVertexBuffer(vertex_buffer, 0, vertex_idx);
            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count * 6);

            Bgfx.Submit(cur_view, default_shader.Program);

            vertex_idx = 0;
            quad_count = 0;

        }



        private unsafe void InitializeRenderBuffers()
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

        private void LoadEmbededShaderProgram(string root_path, string name)
        {
            byte[] vs_file_buffer = { };
            byte[] fs_file_buffer = { };

            var vs_shader_path = root_path + "vs_" + name + ".bin";
            var fs_shader_path = root_path + "fs_" + name + ".bin";

            try
            {
                using (var vs_file_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(vs_shader_path))
                {
                    if (vs_file_stream != null)
                    {
                        vs_file_buffer = new byte[vs_file_stream.Length];

                        vs_file_stream.Read(vs_file_buffer, 0, vs_file_buffer.Length);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load embeded Vertex Shader {vs_shader_path} : {e.Message}");
            }

            try
            {
                using (var fs_file_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fs_shader_path))
                {
                    if (fs_file_stream != null)
                    {
                        fs_file_buffer = new byte[fs_file_stream.Length];

                        fs_file_stream.Read(fs_file_buffer, 0, fs_file_buffer.Length);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load embeded Fragment Shader {fs_shader_path} : {e.Message}");
            }

            LoadShaderProgram(name, vs_file_buffer, fs_file_buffer);
        }

        private void LoadEmbededShaders()
        {
            string shaders_path;

            RendererBackend renderer_backend = Info.RendererBackend;

            switch (renderer_backend)
            {
                case RendererBackend.Direct3D11:
                case RendererBackend.Direct3D12:
                    shaders_path = EMBEDED_SHADERS_PATH + ".hlsl.";
                    break;

                case RendererBackend.OpenGL:
                    shaders_path = EMBEDED_SHADERS_PATH + ".glsl.";
                    break;

                default:
                    throw new InvalidOperationException($"Shaders are unavailable for Renderer Backend: {renderer_backend}");
            }

            LoadEmbededShaderProgram(shaders_path, "base_2d");

            default_shader = shaders_catalog["base_2d"];

            default_shader.AddTextureUniform("texture_2d");
        }

        private Texture2D LoadEmbededTexture(string name)
        {
            try
            {
                ImageReader image_reader = new ImageReader();

                string path = EMBEDED_TEXTURES_PATH + "." + name + ".png";

                using (var tex_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
                {
                    if (tex_stream != null)
                    {
                        var loaded_image = image_reader.Read(tex_stream);

                        var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

                        var texture = new Texture2D(pixmap);

                        pixmap.Dispose();

                        return texture;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load embeded Texture : {e.Message}");
            }

            return null;

        }
    }
}