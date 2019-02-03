using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Graphics;
using Utf8Json;

namespace BLITTEngine.Core.Resources
{
    public class ContentManager
    {
        private readonly Dictionary<string, Resource> _builtin_resources;

        private readonly Dictionary<string, Resource> _loaded_resources;
        private readonly ResourceLoader _loader;
        private readonly List<Resource> _runtime_resources;

        public ContentManager(string root_path)
        {
            RootPath = root_path;

            _loaded_resources = new Dictionary<string, Resource>();
            _builtin_resources = new Dictionary<string, Resource>();
            _runtime_resources = new List<Resource>();
            _loader = new ResourceLoader();

            LoadResourcesManifest();
        }

        public string RootPath { get; }

        internal string EmbededPath { get; } = "BLITTEngine.EngineResources";

        internal ResourcesManifest Manifest { get; private set; }

        private void LoadResourcesManifest()
        {
            var path = Path.Combine(RootPath, "assets.json");

            using (var stream = File.OpenRead(path))
            {
                Manifest = JsonSerializer.Deserialize<ResourcesManifest>(stream);
            }
        }

        public T Get<T>(string resource_name) where T : Resource
        {
            if (_loaded_resources.TryGetValue(resource_name, out var already_loaded)) return (T) already_loaded;

            Resource resource = _loader.LoadResource<T>(resource_name);

            resource.Name = resource_name;

            _loaded_resources.Add(resource_name, resource);

            return (T) resource;
        }

        public T GetBuiltin<T>(string resource_name) where T : Resource
        {
            if (_builtin_resources.TryGetValue(resource_name, out var builtin_resource)) return (T) builtin_resource;

            Resource resource = _loader.LoadBuiltin<T>(resource_name);

            resource.Name = resource_name;

            _builtin_resources.Add(resource_name, resource);

            return (T) resource;
        }

        public T GetFromFile<T>(string file_path) where T : Resource
        {
            var file_name = Path.GetFileNameWithoutExtension(file_path);

            if (_loaded_resources.TryGetValue(file_name ?? throw new Exception("Invalid File Path: " + file_path),
                out var already_loaded)) return (T) already_loaded;

            Resource resource = _loader.LoadFile<T>(file_path);

            resource.Name = file_name;

            _loaded_resources.Add(file_name, resource);

            return (T) resource;
        }

        public Pixmap CreatePixmap(byte[] data, int width, int height)
        {
            var pixmap = new Pixmap(data, width, height);

            RegisterRuntimeLoaded(pixmap);

            return pixmap;
        }

        public Pixmap CreatePixmap(int width, int height)
        {
            var pixmap = new Pixmap(width, height);

            RegisterRuntimeLoaded(pixmap);

            return pixmap;
        }

        public Texture2D CreateTexture(Pixmap pixmap)
        {
            var texture = Game.Instance.GraphicsContext.CreateTexture(pixmap);

            RegisterRuntimeLoaded(texture);

            return texture;
        }

        public Texture2D CreateTexture(int width, int height, Color color)
        {
            var pixmap = new Pixmap(width, height);

            return CreateTexture(pixmap);
        }

        public RenderTarget CreateRenderTarget(int width, int height)
        {
            var render_target = Game.Instance.GraphicsContext.CreateRenderTarget(width, height);

            RegisterRuntimeLoaded(render_target);

            return render_target;
        }

        internal string GetResourceFullPath(string resource_name)
        {
            return Manifest.Resources[resource_name];
        }

        internal string GetBuiltinResourcePath<T>(string resource_name) where T : Resource
        {
            var typeT = typeof(T);

            var sb = new StringBuilder(EmbededPath);

            if (typeT == typeof(Texture2D))
            {
                return sb.Append(".Images.").Append(resource_name).ToString();
            }
            else if (typeT == typeof(ShaderProgram))
            {
                sb.Append(".Shaders.bin");

                switch (Game.Instance.GraphicsContext.Info.RendererBackend)
                {
                    case RendererBackend.Direct3D9:
                    case RendererBackend.Direct3D11:
                    case RendererBackend.Direct3D12:

                        sb.Append(".hlsl.");

                        break;

                    case RendererBackend.OpenGL:

                        sb.Append(".glsl.");

                        break;

                    default:
                        throw new ArgumentOutOfRangeException("No builtin shaders defined for backend: " +
                                                              Game.Instance.GraphicsContext.Info.RendererBackend);
                }

                return sb.Append(resource_name).ToString();
            }
            else if (typeT == typeof(Effect))
            {
                return sb.Append(".Effects.").Append(resource_name).ToString();
            }
            else if (typeT == typeof(Song))
            {
                return sb.Append(".Songs.").Append(resource_name).ToString();
            }

            return null;
        }

        internal void RegisterRuntimeLoaded(Resource resource)
        {
            _runtime_resources.Add(resource);
        }

        internal void FreeEverything()
        {
            Console.WriteLine($" > Diposing {_loaded_resources.Count} loaded resources.");

            foreach (var resource in _loaded_resources) resource.Value.Dispose();

            Console.WriteLine($" > Disposing {_runtime_resources.Count} runtime resources.");

            foreach (var resource in _runtime_resources) resource.Dispose();

            Console.WriteLine($" > Disposing {_builtin_resources.Count} builtin resources.");

            foreach (var resource in _builtin_resources) resource.Value.Dispose();

            _loaded_resources.Clear();
            _runtime_resources.Clear();
            _builtin_resources.Clear();
        }
    }
}