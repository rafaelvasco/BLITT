
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        private const int MAX_SHAPE_VERTICES = 100;

        private static VertexPCT[] shape_vertices;
        private static int shape_vertex_idx;
        private static int shape_quad_count;
        private static Texture2D shape_tex;

        private static void InitShapeRenderResources()
        {
            var shape_pixmap = new Pixmap(2,2);
            shape_pixmap.Fill(Color.White);

            shape_tex = new Texture2D(shape_pixmap);

            shape_pixmap.Dispose();

            shape_vertices = new VertexPCT[MAX_SHAPE_VERTICES];
        }

        public static void AddRect(float x, float y, float w, float h, uint col)
        {
           
            var vidx = shape_vertex_idx;

            fixed (VertexPCT* vertex_ptr = shape_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, 0, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, 1, 0, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, 1, 1, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, 0, 1, col);
            }

            unchecked
            {
                shape_vertex_idx += 4;
                shape_quad_count++;
            }
        }

        private static void FlushShapes()
        {
            if (shape_vertex_idx == 0)
            {
                return;
            }

            var vertex_buffer = new TransientVertexBuffer(shape_vertex_idx, VertexPCT.Layout);

            fixed (void* v = shape_vertices)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)shape_vertex_idx * 20);
            }

            texq_base_shader.SetTexture(shape_tex, "texture_2d");

            Bgfx.SetRenderState(cur_render_group.RenderState);

            Bgfx.SetIndexBuffer(texq_idx_buffer, 0, shape_quad_count * 6);

            Bgfx.SetVertexBuffer(vertex_buffer, 0, shape_vertex_idx);

            Bgfx.Submit(cur_render_group.Id, texq_base_shader.Program);

            shape_vertex_idx = 0;
            shape_quad_count = 0;
        }
    }
}
