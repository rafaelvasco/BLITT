using System;
using System.IO;
using System.Text;
using BLITTEngine.GameToolkit;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class FontLoader : BaseLoader
    {
        public override Resource Load(Stream file_stream)
        {
            var glyphs = new Sprite[255];
            var pre_spacings = new float[255];
            var post_spacings = new float[255];
            Texture2D font_tex = null;

            using (var reader = new StreamReader(file_stream, Encoding.UTF8))
            {
                string line;
                var idx = 0;
                string bitmap_name = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        continue;
                    }

                    if (idx == 0 && !line.Equals(Font.Header_Tag))
                    {
                        throw new Exception("Invalid Font Description File.");
                    }

                    if (line.StartsWith(Font.Bmp_Tag))
                    {
                        bitmap_name = line.Split('=')[1];

                        var manifest_key = bitmap_name + "_sheet";

                        var font_sheet_path = Game.Instance.ContentManager.Manifest.Resources[manifest_key];

                        font_tex = Game.Instance.ResourceLoader.LoadFile<Texture2D>(font_sheet_path);
                    }
                    else if (line.StartsWith(Font.Char_Tag))
                    {
                        if (bitmap_name == null)
                        {
                            throw new Exception("Invalid Font Description File: Missing Bitmap reference");
                        }

                        string char_def_str = line.Split('=')[1];

                        string[] char_def_attrs = char_def_str.Split(',');

                        if (char_def_attrs.Length != 7)
                        {
                            throw new Exception(
                                $"Invalid Font Description File: Invalid Char Definition at line: {line + 1}");
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

                        glyphs[ch_idx] = new Sprite(font_tex, letter_reg_x, letter_reg_y, letter_reg_w, letter_reg_h);

                        pre_spacings[ch_idx] = letter_pre_spac;
                        post_spacings[ch_idx] = letter_post_spac;
                    }

                    idx++;
                }
            }

            return new Font(font_tex, glyphs, pre_spacings, post_spacings);
        }
    }
}