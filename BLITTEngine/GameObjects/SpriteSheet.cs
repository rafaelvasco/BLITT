//using BLITTEngine.Core.Graphics;

//namespace BLITTEngine.GameObjects
//{
//    public class SpriteSheet
//    {
//        private Quad[] quads;

//        internal readonly Texture2D Texture;

//        public ref Quad this[int index]
//        {
//            get => ref quads[index];
//        }

//        private SpriteSheet(Texture2D texture, Quad[] quads)
//        {
//            this.Texture = texture;
//            this.quads = quads;
//        }

//        public static SpriteSheet FromGrid(Texture2D texture, int rows, int cols)
//        {
//            int tex_w = texture.Width;
//            int tex_h = texture.Height;

//            int tile_width = tex_w / cols;
//            int tile_height = tex_h / rows;

//            Quad[] quads = new Quad[rows * cols];

//            var quad_idx = 0;

//            float x, y, u, v, u2, v2;

//            for (var i = 0; i < cols; i++)
//            {
//                for (var j = 0; j < rows; j++)
//                {
//                    x = j * tile_width;
//                    y = i * tile_height;

//                    u = x / tex_w;
//                    v = y / tex_h;
//                    u2 = (x + tile_width) / tex_w;
//                    v2 = (y + tile_height) / tex_h;

//                    quads[quad_idx++] = new Quad()
//                    {
//                        U = u,
//                        V = v,
//                        U2 = u2,
//                        V2 = v2,
//                        W = tile_width,
//                        H = tile_height,
//                        Col = 0xFFFFFFFF
//                    };
//                }
//            }

//            return new SpriteSheet(texture, quads);
//        }

//        public static SpriteSheet FromTileSize(Texture2D texture, int tile_width, int tile_height)
//        {
//            int tex_w = texture.Width;
//            int tex_h = texture.Height;

//            int rows = tex_w / tile_width;
//            int columns = tex_h / tile_height;

//            return SpriteSheet.FromGrid(texture, rows, columns);
//        }
//    }
//}