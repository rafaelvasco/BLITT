
using BLITTEngine.Numerics;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        private static Texture2D shape_tex;

        private static void InitShapeRenderResources()
        {
            var shape_pixmap = new Pixmap(1,1);
            shape_pixmap.Fill(Color.White);

            shape_tex = new Texture2D(shape_pixmap);

            shape_pixmap.Dispose();
        }

        public static void AddRect(float x, float y, float w, float h, uint col)
        {

            if (current_texture != shape_tex)
            {
                Flush();

                current_texture = shape_tex;
            }

            var vidx = quad_vtx_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, 1, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, 1, 1, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, 0, 1, col);
            }

            unchecked
            {
                quad_vtx_idx += 4;
                quad_count++;
            }
        }

        public static void AddRect(
            float x,
            float y,
            float w,
            float h,
            uint top_left_col,
            uint top_right_cop,
            uint bottom_left_col,
            uint bottom_right_col)
        {

            if (current_texture != shape_tex)
            {
                Flush();

                current_texture = shape_tex;
            }

            var vidx = quad_vtx_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, 0, 0, top_left_col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, 1, 0, top_right_cop);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, 1, 1, bottom_right_col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, 0, 1, bottom_left_col);
            }

            unchecked
            {
                quad_vtx_idx += 4;
                quad_count++;
            }
        }

        public static void AddLine(float x1, float y1, float x2, float y2, float width, uint col)
        {

            if (current_texture != shape_tex)
            {
                Flush();

                current_texture = shape_tex;
            }

            var vidx = quad_vtx_idx;

            Vector2 pos = new Vector2(x1, y1);
            Vector2 target = new Vector2(x2, y2);

            Vector2 dir = (target - pos).Normalized;

            float half_w = width*0.5f;

            Vector2 left = dir.PerpendicularLeft * half_w;
            Vector2 right = dir.PerpendicularRight * half_w;


            Vector2 a = pos + left;
            Vector2 b = target + left;
            Vector2 c = target + right;
            Vector2 d = pos + right;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(a.X, a.Y, 0, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(b.X, b.Y, 1, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(c.X, c.Y, 1, 1, col);
                *(vertex_ptr + vidx++) = new VertexPCT(d.X, d.Y, 0, 1, col);
            }

            unchecked
            {
                quad_vtx_idx += 4;
                quad_count++;
            }
        }
    }
}
