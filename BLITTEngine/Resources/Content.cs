using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Foundation.STB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BLITTEngine.Resources
{
    public class Content
    {
        private const string EMBEDED_SHADERS_PATH = "BLITTEngine.EngineResources.Shaders.bin";
        private const string EMBEDED_TEXTURES_PATH = "BLITTEngine.EngineResources.Images";

        private string content_path;

        private Dictionary<string, Resource> loaded_assets;
        private List<Resource> runtime_assets;
        private Dictionary<string, ShaderProgram> builtin_shaders;
        private Dictionary<string, Texture2D> builtin_textures;
        private ImageReader image_reader;

        private List<string> embeded_shaders_names = new List<string>
        {
            "base_2d"
        };

        private List<string> embeded_textures_names = new List<string>
        {
            "sprite"
        };

        internal Content(string root_path)
        {
            content_path = root_path;
            loaded_assets = new Dictionary<string, Resource>();
            runtime_assets = new List<Resource>();
            builtin_shaders = new Dictionary<string, ShaderProgram>();
            builtin_textures = new Dictionary<string, Texture2D>();
            image_reader = new ImageReader();
        }

        public Texture2D GetTexture2D(string asset_name)
        {
            if (loaded_assets.TryGetValue(asset_name, out var asset))
            {
                return (Texture2D)asset;
            }

            var id = new StringBuilder(asset_name);

            if (!asset_name.Contains(".png"))
            {
                id.Append(".png");
            }

            string path = Path.Combine(content_path, id.ToString());

            try
            {
                using (var stream = File.OpenRead(path))
                {
                    var loaded_image = image_reader.Read(stream);

                    var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

                    var texture = new Texture2D(pixmap);

                    pixmap.Dispose();

                    var key = Path.GetFileNameWithoutExtension(path);

                    loaded_assets.Add(key, texture);

                    return texture;
                }
            }
            catch (FileNotFoundException e)
            {
                throw new Exception(e.Message);
            }
        }

        //public static Font GetFont(string asset_name)
        //{
        //    if (loaded_assets.TryGetValue(asset_name, out var asset))
        //    {
        //        return (Font)asset;
        //    }

        //    var id = new StringBuilder(asset_name);

        //    if (!asset_name.Contains(".png"))
        //    {
        //        id.Append(".png");
        //    }

        //    string path = Path.Combine(content_path, id.ToString());

        //    try
        //    {
        //        using (var stream = File.OpenRead(path))
        //        {
        //            var loaded_image = image_reader.Read(stream);

        //            var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

        //            var font = new Font(pixmap);

        //            var key = Path.GetFileNameWithoutExtension(path);

        //            loaded_assets.Add(key, font);

        //            return font;
        //        }
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public ShaderProgram GetBuiltinShader(string name)
        {
            return builtin_shaders[name];
        }

        public Texture2D GetBuiltinTexture(string name)
        {
            return builtin_textures[name];
        }

        public Pixmap CreatePixmap(byte[] data, int width, int height)
        {
            var pixmap = new Pixmap(data, width, height);

            Register(pixmap);

            return pixmap;
        }

        public Pixmap CreatePixmap(int width, int height)
        {
            var pixmap = new Pixmap(width, height);

            Register(pixmap);

            return pixmap;
        }

        public Texture2D CreateTexture(Pixmap pixmap)
        {
            var texture = new Texture2D(pixmap);

            Register(texture);

            return texture;
        }

        public Texture2D CreateTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(width, height);

            var pixmap = new Pixmap(width, height);

            pixmap.Fill(color);

            texture.SetData(pixmap);

            pixmap.Dispose();

            Register(texture);

            return texture;
        }

        internal void Register(Resource resource)
        {
            runtime_assets.Add(resource);
        }

        internal void Free()
        {
            Console.WriteLine($" > Diposing {loaded_assets.Count} loaded assets.");

            foreach (var asset in loaded_assets)
            {
                asset.Value.Dispose();
            }

            Console.WriteLine($" > Disposing {runtime_assets.Count} runtime assets.");

            foreach (var asset in runtime_assets)
            {
                asset.Dispose();
            }

            Console.WriteLine($" > Disposing {builtin_shaders.Keys.Count} embeded shaders. ");

            foreach (var shader in builtin_shaders)
            {
                shader.Value.Dispose();
            }

            Console.WriteLine($" > Disposing {builtin_textures.Keys.Count} embeded textures. ");

            foreach (var texture in builtin_textures)
            {
                texture.Value.Dispose();
            }

            loaded_assets.Clear();
            runtime_assets.Clear();
            builtin_shaders.Clear();
            builtin_textures.Clear();
        }

        internal void LoadEmbededShaders(RendererBackend renderer_backend)
        {
            string shaders_path;

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

            foreach (var shader_name in embeded_shaders_names)
            {
                LoadEmbededShaderProgram(shaders_path, shader_name);
            }
        }

        internal void LoadEmbededTextures()
        {
            try
            {
                ImageReader image_reader = new ImageReader();

                foreach (var tex_name in embeded_textures_names)
                {
                    string path = EMBEDED_TEXTURES_PATH + "." + tex_name + ".png";

                    using (var tex_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
                    {
                        if (tex_stream != null)
                        {
                            var loaded_image = image_reader.Read(tex_stream);

                            var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

                            var texture = new Texture2D(pixmap);

                            pixmap.Dispose();
                            builtin_textures.Add(tex_name, texture);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load embeded Texture : {e.Message}");
            }
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

            var shader_program = Renderer2D.CreateShaderProgram(name, vs_file_buffer, fs_file_buffer);

            builtin_shaders.Add(name, shader_program);
        }
    }
}