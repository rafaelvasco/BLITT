using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Resources;

namespace BLITTBuilder
{
    public static class Loader
    {
        private const string FNT_HEADER_TAG = "[BTFONT]";
        private const string FNT_CHAR_TAG = "Char=";
        
        private static readonly ImageReader _image_reader = new ImageReader();

        public static PixmapData LoadPixmapData(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Image img = _image_reader.Read(stream);

                var pixmap_data = new PixmapData()
                {
                    Data = img.Data,
                    Width = img.Width,
                    Height = img.Height
                };

                return pixmap_data;
            }
        }

        public static ShaderProgramData LoadShaderProgramData(string vs_path, string fs_path)
        {
            var vs_bytes = File.ReadAllBytes(vs_path);
            var fs_bytes = File.ReadAllBytes(fs_path);

            var id = Path.GetFileNameWithoutExtension(vs_path).Replace("vs_", "");

            var shader_program_data = new ShaderProgramData()
            {
                Id = id,
                VertexShader = vs_bytes,
                FragmentShader = fs_bytes
            };

            return shader_program_data;
        }

        public static FontData LoadFontData(string descr_path, string image_path)
        {
            var sheet_data = LoadPixmapData(image_path);

            var glyphs = new Rect[255];
            var pre_spacings = new float[255];
            var post_spacings = new float[255];

            using (var descr_stream = File.OpenRead(descr_path))
            {
                using (var reader = new StreamReader(descr_stream, Encoding.UTF8))
                {
                    string line;
                    var idx = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length == 0)
                        {
                            continue;
                        }

                        if (idx == 0 && !line.Equals(FNT_HEADER_TAG))
                        {
                            throw new Exception("Invalid Font Description File.");
                        }

                        if (line.StartsWith(FNT_CHAR_TAG))
                        {
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

                            glyphs[ch_idx] = new Rect(letter_reg_x, letter_reg_y, letter_reg_w,
                                letter_reg_h);

                            pre_spacings[ch_idx] = letter_pre_spac;
                            post_spacings[ch_idx] = letter_post_spac;
                        }

                        idx++;
                    }
                }
            }

            var id = Path.GetFileNameWithoutExtension(descr_path);

            var font_data = new FontData()
            {
                FontSheet = sheet_data,
                GlyphRects = glyphs,
                Id = id,
                PreSpacings = pre_spacings,
                PostSpacings = post_spacings
            };

            return font_data;
        }

        public static SfxData LoadSfxData(string path)
        {
            var bytes = File.ReadAllBytes(path);

            var id = Path.GetFileNameWithoutExtension(path);

            var sfx_data = new SfxData()
            {
                Id = id,
                Data = bytes
            };

            return sfx_data;
        }

        public static SongData LoadSongData(string path)
        {
            var bytes = File.ReadAllBytes(path);

            var id = Path.GetFileNameWithoutExtension(path);

            var song_data = new SongData()
            {
                Id = id,
                Data = bytes
            };

            return song_data;
        }

        public static TextFileData LoadTextFileData(string path)
        {
            var text = File.ReadAllLines(path);

            var id = Path.GetFileNameWithoutExtension(path);

            var text_file_data = new TextFileData()
            {
                Id = id,
                TextData = text.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray()
            };

            return text_file_data;
        }
        
    }
}