using System.IO;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class TextureLoader : BaseLoader
    {
        private ImageReader image_reader;

        public override Resource Load(Stream file_stream)
        {
            if (image_reader == null)
            {
                image_reader = new ImageReader();
            }

            Image loaded_image = image_reader.Read(file_stream);

            var pixmap = new Pixmap(loaded_image.Data, loaded_image.Width, loaded_image.Height);

            Texture2D texture = Game.Instance.GraphicsContext.CreateTexture(pixmap);

            pixmap.Dispose();

            return texture;
        }
    }
}