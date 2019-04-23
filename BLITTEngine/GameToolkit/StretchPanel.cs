using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class StretchPanel
    {
        public class Props
        {
            public int Id;
            public float X;
            public float Y;
            public int W;
            public int H;
            public Texture2D Texture;
            public int LeftMargin;
            public int RightMargin;
            public int TopMargin;
            public int BottomMargin;
            public CenterFillMode FillMode;
        }
        
        public enum CenterFillMode : byte
        {
            Stretch = 0,
            Tile,
            Transparent
        }

        public int LeftMargin => _left_margin;

        public int RightMargin => _right_margin;

        public int TopMargin => _top_margin;

        public int BottomMargin => _bottom_margin;

        public CenterFillMode FillMode
        {
            get => _fill_mode;
            set
            {
                if (_fill_mode != value)
                {
                    _fill_mode = value;

                    if (_centerTileTexture == null)
                    {
                        RebuildCenterSprite();
                    }
                }
            }
        }

        public Origin Origin { get; set; } = Origin.TopLeft;

        public StretchPanel(Props props)
        {
            
            _texture = props.Texture;
            SetMargins(props.LeftMargin, props.RightMargin, props.TopMargin, props.BottomMargin);

            FillMode = props.FillMode;
        }

        public void SetMargins(int left, int right, int top, int bottom)
        {
            _left_margin = left;
            _right_margin = right;
            _top_margin = top;
            _bottom_margin = bottom;
            
            BuildQuads();

            if (FillMode == CenterFillMode.Tile)
            {
                FillMode = CenterFillMode.Tile;
            }
        }

        private void BuildQuads()
        {
            var w = _texture.Width;
            var h = _texture.Height;

            var center_w = w - _left_margin - _right_margin;
            var center_h = h - _top_margin - _bottom_margin;
            
            _top_left = new Quad(_texture, RectF.FromBox(0, 0, _left_margin, _top_margin));
            _top = new Quad(_texture, RectF.FromBox(_left_margin, 0, center_w, _top_margin));
            _top_right = new Quad(_texture, RectF.FromBox(w -_right_margin, 0, _right_margin, _top_margin));
            _left = new Quad(_texture, RectF.FromBox(0, _top_margin, _left_margin, center_h));
            _center = new Quad(_texture, RectF.FromBox(_left_margin, _top_margin, center_w, center_h));
            _right = new Quad(_texture, RectF.FromBox(w-_right_margin, _top_margin, _right_margin, center_h));
            _bottom_left = new Quad(_texture, RectF.FromBox(0, h - _bottom_margin, _left_margin, _bottom_margin));
            _bottom = new Quad(_texture, RectF.FromBox(_left_margin, h - _bottom_margin, center_w, _bottom_margin));
            _bottom_right = new Quad(_texture, RectF.FromBox(w - _right_margin, h - _bottom_margin, _right_margin, _bottom_margin));

            var left = _rect.X1;
            var top = _rect.Y1;
            var right = _rect.X2;
            var bottom = _rect.Y2;
            
            var left_internal = left + _left_margin;
            var top_internal = top + _left_margin;
            var right_internal = right - _right_margin;
            var bottom_internal = bottom - _bottom_margin;
            
            _top_left.V0.X = left;
            _top_left.V0.Y = top;
            
            _top_left.V1.X = left_internal;
            _top_left.V1.Y = top;
            
            _top_left.V2.X = left_internal;
            _top_left.V2.Y = top_internal;
            
            _top_left.V3.X = left;
            _top_left.V3.Y = top_internal;
            
            _top.V0.X = left_internal;
            _top.V0.Y = top;
            
            _top.V1.X = right_internal;
            _top.V1.Y = top;
            
            _top.V2.X = right_internal;
            _top.V2.Y = top_internal;
            
            _top.V3.X = left_internal;
            _top.V3.Y = top_internal;
            
            _top_right.V0.X = right_internal;
            _top_right.V0.Y = top;
            
            _top_right.V1.X = right;
            _top_right.V1.Y = top;
            
            _top_right.V2.X = right;
            _top_right.V2.Y = top_internal;
            
            _top_right.V3.X = right_internal;
            _top_right.V3.Y = top_internal;
            
            _left.V0.X = left;
            _left.V0.Y = top_internal;
            
            _left.V1.X = left_internal;
            _left.V1.Y = top_internal;
            
            _left.V2.X = left_internal;
            _left.V2.Y = bottom_internal;
            
            _left.V3.X = left;
            _left.V3.Y = bottom_internal;
            
            _center.V0.X = left_internal;
            _center.V0.Y = top_internal;
            
            _center.V1.X = right_internal;
            _center.V1.Y = top_internal;
            
            _center.V2.X = right_internal;
            _center.V2.Y = bottom_internal;
            
            _center.V3.X = left_internal;
            _center.V3.Y = bottom_internal;
            
            _right.V0.X = right_internal;
            _right.V0.Y = top_internal;
            
            _right.V1.X = right;
            _right.V1.Y = top_internal;
            
            _right.V2.X = right;
            _right.V2.Y = bottom_internal;
            
            _right.V3.X = right_internal;
            _right.V3.Y = bottom_internal;
            
            _bottom_left.V0.X = left;
            _bottom_left.V0.Y = bottom_internal;
            
            _bottom_left.V1.X = left_internal;
            _bottom_left.V1.Y = bottom_internal;
            
            _bottom_left.V2.X = left_internal;
            _bottom_left.V2.Y = bottom;
            
            _bottom_left.V3.X = left;
            _bottom_left.V3.Y = bottom;
            
            _bottom.V0.X = left_internal;
            _bottom.V0.Y = bottom_internal;
            
            _bottom.V1.X = right_internal;
            _bottom.V1.Y = bottom_internal;
            
            _bottom.V2.X = right_internal;
            _bottom.V2.Y = bottom;
            
            _bottom.V3.X = left_internal;
            _bottom.V3.Y = bottom;
            
            _bottom_right.V0.X = right_internal;
            _bottom_right.V0.Y = bottom_internal;
            
            _bottom_right.V1.X = right;
            _bottom_right.V1.Y = bottom_internal;
            
            _bottom_right.V2.X = right;
            _bottom_right.V2.Y = bottom;
            
            _bottom_right.V3.X = right_internal;
            _bottom_right.V3.Y = bottom;

            if (FillMode == CenterFillMode.Tile)
            {
                RebuildCenterSprite();
            }
            
        }

        private readonly Texture2D _texture;
        private Texture2D _centerTileTexture;
        private TiledSprite _tiledCenterSprite;

        private CenterFillMode _fill_mode;
        
        private int _left_margin;
        private int _right_margin;
        private int _top_margin;
        private int _bottom_margin;
        
        private Quad _top_left;
        private Quad _top;
        private Quad _top_right;

        private Quad _left;
        private Quad _center;
        private Quad _right;

        private Quad _bottom_left;
        private Quad _bottom;
        private Quad _bottom_right;

        public void OnMove(float dx, float dy)
        {
            _top_left.V0.X += dx;
            _top_left.V0.Y += dy;
            _top_left.V1.X += dx;
            _top_left.V1.Y += dy;
            _top_left.V2.X += dx;
            _top_left.V2.Y += dy;
            _top_left.V3.X += dx;
            _top_left.V3.Y += dy;
            
            _top.V0.X += dx;
            _top.V0.Y += dy;
            _top.V1.X += dx;
            _top.V1.Y += dy;
            _top.V2.X += dx;
            _top.V2.Y += dy;
            _top.V3.X += dx;
            _top.V3.Y += dy;
            
            _top_right.V0.X += dx;
            _top_right.V0.Y += dy;
            _top_right.V1.X += dx;
            _top_right.V1.Y += dy;
            _top_right.V2.X += dx;
            _top_right.V2.Y += dy;
            _top_right.V3.X += dx;
            _top_right.V3.Y += dy;
            
            _left.V0.X += dx;
            _left.V0.Y += dy;
            _left.V1.X += dx;
            _left.V1.Y += dy;
            _left.V2.X += dx;
            _left.V2.Y += dy;
            _left.V3.X += dx;
            _left.V3.Y += dy;
            
            _center.V0.X += dx;
            _center.V0.Y += dy;
            _center.V1.X += dx;
            _center.V1.Y += dy;
            _center.V2.X += dx;
            _center.V2.Y += dy;
            _center.V3.X += dx;
            _center.V3.Y += dy;
            
            _right.V0.X += dx;
            _right.V0.Y += dy;
            _right.V1.X += dx;
            _right.V1.Y += dy;
            _right.V2.X += dx;
            _right.V2.Y += dy;
            _right.V3.X += dx;
            _right.V3.Y += dy;
            
            _bottom_left.V0.X += dx;
            _bottom_left.V0.Y += dy;
            _bottom_left.V1.X += dx;
            _bottom_left.V1.Y += dy;
            _bottom_left.V2.X += dx;
            _bottom_left.V2.Y += dy;
            _bottom_left.V3.X += dx;
            _bottom_left.V3.Y += dy;
            
            _bottom.V0.X += dx;
            _bottom.V0.Y += dy;
            _bottom.V1.X += dx;
            _bottom.V1.Y += dy;
            _bottom.V2.X += dx;
            _bottom.V2.Y += dy;
            _bottom.V3.X += dx;
            _bottom.V3.Y += dy;
            
            _bottom_right.V0.X += dx;
            _bottom_right.V0.Y += dy;
            _bottom_right.V1.X += dx;
            _bottom_right.V1.Y += dy;
            _bottom_right.V2.X += dx;
            _bottom_right.V2.Y += dy;
            _bottom_right.V3.X += dx;
            _bottom_right.V3.Y += dy;
            
        }

        public void Resize(int width, int height)
        {
            
            BuildQuads();
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawQuad(_texture, ref _top_left);
            canvas.DrawQuad(_texture, ref _top);
            canvas.DrawQuad(_texture, ref _top_right);
            canvas.DrawQuad(_texture, ref _left);
            canvas.DrawQuad(_texture, ref _right);
            canvas.DrawQuad(_texture, ref _bottom_left);
            canvas.DrawQuad(_texture, ref _bottom);
            canvas.DrawQuad(_texture, ref _bottom_right);

            if (FillMode == CenterFillMode.Tile || FillMode == CenterFillMode.Stretch)
            {
                if (FillMode == CenterFillMode.Stretch)
                {
                    canvas.DrawQuad(_texture, ref _center);    
                }
                else
                {
                    _tiledCenterSprite.Draw(canvas, _rect.X1 + _left_margin, _rect.Y1 + _top_margin);
                }
            }
        }

        private void RebuildCenterSprite()
        {
            var centerRegion = new RectF(
                _left_margin, 
                _top_margin, 
                _texture.Width - _right_margin, _texture.Height - _bottom_margin);

            _centerTileTexture?.Dispose();
            
            /*_centerTileTexture = Game.Instance.ContentManager.CreateTexture(
                this._texture,
                (int) centerRegion.X1,
                (int) centerRegion.Y1,
                (int) centerRegion.Width,
                (int) centerRegion.Height);*/

            _centerTileTexture =
                Game.Instance.ContentManager.CreateTexture((int) centerRegion.Width, (int) centerRegion.Height, true, false, Color.White);
            
            _texture.BlitTo(_centerTileTexture, 
                (int) centerRegion.X1, 
                (int) centerRegion.Y1, 
                (int) centerRegion.Width, 
                (int) centerRegion.Height);

            
            _tiledCenterSprite = new TiledSprite(
                _centerTileTexture, 
                _rect.Width - _left_margin - _right_margin, 
                _rect.Height - _top_margin - _bottom_margin);
        }

        private RectF _rect;
    }
}