using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Foundation.STB;
using BLITTEngine.Numerics;

namespace BLITTEngine.Resources
{
    public class Content
    {
        private static string content_path;

        private static Dictionary<string, Resource> loaded_assets;
        private static List<Resource> runtime_assets;
        private static ImageReader image_reader;

        internal Content(string root_path)
        {
            content_path = root_path;
            loaded_assets = new Dictionary<string, Resource>();
            runtime_assets = new List<Resource>();
            image_reader = new ImageReader();
        }

        internal void UnloadAll()
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

            loaded_assets.Clear();
            runtime_assets.Clear();
        }

        public Texture2D GetTexture2D(string asset_name)
        {
            if (loaded_assets.TryGetValue(asset_name, out var asset))
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

    }
}