using System.Runtime.InteropServices;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Quad
    {
        public Vertex2D V0;
        public Vertex2D V1;
        public Vertex2D V2;
        public Vertex2D V3;

        public BlendMode Blend;

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Quad));

        public float Width => Calc.Abs(V1.X - V0.X);
        public float Height => Calc.Abs(V2.Y - V1.Y);


        public Quad(Texture2D texture, RectF src_rect = default, RectF dest_rect = default)
        {
            this.V0 = new Vertex2D();
            this.V1 = new Vertex2D();
            this.V2 = new Vertex2D();
            this.V3 = new Vertex2D();

            this.Blend = BlendMode.AlphaBlend;
            
            if (texture == null) return;

            float ax, ay, bx, by;

            float dest_x1, dest_y1, dest_x2, dest_y2;
            
            if (src_rect.IsEmpty)
            {
                src_rect = RectF.FromBox(0, 0, texture.Width, texture.Height);
                
                ax = 0;
                ay = 0;
                bx = 1;
                by = 1;
            }
            else
            {
                float inv_tex_w = 1.0f / texture.Width;
                float inv_tex_h = 1.0f / texture.Height;
                
                ax = src_rect.X1 * inv_tex_w;
                ay = src_rect.Y1 * inv_tex_h;
                bx = src_rect.X2 * inv_tex_w;
                by = src_rect.Y2 * inv_tex_h;
            }

            if (dest_rect.IsEmpty)
            {
                dest_x1 = 0;
                dest_y1 = 0;
                dest_x2 = src_rect.Width;
                dest_y2 = src_rect.Height;
            }
            else
            {
                dest_x1 = dest_rect.X1;
                dest_y1 = dest_rect.Y1;
                dest_x2 = dest_rect.X2;
                dest_y2 = dest_rect.Y2;
            }
            
            this.V0.X = dest_x1;
            this.V0.Y = dest_y1;
            this.V0.Tx = ax;
            this.V0.Ty = ay;
            this.V0.Col = 0xFFFFFFFF;
            
            this.V1.X = dest_x2;
            this.V1.Y = dest_y1;
            this.V1.Tx = bx;
            this.V1.Ty = ay;
            this.V1.Col = 0xFFFFFFFF;
            
            this.V2.X = dest_x2;
            this.V2.Y = dest_y2;
            this.V2.Tx = bx;
            this.V2.Ty = by;
            this.V2.Col = 0xFFFFFFFF;
            
            this.V3.X = dest_x1;
            this.V3.Y = dest_y2;
            this.V3.Tx = ax;
            this.V3.Ty = by;
            this.V3.Col = 0xFFFFFFFF;
        }

        public RectF GetRegionRect(Texture2D texture)
        {
            return new RectF(V0.Tx * texture.Width, V0.Ty * texture.Height, V2.X * texture.Width, V2.Y * texture.Height);
        }
    }
}