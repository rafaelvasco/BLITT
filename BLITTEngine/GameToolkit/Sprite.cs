using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class Sprite
    {
        public bool FlipH => _flip_x;
        public bool FlipV => _flip_y;
        
        public Sprite(Texture2D texture) : this(texture, 0, 0, texture.Width, texture.Height)
        {
        }

        public Sprite(Texture2D texture, Quad quad)
        {
            _texture = texture;
            Width = quad.Width;
            Height = quad.Height;
            
            _origin_x = 0;
            _origin_y = 0;
            _flip_x = false;
            _flip_y = false;

            _quad = quad;

        }

        public Sprite(Texture2D texture, float src_x, float src_y, float src_w, float src_h)
        {
            Width = src_w;
            Height = src_h;

            _origin_x = 0;
            _origin_y = 0;
            _flip_x = false;
            _flip_y = false;
            _texture = texture;
            
            var tex_w = texture.Width;
            var tex_h = texture.Height;

            var u = src_x / tex_w;
            var v = src_y / tex_h;
            var u2 = (src_x + src_w) / tex_w;
            var v2 = (src_y + src_h) / tex_h;

            _quad.V0.Tx = u;
            _quad.V0.Ty = v;
            _quad.V0.Col = 0xFFFFFFFF;
            _quad.V1.Tx = u2;
            _quad.V1.Ty = v;
            _quad.V1.Col = 0xFFFFFFFF;
            _quad.V2.Tx = u2;
            _quad.V2.Ty = v2;
            _quad.V2.Col = 0xFFFFFFFF;
            _quad.V3.Tx = u;
            _quad.V3.Ty = v2;
            _quad.V3.Col = 0xFFFFFFFF;
            
            _quad.Blend = BlendMode.AlphaBlend;
        }

        public Vector2 Origin => new Vector2(_origin_x, _origin_y);

        public float Width { get; protected set; }

        public float Height { get; protected set; }

        public BlendMode BlendMode
        {
            get => _quad.Blend;
            set => _quad.Blend = value;
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            var tx1 = x - _origin_x;
            var ty1 = y - _origin_y;
            var tx2 = x + Width - _origin_x;
            var ty2 = y + Height - _origin_y;

            _quad.V0.X = tx1;
            _quad.V0.Y = ty1;
            _quad.V1.X = tx2;
            _quad.V1.Y = ty1;
            _quad.V2.X = tx2;
            _quad.V2.Y = ty2;
            _quad.V3.X = tx1;
            _quad.V3.Y = ty2;

            canvas.DrawQuad(_texture, ref _quad);
        }

        public void DrawEx(Canvas canvas, float x, float y, float rot, float hscale = 1, float vscale = -1)
        {

            if (vscale < 0)
            {
                vscale = hscale;
            }

            var tx1 = -_origin_x * hscale;
            var ty1 = -_origin_y * vscale;
            var tx2 = (Width - _origin_x) * hscale;
            var ty2 = (Height - _origin_y) * vscale;

            if (rot != 0.0f)
            {
                var cost = Calc.Cos(rot);
                var sint = Calc.Sin(rot);

                _quad.V0.X = tx1 * cost - ty1 * sint + x;
                _quad.V0.Y = tx1 * sint + ty1 * cost + y;
                _quad.V1.X = tx2 * cost - ty1 * sint + x;
                _quad.V1.Y = tx2 * sint + ty1 * cost + y;
                _quad.V2.X = tx2 * cost - ty2 * sint + x;
                _quad.V2.Y = tx2 * sint + ty2 * cost + y;
                _quad.V3.X = tx1 * cost - ty2 * sint + x;
                _quad.V3.Y = tx1 * sint + ty2 * cost + y;
            }
            else
            {
                _quad.V0.X = tx1 + x;
                _quad.V0.Y = ty1 + y;
                _quad.V1.X = tx2 + x;
                _quad.V1.Y = ty1 + y;
                _quad.V2.X = tx2 + x;
                _quad.V2.Y = ty2 + y;
                _quad.V3.X = tx1 + x;
                _quad.V3.Y = ty2 + y;
            }

            canvas.DrawQuad(_texture, ref _quad);
        }


        public void SetFlipH(bool flip, bool flip_origin = true)
        {
            if (_flip_x == flip)
            {
                return;
            }
            
            SetFlip(flip, _flip_y, flip_origin);
        }

        public void SetFlipV(bool flip, bool flip_origin = true)
        {
            if (_flip_y == flip)
            {
                return;
            }
            
            SetFlip(_flip_x, flip, flip_origin);
        }

        public void SetFlip(bool flip_h, bool flip_v, bool flip_origin = true)
        {
            if (_flip_x == flip_h && _flip_y == flip_v)
            {
                return;
            }
            
            float tx, ty;

            if (flip_origin && _flip_x) _origin_x = Width - _origin_x;
            if (flip_origin && _flip_y) _origin_y = Height - _origin_y;

            _orig_flip = flip_origin;

            if (flip_origin && _flip_x) _origin_x = Width - _origin_x;
            if (flip_origin && _flip_y) _origin_y = Height - _origin_y;


            if (flip_h != _flip_x)
            {
                tx = _quad.V0.Tx;
                _quad.V0.Tx = _quad.V1.Tx;
                _quad.V1.Tx = tx;

                ty = _quad.V0.Ty;
                _quad.V0.Ty = _quad.V1.Ty;
                _quad.V1.Ty = ty;

                tx = _quad.V3.Tx;
                _quad.V3.Tx = _quad.V2.Tx;
                _quad.V2.Tx = tx;

                ty = _quad.V3.Ty;
                _quad.V3.Ty = _quad.V2.Ty;
                _quad.V2.Ty = ty;

                _flip_x = !_flip_x;
            }

            if (flip_v != _flip_y)
            {
                tx = _quad.V0.Tx;
                _quad.V0.Tx = _quad.V3.Tx;
                _quad.V3.Tx = tx;

                ty = _quad.V0.Ty;
                _quad.V0.Ty = _quad.V3.Ty;
                _quad.V3.Ty = ty;

                tx = _quad.V1.Tx;
                _quad.V1.Tx = _quad.V2.Tx;
                _quad.V2.Tx = tx;

                ty = _quad.V1.Ty;
                _quad.V1.Ty = _quad.V2.Ty;
                _quad.V2.Ty = ty;

                _flip_y = !_flip_y;
            }
            
            
        }

        public void SetTexture(Texture2D tex)
        {
            if (tex == null) 
            {
                _quad = new Quad();
                return;
            }

            var old_tex_w = _texture.Width;
            var old_tex_h = _texture.Height;

            var tex_w = tex.Width;
            var tex_h = tex.Height;
            

            if ( tex_w != old_tex_w || tex_h != old_tex_h)
            {
                var u = _quad.V0.Tx * old_tex_w;
                var v = _quad.V0.Ty * old_tex_h;
                var u2 = _quad.V2.Tx * old_tex_w;
                var v2 = _quad.V2.Ty * old_tex_h;

                u /= tex_w;
                v /= tex_h;
                u2 /= tex_w;
                v2 /= tex_h;

                _quad.V0.Tx = u;
                _quad.V0.Ty = v;
                _quad.V1.Tx = u2;
                _quad.V1.Ty = v;
                _quad.V2.Tx = u2;
                _quad.V2.Ty = v2;
                _quad.V3.Tx = u;
                _quad.V3.Ty = v2;
            }
        }

        public void SetTextureRect(float src_x, float src_y, float src_w, float src_h, bool adj_size)
        {
            if (adj_size)
            {
                Width = src_w;
                Height = src_h;
            }

            var tex_w = _texture.Width;
            var tex_h = _texture.Height;

            var u = src_x / tex_w;
            var v = src_y / tex_h;
            var u2 = (src_x + src_w) / tex_w;
            var v2 = (src_y + src_h) / tex_h;

            _quad.V0.Tx = u;
            _quad.V0.Ty = v;
            _quad.V1.Tx = u2;
            _quad.V1.Ty = v;
            _quad.V2.Tx = u2;
            _quad.V2.Ty = v2;
            _quad.V3.Tx = u;
            _quad.V3.Ty = v2;

            var flipx = _flip_x;
            var flipy = _flip_y;

            _flip_x = false;
            _flip_y = false;

            SetFlip(flipx, flipy, _orig_flip);
        }

        public void SetColor(Color color)
        {
            uint abgr = color.ABGR;
            
            _quad.V0.Col = abgr;
            _quad.V1.Col = abgr;
            _quad.V2.Col = abgr;
            _quad.V3.Col = abgr;
        }

        public void SetColor(uint abgr)
        {
            _quad.V0.Col = abgr;
            _quad.V1.Col = abgr;
            _quad.V2.Col = abgr;
            _quad.V3.Col = abgr;
        }
        

        public void SetColor
        (
            Color top_left_col,
            Color top_right_col,
            Color bottom_left_col,
            Color bottom_right_col
        )
        {
            uint abgr_tl = top_left_col.ABGR;
            uint abgr_tr = top_right_col.ABGR;
            uint abgr_bl = bottom_left_col.ABGR;
            uint abgr_br = bottom_right_col.ABGR;

            _quad.V0.Col = abgr_tl;
            _quad.V1.Col = abgr_tr;
            _quad.V2.Col = abgr_br;
            _quad.V3.Col = abgr_bl;
        }
        
        public void SetColor
        (
            uint top_left_col,
            uint top_right_col,
            uint bottom_left_col,
            uint bottom_right_col
        )
        {
            _quad.V0.Col = top_left_col;
            _quad.V1.Col = top_right_col;
            _quad.V2.Col = bottom_right_col;
            _quad.V3.Col = bottom_left_col;
        }

        public Color GetColor(int corner = 0)
        {
            switch (corner)
            {
                case 0: return new Color(_quad.V0.Col);
                case 1: return new Color(_quad.V1.Col);
                case 2: return new Color(_quad.V2.Col);
                case 3: return new Color(_quad.V3.Col);
            }

            return default;
        }

        public void SetOrigin(float ox, float oy)
        {
            _origin_x = Width * ox;
            _origin_y = Height * oy;
        }

        public void SetOrigin(ref Vector2 orig)
        {
            _origin_x = Width * orig.X;
            _origin_y = Height * orig.Y;
        }

        public Rect GetBoundingBox(float ref_x, float ref_y)
        {
            return Rect.FromBox(ref_x - _origin_x, ref_y - _origin_y, ref_x - _origin_x + Width,
                ref_y - _origin_y + Height);
        }

       
        
        protected bool _flip_x;
        protected bool _flip_y;
        protected bool _orig_flip;
        protected float _origin_x;
        protected float _origin_y;

        protected readonly Texture2D _texture;
        
        protected Quad _quad;

      
    }
}