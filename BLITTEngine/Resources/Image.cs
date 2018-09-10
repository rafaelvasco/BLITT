using BLITTEngine.Foundation;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Resources
{
    public class Image : Resource, BlitSource
    {
        private RectangleI bounding_rect;
        
        internal Texture Texture { get; }
        internal Pixmap Pixmap { get; }
        
        internal bool Invalidated { get; set; }

        public int Width => Pixmap.Width;
        public int Height => Pixmap.Height;
            
        public Image(Pixmap pixmap, Texture texture)
        {
            this.Texture = texture;
            this.Pixmap = pixmap;
            
            bounding_rect = new RectangleI(0, 0, Width, Height);
        }

        public void BlitImage(Image image, int x, int y)
        {
            var w = image.Width;
            var h = image.Height;
            var srcData = image.Pixmap.PixelData;
            var targetData = this.Pixmap.PixelData;

            for (var i = 0; i < w; ++i)
            {
                for (var j = 0; j < h; ++j)
                {
                    var idx = (i + j * w) * 4;

                    var a = srcData[idx + 3];

                    if (a != 255)
                    {
                        continue;
                    }

                    var r = srcData[idx + 2];
                    var g = srcData[idx + 1];
                    var b = srcData[idx];

                    var targetIdx = ((i + x) + (j + y) * this.Width) * 4;

                    targetData[targetIdx] = b;
                    targetData[targetIdx + 1] = g;
                    targetData[targetIdx + 2] = r;
                }
            }

            Invalidated = true;
        }
        
        public void SaveToFile(string file)
        {
            var pixmapBytes = Pixmap.GetRgbaBytes();

            ImageSaver.Save(pixmapBytes, this.Width, this.Height, file);
        }

        internal override void Dispose()
        {
            Texture.Dispose();
            Pixmap.Dispose();
        }

        public ref RectangleI this[int index] => ref bounding_rect;

        public Image SourceImage => this;
    }
}