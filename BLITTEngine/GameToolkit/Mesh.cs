using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public enum MeshDisplacementReference
    {
        Node,
        TopLeft,
        Center
    }

    public class Mesh
    {
        public BlendMode BlendMode => _quad.Blend;
        
        public Rect TextureRect => new Rect(_src_x, _src_y, _src_w, _src_h);

        public int Rows => _rows;
        
        public int Cols => _cols;

        public Mesh(int cols, int rows)
        {
            cols = Calc.Max(cols, 2);
            rows = Calc.Max(rows, 2);
            
            _rows = rows;
            _cols = cols;
            _cell_w = _cell_h = 0;
            _quad.Blend = BlendMode.AlphaBlend;
            _mesh_vertices = new Vertex2D[rows * cols];

            for (int i = 0; i < rows * cols; ++i)
            {
                _mesh_vertices[i].X = 0.0f;
                _mesh_vertices[i].Y = 0.0f;
                _mesh_vertices[i].Tx = 0.0f;
                _mesh_vertices[i].Ty = 0.0f;
                _mesh_vertices[i].Col = 0xFFFFFFFF;
            }
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            for (int j = 0; j < _rows - 1; j++)
            {
                for (int i = 0; i < _cols - 1; i++)
                {
                    var idx = j * _cols + i;

                    _quad.V0.Tx = _mesh_vertices[idx].Tx;
                    _quad.V0.Ty = _mesh_vertices[idx].Ty;
                    _quad.V0.X = x + _mesh_vertices[idx].X;
                    _quad.V0.Y = y + _mesh_vertices[idx].Y;
                    _quad.V0.Col = _mesh_vertices[idx].Col;

                    _quad.V1.Tx = _mesh_vertices[idx+1].Tx;
                    _quad.V1.Ty = _mesh_vertices[idx+1].Ty;
                    _quad.V1.X = x + _mesh_vertices[idx+1].X;
                    _quad.V1.Y = y + _mesh_vertices[idx+1].Y;
                    _quad.V1.Col = _mesh_vertices[idx+1].Col;

                    _quad.V2.Tx = _mesh_vertices[idx+_cols+1].Tx;
                    _quad.V2.Ty = _mesh_vertices[idx+_cols+1].Ty;
                    _quad.V2.X = x + _mesh_vertices[idx+_cols+1].X;
                    _quad.V2.Y = y + _mesh_vertices[idx+_cols+1].Y;
                    _quad.V2.Col = _mesh_vertices[idx+_cols+1].Col;

                    _quad.V3.Tx = _mesh_vertices[idx+_cols].Tx;
                    _quad.V3.Ty = _mesh_vertices[idx+_cols].Ty;
                    _quad.V3.X = x + _mesh_vertices[idx+_cols].X;
                    _quad.V3.Y = y + _mesh_vertices[idx+_cols].Y;
                    _quad.V3.Col = _mesh_vertices[idx+_cols].Col;
                    
                    
                    canvas.DrawQuad(_texture, ref _quad);
                }
            }
        }
        
        public void DrawEx(Canvas canvas, float x, float y, float scale_x, float scale_y=-1)
        {
            if (scale_y < 0)
            {
                scale_y = scale_x;
            }
            
            for (int j = 0; j < _rows - 1; j++)
            {
                for (int i = 0; i < _cols - 1; i++)
                {
                    var idx = j * _cols + i;

                    _quad.V0.Tx = _mesh_vertices[idx].Tx;
                    _quad.V0.Ty = _mesh_vertices[idx].Ty;
                    _quad.V0.X = x + _mesh_vertices[idx].X*scale_x;
                    _quad.V0.Y = y + _mesh_vertices[idx].Y*scale_y;
                    _quad.V0.Col = _mesh_vertices[idx].Col;

                    _quad.V1.Tx = _mesh_vertices[idx+1].Tx;
                    _quad.V1.Ty = _mesh_vertices[idx+1].Ty;
                    _quad.V1.X = x + _mesh_vertices[idx+1].X*scale_x;
                    _quad.V1.Y = y + _mesh_vertices[idx+1].Y*scale_y;
                    _quad.V1.Col = _mesh_vertices[idx+1].Col;

                    _quad.V2.Tx = _mesh_vertices[idx+_cols+1].Tx;
                    _quad.V2.Ty = _mesh_vertices[idx+_cols+1].Ty;
                    _quad.V2.X = x + _mesh_vertices[idx+_cols+1].X*scale_x;
                    _quad.V2.Y = y + _mesh_vertices[idx+_cols+1].Y*scale_y;
                    _quad.V2.Col = _mesh_vertices[idx+_cols+1].Col;

                    _quad.V3.Tx = _mesh_vertices[idx+_cols].Tx;
                    _quad.V3.Ty = _mesh_vertices[idx+_cols].Ty;
                    _quad.V3.X = x + _mesh_vertices[idx+_cols].X*scale_x;
                    _quad.V3.Y = y + _mesh_vertices[idx+_cols].Y*scale_y;
                    _quad.V3.Col = _mesh_vertices[idx+_cols].Col;
                    
                    
                    canvas.DrawQuad(_texture, ref _quad);
                    //canvas.SetColor(Color.Red);
                    //canvas.DrawRect(_quad.V0.X, _quad.V0.Y, _quad.V1.X - _quad.V0.X, _quad.V2.Y - _quad.V0.Y);
                }
            }
        }
        
        

        public void Clear(Color color)
        {
            for (int j = 0; j < _rows; ++j)
            {
                for (int i = 0; i < _cols; ++i)
                {
                    _mesh_vertices[j * _cols + i].X = i * _cell_w;
                    _mesh_vertices[j * _cols + i].Y = j * _cell_h;
                    
                    _mesh_vertices[j * _cols + i].Col = color.ABGR;
                }
            }
        }

        public void SetTexture(Texture2D tex)
        {
            _texture = tex;
            SetTextureRect(0, 0, tex.Width, tex.Height);
        }

        
        public void SetTextureRect(float src_x, float src_y, float src_w, float src_h)
        {

            float tw, th;

            _src_x = src_x;
            _src_y = src_y;
            
            _src_w = src_w;
            _src_h = src_h;
            
            if (_texture != null)
            {
                tw = _texture.Width;
                th = _texture.Height;
            }
            else {
                tw = src_w;
                th = src_h;
            }

            _cell_w = src_w / (_cols - 1);
            _cell_h = src_h / (_rows - 1);


            for (int j = 0; j < _rows; ++j)
            {
                for (int i = 0; i < _cols; ++i)
                {

                    _mesh_vertices[j * _cols + i].Tx = (src_x + i * _cell_w) / tw;
                    _mesh_vertices[j * _cols + i].Ty = (src_y + j * _cell_h) / th;
                    
                    _mesh_vertices[j * _cols + i].X = i * _cell_w;
                    _mesh_vertices[j * _cols + i].Y = j * _cell_h;
                    
                }
            }
        }

        public void SetBlendMode(BlendMode blend)
        {
            _quad.Blend = blend;
        }

        public void SetColor(int col, int row, Color color)
        {
            if (row < _rows && col < _cols)
            {
                _mesh_vertices[row * _cols + col].Col = color.ABGR;
            }
        }

        public void SetDisplacement(int col, int row, float dx, float dy, MeshDisplacementReference reference)
        {
            if (row < _rows && col < _cols)
            {
                switch (reference)
                {
                    case MeshDisplacementReference.Node:

                        dx += col * _cell_w;
                        dy += row * _cell_h;

                        break;
                    case MeshDisplacementReference.TopLeft:
                        break;
                    case MeshDisplacementReference.Center:

                        dx += _cell_w * (_cols - 1) / 2;
                        dy += _cell_h * (_rows - 1) / 2;

                        break;
                }

                _mesh_vertices[row * _cols + col].X = dx;
                _mesh_vertices[row * _cols + col].Y = dy;
            }
        }

        public uint GetColor(int col, int row)
        {
            if (row < _rows && col < _cols)
            {
                return _mesh_vertices[row * _cols + col].Col;
            }

            return 0;
        }

        public void GetDisplacement(int col, int row, out float dx, out float dy, MeshDisplacementReference reference)
        {
            dx = 0;
            dy = 0;

            if (row < _rows && col < _cols)
            {
                switch (reference)
                {
                    case MeshDisplacementReference.Node:

                        dx = _mesh_vertices[row * _cols + col].X - col * _cell_w;
                        dy = _mesh_vertices[row * _cols + col].Y - row * _cell_h;

                        break;
                    case MeshDisplacementReference.TopLeft:

                        dx = _mesh_vertices[row * _cols + col].X;
                        dy = _mesh_vertices[row * _cols + col].Y;

                        break;
                    case MeshDisplacementReference.Center:

                        dx = _mesh_vertices[row * _cols + col].X - _cell_w * (_cols-1)/2;
                        dy = _mesh_vertices[row * _cols + col].Y - _cell_h * (_rows-1)/2;

                        break;
                }
            }
        }

        private readonly Vertex2D[] _mesh_vertices;

        private readonly int _rows;

        private readonly int _cols;

        private float _cell_w;

        private float _cell_h;

        private float _src_x;

        private float _src_y;

        private float _src_w;

        private float _src_h;

        private Quad _quad;

        private Texture2D _texture;
    }
}
