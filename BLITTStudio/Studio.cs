using BLITTEngine;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Resources;
using System.IO;
using System.Linq;

namespace BLITTStudio
{
    public class Studio : Scene
    {
        private Pixmap font_pixmap;
        private Texture2D font_texture;

        private const int TEX_W = 512;
        private const int TEX_H = 512;
        private const int FONT_SIZE = 16;


        public override void Init()
        {
            var ttf_bytes = File.ReadAllBytes(@"Assets\chronotype.ttf");

            var bake_bytes = new byte[TEX_W*TEX_H];

            var font_baker = new FontBaker();

            font_baker.Begin(bake_bytes, TEX_W, TEX_H);

            font_baker.Add(ttf_bytes, FONT_SIZE, new []{
                FontBakerCharacterRange.BasicLatin
            });

            var char_data = font_baker.End();

            float minimumOffsetY = 10000;
			foreach (var pair in char_data)
			{
				if (pair.Value.yoff < minimumOffsetY)
				{
					minimumOffsetY = pair.Value.yoff;
				}
			}

            var keys = char_data.Keys.ToArray();
			foreach (var key in keys)
			{
				var pc = char_data[key];
				pc.yoff -= minimumOffsetY;
				char_data[key] = pc;
			}

            var font_pixmap_bytes = new byte[TEX_W*TEX_H*4];

            int col_idx = 0;

            for(var i = 0; i < bake_bytes.Length; ++i)
            {
                var b = bake_bytes[i];

                font_pixmap_bytes[col_idx] = b;
                font_pixmap_bytes[col_idx+1] = b;
                font_pixmap_bytes[col_idx+2] = b;
                font_pixmap_bytes[col_idx+3] = b;

                col_idx += 4;
            }

            font_pixmap = Content.CreatePixmap(font_pixmap_bytes, TEX_W, TEX_H);

            font_texture = Content.CreateTexture(font_pixmap);


        }

        public override void Draw(Canvas canvas)
        {
            /*canvas.Begin();
            canvas.DrawRect(-400, -300, TEX_W, TEX_H, 1, Color.White);
            canvas.DrawTexture(font_texture, -400, -300, Color.White);
            canvas.End();*/
        }

        public override void Update(float dt)
        {
        }
    }
}
