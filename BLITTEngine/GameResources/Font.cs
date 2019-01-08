using System;
using System.IO;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.DisplayObjects;

namespace BLITTEngine.GameResources
{
    public class Font : Resource
    {
        public Texture2D Texture => font_tex;

        private const string header_tag = "[BTFONT]";
        private const string bmp_tag = "Bitmap=";
        private const string char_tag = "Char=";

        internal Texture2D font_tex;
        internal readonly Sprite[] letters;
        internal readonly float[] pre_spacings;
        internal readonly float[] post_spacings;

        internal Font(string file)
        {
            letters = new Sprite[256];
            pre_spacings = new float[256];
            post_spacings = new float[256];

            ParseFontDescription(file);
        }

        private void ParseFontDescription(string file)
        {
            string[] file_lines = File.ReadAllLines(file);

            string line;

            string bitmap_file = null;

            for (int i = 0; i < file_lines.Length; ++i)
            {
                line = file_lines[i];

                if (line.Length == 0)
                {
                    continue;
                }

                if (i == 0 && !line.Equals(header_tag))
                {
                    throw new Exception("Invalid Font Description File.");
                }

                if (line.StartsWith(bmp_tag))
                {
                    bitmap_file = line.Split('=')[1];

                    font_tex = Content.LoadTexture(bitmap_file);
                }
                else if (line.StartsWith(char_tag))
                {
                    if (bitmap_file == null)
                    {
                        throw new Exception("Invalid Font Description File: Missing Bitmap reference");
                    }

                    string char_def_str = line.Split('=')[1];

                    string[] char_def_attrs = char_def_str.Split(',');

                    if (char_def_attrs.Length != 7)
                    {
                        throw new Exception($"Invalid Font Description File: Invalid Char Definition at line: {i+1}");
                    }

                    int ch_idx = int.Parse(char_def_attrs[0]);

                    if (ch_idx < 0 || ch_idx > 255)
                    {
                        throw new Exception("Invalid Font Description File: Character Id out of range");
                    }

                    int letter_reg_x = int.Parse(char_def_attrs[1]);
                    int letter_reg_y = int.Parse(char_def_attrs[2]);
                    int letter_reg_w = int.Parse(char_def_attrs[3]);
                    int letter_reg_h = int.Parse(char_def_attrs[4]);
                    int letter_pre_spac = int.Parse(char_def_attrs[5]);
                    int letter_post_spac = int.Parse(char_def_attrs[6]);

                    letters[ch_idx] = new Sprite(font_tex, letter_reg_x, letter_reg_y, letter_reg_w, letter_reg_h);



                    pre_spacings[ch_idx] = letter_pre_spac;
                    post_spacings[ch_idx] = letter_post_spac;
                }
            }
        }

        internal override void Dispose()
        {
            this.font_tex.Dispose();
        }
    }
}
