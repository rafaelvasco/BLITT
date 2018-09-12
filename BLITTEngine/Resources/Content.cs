using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLITTEngine.Foundation;
using BLITTEngine.Graphics;

namespace BLITTEngine.Resources
{
    public static class Content
    {
        private static string content_path;

        private static Dictionary<string, Resource> loaded_assets;
        private static List<Resource> runtime_assets;

        internal static void Init(string path)
        {
            content_path = path;
            loaded_assets = new Dictionary<string, Resource>();
            runtime_assets = new List<Resource>();
        }
        
        internal static void UnloadAll()
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
        
        public static T Get<T>(string asset_id) where T : Resource
        {
            if (loaded_assets.TryGetValue(asset_id, out var asset))
            {
                return (T) asset;
            }

            var type = typeof(T);

            if (type == typeof(Image))
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
                        
                        var pixmap = new Pixmap(bitmap.PixelData, bitmap.Width, bitmap.Height);

                        var texture = Game.Platform.Graphics.CreateTexture(pixmap, is_render_target: false);
                        
                        var image = (T)Activator.CreateInstance(type, pixmap, texture);

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

        public static Image CreateImage(int width, int height, bool is_draw_target=false)
        {
            var pixmap = new Pixmap(width, height);
            var texture = Game.Platform.Graphics.CreateTexture(width, height, is_draw_target);
            var image = new Image(pixmap, texture);
            
            Register(image);
            
            return image;
        }
        
        internal static void Register(Resource resource) 
        {
            runtime_assets.Add(resource);
        }
    }
}