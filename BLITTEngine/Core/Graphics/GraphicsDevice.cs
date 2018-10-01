using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using BLITTEngine.Foundation;

namespace BLITTEngine.Core.Graphics
{
    public class GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;
    }
    
    public class GraphicsDevice : IDisposable
    {
        private const string EMBEDED_SHADERS_PATH = "BLITTEngine.EngineResources.Shaders.bin";

        public GraphicsInfo Info { get; }

        private const int MAX_QUADS = 4096;
        
        private ShaderProgram default_shader;
        private Dictionary<string, ShaderProgram> shaders_catalog;
        private Texture2D current_texture;
        private VertexPCT[] quad_vertices;
        private ushort[] quad_indices;
        private IndexBuffer index_buffer;
        private int vertex_idx;
        private int quad_count;

        private Matrix4x4 projection_matrix;
        private Matrix4x4 transform_matrix;
        private int backbuffer_width;
        private int backbuffer_height;
        
        
        public GraphicsDevice(IntPtr surface_handle, int backbuffer_width, int backbuffer_height)
        {
            this.backbuffer_width = backbuffer_width;
            this.backbuffer_height = backbuffer_height;
            this.shaders_catalog = new Dictionary<string, ShaderProgram>();
            
            Bgfx.SetPlatformData(new PlatformData() { WindowHandle = surface_handle});
            Bgfx.Init(new InitSettings()
            {
                Backend = RendererBackend.Default,
                Adapter = Adapter.Default,
                Debug = false,
                Width = backbuffer_width, 
                Height = backbuffer_height,
                Profiling = false,
                ResetFlags = ResetFlags.Vsync
            });

            Info = new GraphicsInfo();
            
            Capabilities caps = Bgfx.GetCaps();

            Info.RendererBackend = caps.Backend;
            Info.MaxTextureSize = caps.MaxTextureSize;

            Console.WriteLine($"GRAPHICS DEVICE : {Info.RendererBackend}");
            
            LoadEmbededShaders();

            InitializeState();
        }

        private unsafe void InitializeState()
        {
            Bgfx.SetViewRect(0, 0, 0, backbuffer_width, backbuffer_height);
            Bgfx.SetRenderState(
                
                RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInverseSourceAlpha)
            );
            
            transform_matrix = Matrix4x4.Identity;

            projection_matrix = Matrix4x4.CreateOrthographicOffCenter(
                
                left: 0.0f,
                right: backbuffer_width,
                bottom: backbuffer_height,
                top: 0.0f,
                0.0f, 
                1.0f
            );
            
            var viewMatrix = transform_matrix;
            var projMatrix = projection_matrix;
            
            Bgfx.SetViewTransform(0, &viewMatrix.M11, &projMatrix.M11);
            
            quad_vertices = new VertexPCT[MAX_QUADS*4];
            
            quad_indices = new ushort[MAX_QUADS*6];
            
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
                throw new Exception($"Failed to load embed Vertex Shader {vs_shader_path} : {e.Message}");
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
                throw new Exception($"Failed to load embed Fragment Shader {fs_shader_path} : {e.Message}");
            }
            
            LoadShaderProgram(name, vs_file_buffer, fs_file_buffer);
        }

        private void LoadEmbededShaders()
        {
            string shaders_path;

            RendererBackend renderer_backend = Game.Instance.GraphicsDevice.Info.RendererBackend;

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

        }

        public unsafe void AddQuad(float x, float y, float w, float h)
        {
            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr+0) = new VertexPCT(x, y, 0f, 0f, 0xffffffff);
                *(vertex_ptr+1) = new VertexPCT(x+w, y, 1f, 0f, 0xffffffff);
                *(vertex_ptr+2) = new VertexPCT(x+w, y+h, 1f, 1f, 0xffffffff);
                *(vertex_ptr+3) = new VertexPCT(x, y+h, 0f, 1f, 0xffffffff);
            }

            unchecked
            {
                vertex_idx += 1;
                quad_count += 1;    
            }
        }

        public void SetTexture(Texture2D texture)
        {
            current_texture = texture;
        }

        public void Begin()
        {
           Bgfx.SetViewClear(0, ClearTargets.Color, 0x171717ff);
        }

        public unsafe void Submit()
        {
            if (vertex_idx == 0)
            {
                return;
            }
            
            var vertex_buffer = new TransientVertexBuffer(vertex_idx, VertexPCT.Layout);
            
            fixed (void* v = &quad_vertices[0])
            {
                Unsafe.CopyBlock((void*) vertex_buffer.Data, v, (uint)quad_vertices.Length);
            }
            
            default_shader.SetTexture(current_texture, "texture_2d");
            
            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, vertex_idx);
            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count*6);

            Bgfx.Submit(0, default_shader.Program);

        }

        public void Dispose()
        {
            foreach(var shader in shaders_catalog)
            {
                shader.Value.Dispose();
            }

            shaders_catalog.Clear();

            index_buffer.Dispose();
            
        }
    }
}