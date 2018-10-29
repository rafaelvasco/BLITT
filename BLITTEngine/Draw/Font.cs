//using BLITTEngine.Core.Graphics;
//using BLITTEngine.Resources;
//using System;
//using System.Collections.Generic;

//namespace BLITTEngine.Draw
//{
//    public class Font : Resource
//    {
//        public Texture2D Texture => font_texture;

//        private readonly Texture2D font_texture;
//        private readonly Pixmap font_pixmap;

//        private readonly Dictionary<int, Quad> glyph_quads;

//        private Quad empty_glyph_quad;

//        public Quad this[char ch]
//        {
//            get
//            {
//                if (glyph_quads.TryGetValue(ch, out Quad glyph_quad))
//                {
//                    return glyph_quad;
//                }
//                return empty_glyph_quad;

//            }
//        }

//        internal Font(Pixmap font_bitmap)
//        {
//            this.glyph_quads = new Dictionary<int, Quad>();
//            this.font_texture = new Texture2D(font_bitmap);
//            this.font_pixmap = font_bitmap;

//            LoadGlyphRects(font_bitmap);
//        }

//        private unsafe void LoadGlyphRects(Pixmap font_pixmap)
//        {
//            var pixel_data = font_pixmap.PixelData;

//            var w = font_pixmap.Width;

//            var h = font_pixmap.Height;
//            int idx = 0;
//            int glyph_idx = 32;
//            bool first_found = false;
//            int x_first_found = 0;
//            int glpyh_h = 0;

//            fixed (byte* p = pixel_data)
//            {
//                for (int j = 0; j < h; j++)
//                {
//                    for (int i = 0; i < w; i++)
//                    {
//                        idx = (i + j * w) * 4;

//                        byte r = p[idx];
//                        byte g = p[idx + 1];
//                        byte b = p[idx + 2];

//                        if (!first_found && r == 255 && g == 0 && b == 255)
//                        {
//                            x_first_found = i;

//                            if (glpyh_h == 0)
//                            {
//                                glpyh_h = j;
//                            }

//                            first_found = true;
//                        }
//                        else if (first_found && (r != 255 || g != 0 || b != 255))
//                        {
//                            first_found = false;

//                            int gw = i - x_first_found;
//                            int gy = j - glpyh_h;

//                            glyph_quads[glyph_idx++] = new Quad(font_texture, x_first_found, gy, gw, glpyh_h);

//                        }

//                    }
//                }
//            }

//            empty_glyph_quad = this[' '];

//        }

//        internal override void Dispose()
//        {
//            font_pixmap.Dispose();
//            font_texture.Dispose();
//        }
//    }
//}
