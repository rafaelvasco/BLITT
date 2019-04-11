using System.Collections.Generic;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Resources
{
    
    public class TextureAtlas : Resource
    {
        public Quad this[int index] => _quads[index];

        public Quad this[string name]
        {
            get
            {
                if (_name_map.TryGetValue(name, out int index))
                {
                    return _quads[index];
                }

                return _empty_quad;
            }
        }

        public Texture2D Texture => _texture;
        
        public int Count => _quads.Length;

        private readonly Quad[] _quads;

        private readonly Texture2D _texture;

        private readonly Quad _empty_quad = new Quad();

        private Dictionary<string, int> _name_map;

        
        public static TextureAtlas FromGrid(Texture2D texture, int rows, int columns)
        {
            
            int tex_w = texture.Width;
            int tex_h = texture.Height;

            int tile_width = tex_w / columns;
            int tile_height = tex_h / rows;
            
            var regions = new Rect[rows * columns];

            int index = 0;
            
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    float x = j * tile_width;
                    float y = i * tile_height;

                    regions[index++] = Rect.FromBox(x, y, tile_width, tile_height);
                }   
            }
            
            return new TextureAtlas(texture, regions);
        }

        public static TextureAtlas FromAtlas(Texture2D texture, Dictionary<string, Rect> atlas)
        {
            var regions = new Rect[atlas.Count];
            var map = new Dictionary<string, int>();

            var idx = 0;
            
            foreach (var atlas_pair in atlas)
            {
                regions[idx] = atlas_pair.Value;
                
                map.Add(atlas_pair.Key, idx);

                idx++;
            }

            var tex_atlas = new TextureAtlas(texture, regions) {_name_map = map};

            return tex_atlas;

        }


        private TextureAtlas(Texture2D texture, Rect[] regions)
        {
            _texture = texture;
            
            this._quads = new Quad[regions.Length];

            for (int i = 0; i < regions.Length; ++i)
            {
                ref var region = ref regions[i];
                
                _quads[i] = new Quad(texture, Rect.FromBox(region.X1, region.Y1, region.Width, region.Height));
            }
        }


        internal override void Dispose()
        {
            _texture?.Dispose();
        }
    }

}
