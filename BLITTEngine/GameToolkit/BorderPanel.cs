using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class BorderPanel
    {
        public enum FillMode
        {
            Stretch,
            Tile,
            Transparent
        }

        public int LeftMargin { get; set; } = 5;
        public int RightMargin { get; set; } = 5;
        public int TopMargin { get; set; } = 5;
        public int BottomMargin { get; set; } = 5;

        public FillMode EdgesFillMode { get; set; } = FillMode.Stretch;
        public FillMode CenterFillMode { get; set; } = FillMode.Stretch;

        public Origin Origin { get; set; } = Origin.TopLeft;

        public BorderPanel(Texture2D texture)
        {
            _texture = texture;
            
            BuildQuads();
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            
        }

        private void BuildQuads()
        {
            var w = _texture.Width;
            var h = _texture.Height;

            var center_w = w - _left_margin - _right_margin;
            var center_h = h - _top_margin - _bottom_margin;
            
            _top_left = new Quad(_texture, Rect.FromBox(0, 0, _left_margin, _top_margin));
            _top = new Quad(_texture, Rect.FromBox(_left_margin, 0, w - _left_margin - _right_margin, _top_margin));
            _top_right = new Quad(_texture, Rect.FromBox(w -_right_margin, 0, _right_margin, _top_margin));
            _left = new Quad(_texture, Rect.FromBox(0, _top_margin, _left_margin, center_h));
            _center = new Quad(_texture, Rect.FromBox(_left_margin, _top_margin, center_w, center_h));
            _right = new Quad(_texture, Rect.FromBox(w-_right_margin, _top_margin, _right_margin, center_h));
            _bottom_left = new Quad(_texture, Rect.FromBox(0, h - _bottom_margin, _left_margin, _bottom_margin));
            _bottom = new Quad(_texture, Rect.FromBox(_left_margin, h - _bottom_margin, center_w, _bottom_margin));
            _bottom_right = new Quad(_texture, Rect.FromBox(w - _right_margin, h - _bottom_margin, _right_margin, _bottom_margin));
        }

        private Texture2D _texture;

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
        
        
    }
}