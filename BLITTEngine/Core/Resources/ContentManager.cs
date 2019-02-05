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
        
        private readonly List<Resource> _runtime_resources;
        
        private readonly ResourceLoader _loader;

        public ContentManager(string root_path)
        {
            RootPath = root_path;

            _loaded_resources = new Dictionary<string, Resource>();
            _builtin_resources = new Dictionary<string, Resource>();
            _runtime_resources = new List<Resource>();
            _loader = new ResourceLoader();
        }

        public string RootPath { get; }

        public T Get<T>(string resource_id) where T : Resource
        {
            if (_loaded_resources.TryGetValue(resource_id, out Resource resource))
            {
                return (T) resource;
            }
            
            throw new Exception("Can't find resource with ID: " + resource_id);
        }

        public void LoadFromPak(string pak_name)
        {
            ResourcePak pak = _loader.LoadPak(pak_name);
            
            // Process Images

            foreach (var image_res in pak.Images)
            {
                Texture2D texture = _loader.LoadTexture(image_res.Value);
                
                _loaded_resources.Add(texture.Id, texture);
            }
            
            // Process Fonts

            foreach (var font_res in pak.Fonts)
            {
                Font font = _loader.LoadFont(font_res.Value);
                
                _loaded_resources.Add(font.Id, font);
            }
            
            // Process Shaders

            foreach (var shader_res in pak.Shaders)
            {
                ShaderProgram shader = _loader.LoadShader(shader_res.Value);
                
                _loaded_resources.Add(shader.Id, shader);
            }
            
            // Process Effects

            foreach (var sfx_res in pak.Sfx)
            {
                Effect effect = _loader.LoadEffect(sfx_res.Value);
                
                _loaded_resources.Add(effect.Id, effect);
            }
            
            // Process Songs

            foreach (var song_res in pak.Songs)
            {
                Song song = _loader.LoadSong(song_res.Value);
                
                _loaded_resources.Add(song.Id, song);
            }
            
            // Process Text Files

            foreach (var txt_res in pak.TextFiles)
            {
                TextFile text_file = _loader.LoadTextFile(txt_res.Value);
                
                _loaded_resources.Add(text_file.Id, text_file);
            }
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