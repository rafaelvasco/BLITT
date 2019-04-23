using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class TiledSprite
    {
        public TiledSprite(Texture2D texture, float width, float height)
        {
            _quad = new Quad
            {
                V0 = {Col = 0xFFFFFFFF}, V1 = {Col = 0xFFFFFFFF}, V2 = {Col = 0xFFFFFFFF}, V3 = {Col = 0xFFFFFFFF}
            };


            SetTexture(texture);
            SetSize(width, height);
        }

        public void SetTexture(Texture2D texture)
        {
            _tile_width = texture.Width;
            _tile_height = texture.Height;
            
            _texture = texture;
            _texture.Tiled = true;
            
            SetSize(_width, _height);
        }

        public void SetSize(float width, float height)
        {
            _width = width;
            _height = height;
            
            _quad.V0.X = 0;
            _quad.V0.Y = 0;
            _quad.V1.X = _width;
            _quad.V1.Y = 0;
            _quad.V2.X = _width;
            _quad.V2.Y = _height;
            _quad.V3.X = 0;
            _quad.V3.Y = _height;

            _quad.V0.Tx = 0;
            _quad.V0.Ty = 0;
            _quad.V1.Tx = _width / _tile_width;
            _quad.V1.Ty = 0;
            _quad.V2.Tx = _width / _tile_width;
            _quad.V2.Ty = _height / _tile_height;
            _quad.V3.Tx = 0;
            _quad.V3.Ty = _height / _tile_height;
            
        }

        public void SetScrollSpeed(float sx, float sy)
        {
            _scroll_sx = sx;
            _scroll_sy = sy;
        }

        public void Update()
        {
            _tx += _scroll_sx;
            _ty += _scroll_sy;
            
            _quad.V0.Tx = _tx;
            _quad.V0.Ty = _ty;
            _quad.V1.Tx = _tx + _width / _tile_width;
            _quad.V1.Ty = _ty;
            _quad.V2.Tx = _tx + _width / _tile_width;
            _quad.V2.Ty = _ty + _height / _tile_height;
            _quad.V3.Tx = _tx;
            _quad.V3.Ty = _ty + _height / _tile_height;

           /* if (_tx > 1.0f)
            {
                _tx = 0.0f;
            }

            if (_ty > 1.0f)
            {
                _ty = 0.0f;
            }*/
             
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            _quad.V0.X = x;
            _quad.V0.Y = y;
            _quad.V1.X = x + _width;
            _quad.V1.Y = y;
            _quad.V2.X = x + _width;
            _quad.V2.Y = y + _height;
            _quad.V3.X = x;
            _quad.V3.Y = y + _height;
            
            canvas.DrawQuad(_texture, ref _quad);
        }
        
        private Quad _quad;
        private Texture2D _texture;
        
        private float _tx;
        private float _ty;

        private float _scroll_sx;
        private float _scroll_sy;
        
        private float _width;
        private float _height;
        private float _tile_width;
        private float _tile_height;

    }
}