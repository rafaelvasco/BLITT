using BLITTEngine.Core.Graphics;
using BLITTEngine.Numerics;

namespace BLITTEngine.Draw
{
    public class Sprite : GameObject
    {
        public float X;

        public float Y;

        public Color ColorTint
        {
            get => col;
            set
            {
                if (col != value)
                {
                    col = value;

                    Quad.Col = col;
                }
            }
        }

        public Vector2 Origin
        {
            get => new Vector2(offset_x / Quad.W, offset_y / Quad.H);
            set
            {
                offset_x = value.X * Quad.W;
                offset_y = value.Y * Quad.H;
            }
        }

        public Size Size
        {
            get => new Size(Quad.W, Quad.H);
            set
            {
                float ox = Origin.X;
                float oy = Origin.Y;

                Quad.W = value.W;
                Quad.H = value.H;

                offset_x = ox * Quad.W;
                offset_y = oy * Quad.H;

                scale = 1.0f;
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                if (scale != value)
                {
                    scale = value;

                    Size = new Size(Quad.W * scale, Quad.H * scale);
                }
            }
        }

        internal Texture2D Texture;
        internal Quad Quad;

        private uint col = 0xFFFFFFFF;
        private float offset_x;
        private float offset_y;
        private float scale = 1.0f;

        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.Quad = new Quad()
            {
                U = 0,
                V = 0,
                U2 = 1,
                V2 = 1,
                W = texture.Width,
                H = texture.Height,
                Col = col
            };

            this.Origin = new Vector2(0.5f, 0.5f);
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            ref var quad = ref Quad;

            Renderer.AddQuad(Texture, X - offset_x, Y - offset_y, in Quad);
        }
    }
}