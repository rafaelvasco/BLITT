using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BLITTEngine.Core.Resources.Loaders;

namespace BLITTEngine.Core.Resources
{
    internal class ResourceLoader
    {
        private readonly Dictionary<string, BaseLoader> _loaders;

        public ResourceLoader()
        {
            _loaders = new Dictionary<string, BaseLoader>();

            _loaders = new Dictionary<string, BaseLoader>
            {
                {typeof(Texture2D).Name, new TextureLoader()},
                {typeof(Song).Name, new SongLoader()},
                {typeof(Effect).Name, new EffectLoader()},
                {typeof(Font).Name, new FontLoader()},
                {typeof(ShaderProgram).Name, new ShaderLoader()}
            };
        }

        public T LoadResource<T>(string resource_name) where T : Resource
        {
            var resource_path = Game.Instance.ContentManager.GetResourceFullPath(resource_name);

            var typeT = typeof(T);

            if (typeT != typeof(ShaderProgram))
            {
                using (var resource_stream = File.OpenRead(resource_path))
                {
                    return (T) _loaders[typeT.Name].Load(resource_stream);
                }
            }
            else
            {
                var paths = resource_path.Split(';');

                using (var resource_stream_vs = File.OpenRead(paths[0]))
                using (var resource_stream_fs = File.OpenRead(paths[1]))
                {
                    return (T) _loaders[typeT.Name].Load(resource_stream_vs, resource_stream_fs);
                }
            }
        }

        public T LoadBuiltin<T>(string resource_name) where T : Resource
        {
            var resource_path = Game.Instance.ContentManager.GetBuiltinResourcePath<T>(resource_name);

            using (var resource_stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource_path))
            {
                return (T) _loaders[typeof(T).Name].Load(resource_stream);
            }
        }

        public T LoadFile<T>(string file_path) where T : Resource
        {
            using (var resource_stream = File.OpenRead(file_path))
            {
                return (T) _loaders[typeof(T).Name].Load(resource_stream);
            }
        }
    }
}