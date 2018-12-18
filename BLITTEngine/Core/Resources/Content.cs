using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Resources
{
    public static class Content
    {
        private const string EMBEDED_SHADERS_PATH = "BLITTEngine.EngineResources.Shaders.bin";

        internal static GraphicsContext GraphicsContext;

        private static string content_path;

        private static Dictionary<string, Resource> loaded_assets;
        private static List<Resource> runtime_assets;
        private static Dictionary<string, ShaderProgram> builtin_shaders;
        private static ImageReader image_reader;

        private static readonly string[] embeded_shader_names = { "base_2d" };

        internal static void Init(string root_path)
        {
            content_path = root_path;
            loaded_assets = new Dictionary<string, Resource>();
            runtime_assets = new List<Resource>();
            builtin_shaders = new Dictionary<string, ShaderProgram>();
            image_reader = new ImageReader();
        }

        public static Texture2D GetTexture2D(string asset_name)
        {
            if (loaded_assets.TryGetValue(asset_name, out Resource asset))
            {
                return (Texture2D) asset;
            }

            var id = new StringBuilder(asset_name);

            if (!asset_name.Contains(".png"))
            {
                id.Append(".png");
            }

            string path = Path.Combine(content_path, id.ToString());

            try
            {
                using (FileStream stream = File.OpenRead(path))
                {
                    Image loaded_image = image_reader.Read(stream);

                    var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

                    Texture2D texture = GraphicsContext.CreateTexture(pixmap.PixelData, pixmap.Width, pixmap.Height);

                    pixmap.Dispose();

                    string key = Path.GetFileNameWithoutExtension(path);

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

        public static Effect GetEffect(string asset_name)
        {
            if (loaded_assets.TryGetValue(asset_name, out Resource asset))
            {
                return (Effect) asset;
            }

            var id = new StringBuilder(asset_name);

            if (!asset_name.Contains(".wav"))
            {
                id.Append(".wav");
            }

            string path = Path.Combine(content_path, id.ToString());

            var effect = MediaPlayer.LoadEffect(path);

            loaded_assets.Add(asset_name, effect);

            return effect;
        }

        public static Song GetSong(string asset_name)
        {
            if (loaded_assets.TryGetValue(asset_name, out Resource asset))
            {
                return (Song) asset;
            }

            var id = new StringBuilder(asset_name);

            if (!asset_name.Contains(".ogg"))
            {
                id.Append(".ogg");
            }

            string path = Path.Combine(content_path, id.ToString());

            var song = MediaPlayer.LoadSong(path);

            loaded_assets.Add(asset_name, song);

            return song;
        }

        public static ShaderProgram GetBuiltinShader(string name)
        {
            return builtin_shaders[name];
        }

        public static Pixmap CreatePixmap(byte[] data, int width, int height)
        {
            var pixmap = new Pixmap(data, width, height);

            Register(pixmap);

            return pixmap;
        }

        public static Pixmap CreatePixmap(int width, int height)
        {
            var pixmap = new Pixmap(width, height);

            Register(pixmap);

            return pixmap;
        }

        public static Texture2D CreateTexture(Pixmap pixmap)
        {
            Texture2D texture = GraphicsContext.CreateTexture(pixmap.PixelData, pixmap.Width, pixmap.Height);

            Register(texture);

            return texture;
        }

        public static Texture2D CreateTexture(int width, int height, Color color)
        {
            var pixmap = new Pixmap(width, height);

            return CreateTexture(pixmap);
        }

        public static RenderTarget CreateRenderTarget(int width, int height)
        {
            RenderTarget render_target = GraphicsContext.CreateRenderTarget(width, height);

            Register(render_target);

            return render_target;
        }

        internal static void Register(Resource resource)
        {
            runtime_assets.Add(resource);
        }

        internal static void FreeEverything()
        {
            Console.WriteLine($" > Diposing {loaded_assets.Count} loaded assets.");

            foreach (KeyValuePair<string, Resource> asset in loaded_assets)
            {
                asset.Value.Dispose();
            }

            Console.WriteLine($" > Disposing {runtime_assets.Count} runtime assets.");

            foreach (Resource asset in runtime_assets)
            {
                asset.Dispose();
            }

            Console.WriteLine($" > Disposing {builtin_shaders.Count} builtin shaders.");

            foreach (KeyValuePair<string, ShaderProgram> shader_program in builtin_shaders)
            {
                shader_program.Value.Dispose();
            }

            loaded_assets.Clear();
            runtime_assets.Clear();
            builtin_shaders.Clear();
        }

        internal static void LoadEmbededShaders(RendererBackend renderer_backend)
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

            for (var i = 0; i < embeded_shader_names.Length; i++)
            {
                LoadEmbededShaderProgram(shaders_path, embeded_shader_names[i]);
            }
        }

        internal static List<string> LoadEmbededTextFile(string fileName)
        {
            const string EMBEDDED_RESOURCES_NAMESPACE = "BLITTEngine.EngineResources.TextFiles.";

            var lines = new List<string>();

            using (var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(EMBEDDED_RESOURCES_NAMESPACE + fileName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null && !String.IsNullOrWhiteSpace(line))
                        {
                            lines.Add(line);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            return lines;
        }

        private static void LoadEmbededShaderProgram(string root_path, string name)
        {
            byte[] vs_file_buffer = { };
            byte[] fs_file_buffer = { };

            string vs_shader_path = root_path + "vs_" + name + ".bin";
            string fs_shader_path = root_path + "fs_" + name + ".bin";

            try
            {
                using (Stream vs_file_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(vs_shader_path))
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
                using (Stream fs_file_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fs_shader_path))
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

            ShaderProgram shader_program = GraphicsContext.CreateShader(vs_file_buffer, fs_file_buffer);

            builtin_shaders.Add(name, shader_program);

        }
    }
}