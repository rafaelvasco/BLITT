using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameToolkit;

namespace BLITTEngine.Core.Resources
{
    internal class ResourceLoader
    {
        public ResourcePak LoadPak(string pak_name)
        {
            var path = Path.Combine(Game.Instance.ContentManager.RootPath,
                !pak_name.Contains(".pak") ? pak_name + ".pak" : pak_name);

            var pak_bytes = File.ReadAllBytes(path);

            var resource_pak = BinarySerializer.Deserialize<ResourcePak>(pak_bytes);

            return resource_pak;
        }

        public Texture2D LoadTexture(PixmapData pixmap_data)
        {
            var pixmap = new Pixmap(pixmap_data.Data, pixmap_data.Width, pixmap_data.Height);

            var texture = Game.Instance.GraphicsContext.CreateTexture(pixmap);

            texture.Id = pixmap_data.Id;

            return texture;
        }

        public Font LoadFont(FontData font_data)
        {
            var texture = LoadTexture(font_data.FontSheet);
            var glyphs = new Sprite[font_data.GlyphRects.Length];

            for (int i = 0; i < font_data.GlyphRects.Length; ++i)
            {
                var glyph_rect = font_data.GlyphRects[i];
                
                glyphs[i] = new Sprite(texture, glyph_rect.X1, glyph_rect.Y1, glyph_rect.Width, glyph_rect.Height);
            }

            var font = new Font(texture, glyphs, font_data.PreSpacings, font_data.PostSpacings) {Id = font_data.Id};


            return font;
        }

        public ShaderProgram LoadShader(ShaderProgramData shader_data)
        {
            var shader_program =
                Game.Instance.GraphicsContext.CreateShader(shader_data.VertexShader, shader_data.FragmentShader);

            shader_program.Id = shader_data.Id;

            return shader_program;
        }

        public Effect LoadEffect(SfxData sfx_data)
        {
            var wav = new Wav();
            
            var pinned_array = GCHandle.Alloc(sfx_data.Data, GCHandleType.Pinned);

            var data_ptr = pinned_array.AddrOfPinnedObject();

            wav.loadMem(data_ptr, (uint) sfx_data.Data.Length);
            
            pinned_array.Free();
            
            var effect = new Effect(wav);

            return effect;
        }

        public Song LoadSong(SongData song_data)
        {
            var wav_stream = new WavStream();
            
            var pinned_array = GCHandle.Alloc(song_data.Data, GCHandleType.Pinned);

            var data_ptr = pinned_array.AddrOfPinnedObject();

            wav_stream.loadMem(data_ptr, (uint) song_data.Data.Length);
            
            pinned_array.Free();
            
            var song = new Song(wav_stream);

            return song;
        }

        public TextFile LoadTextFile(TextFileData txt_data)
        {
            var txt_file = new TextFile(txt_data.TextData.ToList());

            return txt_file;
        }
    }
}