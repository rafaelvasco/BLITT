using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class Sprite
    {
        private bool flip_x;
        private bool flip_y;
        private bool orig_flip;
        private float origin_x;
        private float origin_y;

        private Quad quad;
        private float src_x;
        private float src_y;
        private float tex_h;
        private float tex_w;

        public Sprite(Texture2D texture) : this(texture, 0, 0, texture.Width, texture.Height)
        {
        }

        public Sprite(Texture2D texture, float src_x, float src_y, float w, float h)
        {
            this.src_x = src_x;
            this.src_y = src_y;
            Width = w;
            Height = h;

            if (texture != null)
            {
                tex_w = texture.Width;
                tex_h = texture.Height;
            }
            else
            {
                tex_w = 1.0f;
                tex_h = 1.0f;
            }

            origin_x = 0;
            origin_y = 0;
            flip_x = false;
            flip_y = false;
            quad.Texture = texture;

            var u = src_x / tex_w;
            var v = src_y / tex_h;
            var u2 = (src_x + w) / tex_w;
            var v2 = (src_y + h) / tex_h;

            quad.V0.Tx = u;
            quad.V0.Ty = v;
            quad.V0.Col = 0xFFFFFFFF;
            quad.V1.Tx = u2;
            quad.V1.Ty = v;
            quad.V1.Col = 0xFFFFFFFF;
            quad.V2.Tx = u2;
            quad.V2.Ty = v2;
            quad.V2.Col = 0xFFFFFFFF;
            quad.V3.Tx = u;
            quad.V3.Ty = v2;
            quad.V3.Col = 0xFFFFFFFF;

            quad.Blend = BlendMode.AlphaBlend;
        }

        public Vector2 Origin => new Vector2(origin_x, origin_y);

        public float Width { get; private set; }

        public float Height { get; private set; }

        public BlendMode BlendMode
        {
            get => quad.Blend;
            set => quad.Blend = value;
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            var tx1 = x - origin_x;
            var ty1 = y - origin_y;
            var tx2 = x + Width - origin_x;
            var ty2 = y + Height - origin_y;

            ref var q = ref quad;

            q.V0.X = tx1;
            q.V0.Y = ty1;
            q.V1.X = tx2;
            q.V1.Y = ty1;
            q.V2.X = tx2;
            q.V2.Y = ty2;
            q.V3.X = tx1;
            q.V3.Y = ty2;

            canvas.DrawQuad(ref q);
        }

        public void DrawEx(Canvas canvas, float x, float y, float rot, float hscale, float vscale = 0)
        {
            if (vscale == 0.0f) vscale = hscale;

            var tx1 = -origin_x * hscale;
            var ty1 = -origin_y * vscale;
            var tx2 = (Width - origin_x) * hscale;
            var ty2 = (Height - origin_y) * vscale;

            ref var q = ref quad;

            if (rot != 0.0f)
            {
                var cost = Calc.Cos(rot);
                var sint = Calc.Sin(rot);

                q.V0.X = tx1 * cost - ty1 * sint + x;
                q.V0.Y = tx1 * sint + ty1 * cost + y;
                q.V1.X = tx2 * cost - ty1 * sint + x;
                q.V1.Y = tx2 * sint + ty1 * cost + y;
                q.V2.X = tx2 * cost - ty2 * sint + x;
                q.V2.Y = tx2 * sint + ty2 * cost + y;
                q.V3.X = tx1 * cost - ty2 * sint + x;
                q.V3.Y = tx1 * sint + ty2 * cost + y;
            }
            else
            {
                q.V0.X = tx1 + x;
                q.V0.Y = ty1 + y;
                q.V1.X = tx2 + x;
                q.V1.Y = ty1 + y;
                q.V2.X = tx2 + x;
                q.V2.Y = ty2 + y;
                q.V3.X = tx1 + x;
                q.V3.Y = ty2 + y;
            }

            canvas.DrawQuad(ref q);
        }

        public void DrawStretch(Canvas canvas, float x1, float y1, float x2, float y2)
        {
            ref var q = ref quad;

            q.V0.X = x1;
            q.V0.Y = y1;
            q.V1.X = x2;
            q.V1.Y = y1;
            q.V2.X = x2;
            q.V2.Y = y2;
            q.V3.X = x1;
            q.V3.Y = y2;

            canvas.DrawQuad(ref q);
        }

        public void Draw4Vertices
        (
            Canvas canvas,
            float x0, float y0,
            float x1, float y1,
            float x2, float y2,
            float x3, float y3
        )
        {
            ref var q = ref quad;

            q.V0.X = x0;
            q.V0.Y = y0;
            q.V1.X = x1;
            q.V1.Y = y1;
            q.V2.X = x2;
            q.V2.Y = y2;
            q.V3.X = x3;
            q.V3.Y = y3;

            canvas.DrawQuad(ref q);
        }

        public void SetFlip(bool flip_h, bool flip_v, bool flip_origin = true)
        {
            float tx, ty;

            if (flip_origin && flip_h) origin_x = Width - origin_x;
            if (flip_origin && flip_v) origin_y = Height - origin_y;

            orig_flip = flip_origin;

            if (flip_origin && flip_h) origin_x = Width - origin_x;
            if (flip_origin && flip_v) origin_y = Height - origin_y;

            ref var q = ref quad;

            if (flip_h != flip_x)
            {
                tx = q.V0.Tx;
                q.V0.Tx = q.V1.Tx;
                q.V1.Tx = tx;

                ty = q.V0.Ty;
                q.V0.Ty = q.V1.Ty;
                q.V1.Ty = ty;

                tx = q.V3.Tx;
                q.V3.Tx = q.V2.Tx;
                q.V2.Tx = tx;

                ty = q.V3.Ty;
                q.V3.Ty = q.V2.Ty;
                q.V2.Ty = ty;

                flip_x = flip_h;
            }

            if (flip_v != flip_y)
            {
                tx = q.V0.Tx;
                q.V0.Tx = q.V3.Tx;
                q.V3.Tx = tx;

                ty = q.V0.Ty;
                q.V0.Ty = q.V3.Ty;
                q.V3.Ty = ty;

                tx = q.V1.Tx;
                q.V1.Tx = q.V2.Tx;
                q.V2.Tx = tx;

                ty = q.V1.Ty;
                q.V1.Ty = q.V2.Ty;
                q.V2.Ty = ty;

                flip_y = flip_v;
            }
        }

        public void SetTexture(Texture2D tex)
        {
            float tw, th;

            ref var q = ref quad;

            q.Texture = tex;

            if (tex != null)
            {
                tw = tex.Width;
                th = tex.Height;
            }
            else
            {
                tw = 1.0f;
                th = 1.0f;
            }

            if (tw != tex_w || th != tex_h)
            {
                var u = q.V0.Tx * tex_w;
                var v = q.V0.Ty * tex_h;
                var u2 = q.V2.Tx * tex_w;
                var v2 = q.V2.Ty * tex_h;

                tex_w = tw;
                tex_h = th;

                u /= tw;
                v /= th;
                u2 /= tw;
                v2 /= th;

                quad.V0.Tx = u;
                quad.V0.Ty = v;
                quad.V1.Tx = u2;
                quad.V1.Ty = v;
                quad.V2.Tx = u2;
                quad.V2.Ty = v2;
                quad.V3.Tx = u;
                quad.V3.Ty = v2;
            }
        }

        public void SetTextureRect(float x, float y, float w, float h, bool adj_size)
        {
            src_x = x;
            src_y = y;

            if (adj_size)
            {
                Width = w;
                Height = h;
            }

            var u = src_x / tex_w;
            var v = src_y / tex_h;
            var u2 = (src_x + w) / tex_w;
            var v2 = (src_y + h) / tex_h;

            ref var q = ref quad;

            q.V0.Tx = u;
            q.V0.Ty = v;
            q.V1.Tx = u2;
            q.V1.Ty = v;
            q.V2.Tx = u2;
            q.V2.Ty = v2;
            q.V3.Tx = u;
            q.V3.Ty = v2;

            var flipx = flip_x;
            var flipy = flip_y;

            flip_x = false;
            flip_y = false;

            SetFlip(flipx, flipy, orig_flip);
        }

        public void SetColor(Color color)
        {
            ref var q = ref quad;

            q.V0.Col = color;
            q.V1.Col = color;
            q.V2.Col = color;
            q.V3.Col = color;
        }

        public void SetColor
        (
            Color top_left_col,
            Color top_right_col,
            Color bottom_left_col,
            Color bottom_right_col
        )
        {
            ref var q = ref quad;

            q.V0.Col = top_left_col;
            q.V1.Col = top_right_col;
            q.V2.Col = bottom_right_col;
            q.V3.Col = bottom_left_col;
        }

        public Color GetColor(int corner = 0)
        {
            switch (corner)
            {
                case 0: return new Color(quad.V0.Col);
                case 1: return new Color(quad.V1.Col);
                case 2: return new Color(quad.V2.Col);
                case 3: return new Color(quad.V3.Col);
            }

            return default;
        }

        public void SetOrigin(float ox, float oy)
        {
            origin_x = Width * ox;
            origin_y = Height * oy;
        }

        public Rect GetBoundingBox(float ref_x, float ref_y)
        {
            return Rect.FromBox(ref_x - origin_x, ref_y - origin_y, ref_x - origin_x + Width,
                ref_y - origin_y + Height);
        }
    }
}