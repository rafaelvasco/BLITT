using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLITTEngine.Foundation;
using BLITTEngine.Platform;

namespace BLITTEngine.Resources
{
    public class Content
    {
        private static string content_path;
        private readonly BLITTCore core;

        private static Dictionary<string, Resource> loaded_assets;
        private static List<Resource> runtime_assets;

        internal Content(BLITTCore core, string root_path)
        {
            this.core = core;
            content_path = root_path;
            loaded_assets = new Dictionary<string, Resource>();
            runtime_assets = new List<Resource>();
        }
        
        internal void UnloadAll()
        {
            Console.WriteLine($"Destroying {loaded_assets.Count} loaded assets.");
            
            foreach (var asset in loaded_assets)
            {
                asset.Value.Dispose();
            }
            
            Console.WriteLine($"Destroying {runtime_assets.Count} runtime assets.");

            foreach (var asset in runtime_assets)
            {
                asset.Dispose();
            }

            loaded_assets.Clear();
            runtime_assets.Clear();
        }
        
        public T Get<T>(string asset_id) where T : Resource
        {
            if (loaded_assets.TryGetValue(asset_id, out var asset))
            {
                return (T) asset;
            }

            var type = typeof(T);

            if (type == typeof(Pixmap))
            {
                var id = new StringBuilder(asset_id);
                
                if (!asset_id.Contains(".png"))
                {
                    id.Append(".png");
                }

                string path = Path.Combine(content_path, id.ToString());

                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        var bitmap = ImageLoader.LoadFile(stream);
                        
                        var texture = core.Graphics.AddTexture(bitmap.)
                        
                        var image = (T)Activator.CreateInstance(bitmap.PixelData, bitmap.Width, bitmap.Height);

                        var key = Path.GetFileNameWithoutExtension(path);
                        
                        loaded_assets.Add(key, image);

                        return image;

                    }
                }
                catch (FileNotFoundException e)
                {
                    throw new Exception(e.Message);
                }

            }
            
            return null;
        }

        public Pixmap CreateImage(int width, int height, bool is_draw_target=false)
        {
            var pixmap = new Pixmap(width, height);

           

            var image = new Pixmap(pixmap, texture);
            
            Register(image);
            
            return image;
        }
        
        internal void Register(Resource resource) 
        {
            runtime_assets.Add(resource);
        }
    }
}