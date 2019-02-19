using System;
using System.Collections.Generic;
using System.IO;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Resources
{
    public class ContentManager
    {
        private readonly Dictionary<string, Resource> _loaded_resources;
        
        private readonly List<Resource> _runtime_resources;
        
        private readonly ResourceLoader _loader;

        public ContentManager()
        {
            _loaded_resources = new Dictionary<string, Resource>();
            _runtime_resources = new List<Resource>();
            _loader = new ResourceLoader();
            
            LoadContentPack("base");
        }

        public T Get<T>(string resource_id) where T : Resource
        {
            if (_loaded_resources.TryGetValue(resource_id, out Resource resource))
            {
                return (T) resource;
            }
            
            throw new Exception("Can't find resource with ID: " + resource_id);
        }

        public Texture2D LoadTexture(string texture_path) 
        {
            var full_path = Path.Combine("Content", texture_path);
            
            var id = Path.GetFileNameWithoutExtension(texture_path);
            
            if (_loaded_resources.TryGetValue(id, out Resource res))
            {
                return (Texture2D) res;
            }

            Texture2D texture = _loader.LoadTexture(full_path);

            _loaded_resources.Add(texture.Id, texture);

            return texture;
        }
        
        public ShaderProgram LoadShader(string vs_path, string fs_path) 
        {
            var vs_full_path = Path.Combine("Content", vs_path);
            var fs_full_path = Path.Combine("Content", fs_path);
            
            var id = Path.GetFileNameWithoutExtension(vs_path);
            
            if (_loaded_resources.TryGetValue(id, out Resource res))
            {
                return (ShaderProgram) res;
            }

            ShaderProgram shader = _loader.LoadShader(vs_full_path, fs_full_path);

            _loaded_resources.Add(shader.Id, shader);

            return shader;
        }

        public Font LoadFont(string font_path)
        {
            var full_path = Path.Combine("Content", font_path);
            
            var id = Path.GetFileNameWithoutExtension(font_path);
            
            if (_loaded_resources.TryGetValue(id, out Resource res))
            {
                return (Font) res;
            }

            Font font = _loader.LoadFont(full_path);

            _loaded_resources.Add(font.Id, font);

            return font;
        }

        public Effect LoadSfx(string sfx_path)
        {
            var full_path = Path.Combine("Content", sfx_path);
            
            var id = Path.GetFileNameWithoutExtension(sfx_path);
            
            if (_loaded_resources.TryGetValue(id, out Resource res))
            {
                return (Effect) res;
            }

            Effect sfx = _loader.LoadEffect(full_path);

            _loaded_resources.Add(sfx.Id, sfx);

            return sfx;
        }

        public Song LoadSong(string song_path)
        {
            var full_path = Path.Combine("Content", song_path);
            
            var id = Path.GetFileNameWithoutExtension(song_path);
            
            if (_loaded_resources.TryGetValue(id, out Resource res))
            {
                return (Song) res;
            }

            Song song = _loader.LoadSong(full_path);

            _loaded_resources.Add(song.Id, song);

            return song;
        }
        
        public void LoadContentPack(string pak_name)
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
            var pixmap = new Pixmap(data, width, height)
            {
                Id = $"Pixmap [{width},{height}]"
            };

            RegisterRuntimeLoaded(pixmap);

            return pixmap;
        }

        public Pixmap CreatePixmap(int width, int height)
        {
            var pixmap = new Pixmap(width, height) {Id = $"Pixmap [{width},{height}]"};

            RegisterRuntimeLoaded(pixmap);

            return pixmap;
        }

        public Texture2D CreateTexture(Pixmap pixmap)
        {
            var texture = Game.Instance.GraphicsContext.CreateTexture(pixmap);

            texture.Id = $"Pixmap [{texture.Width},{texture.Height}]";
            
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

            render_target.Id = $"Render Target: [{width}, {height}]";

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

            foreach (var resource in _loaded_resources)
            {
                Console.WriteLine($" > Diposing {resource.Key}.");
                resource.Value.Dispose();
            }

            Console.WriteLine($" > Disposing {_runtime_resources.Count} runtime resources.");

            foreach (var resource in _runtime_resources)
            {
                Console.WriteLine($" > Diposing {resource.Id}.");
                resource.Dispose();
            }

            _loaded_resources.Clear();
            _runtime_resources.Clear();
        }
    }
}