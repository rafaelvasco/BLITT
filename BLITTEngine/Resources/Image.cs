using BLITTEngine.Foundation;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Resources
{
    

   

    public class Image : Resource, BlitSource
    {


        private RectangleI bounding_rect;
        private ImageWrapMode wrap_mode;
        private ImageFilterMode filter_mode;
        
        internal Texture Texture { get; }
        internal Pixmap Pixmap { get; }

        internal bool DataChanged { get; set; }
        internal bool ConfigChanged { get; set; }

        public int Width => Pixmap.Width;
        public int Height => Pixmap.Height;

        public ImageWrapMode WrapMode
        {
            get => wrap_mode;
            set
            {
                if (wrap_mode != value)
                {
                    wrap_mode = value;
                    ConfigChanged = true;
                }
            }
        }

        public ImageFilterMode FilterMode
        {
            get => filter_mode;
            set
            {
                if(filter_mode != value)
                {
                    filter_mode = value;
                    ConfigChanged = true;
                }
            }
        }

            
        public Image(Pixmap pixmap, Texture texture)
        {
            this.Texture = texture;
            this.Pixmap = pixmap;
            
            bounding_rect = new RectangleI(0, 0, Width, Height);
            wrap_mode = ImageWrapMode.None;
            filter_mode = ImageFilterMode.Crisp;
        }

        public void Fill(Color color)
        {
            var pixel_data = this.Pixmap.PixelData;

            int length = pixel_data.Length - 4;
            
            for (int i = 0; i < length; i+=4)
            {
                pixel_data[i+0] = color.R;
                pixel_data[i+1] = color.G;
                pixel_data[i+2] = color.B;
                pixel_data[i+3] = color.A;
            }

            DataChanged = true;
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

            DataChanged = true;
        }
        
        public void SaveToFile(string file)
        {
            var pixmapBytes = Pixmap.GetRgbaBytes();

            ImageSaver.Save(pixmapBytes, this.Width, this.Height, file);
        }

        internal override void Dispose()
        {
            Pixmap.Dispose();
        }

        public ref RectangleI this[int index] => ref bounding_rect;

        public Image SourceImage => this;
    }
}