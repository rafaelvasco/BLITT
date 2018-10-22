
using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        public static void AddQuad(Texture2D texture, float x, float y, in Quad quad)
        {
            if (current_texture != texture)
            {
                Flush();

                current_texture = texture;
            }

            float w = quad.W;
            float h = quad.H;
            float u = quad.U;
            float v = quad.V;
            float u2 = quad.U2;
            float v2 = quad.V2;
            uint col = quad.Col;

            var vidx = quad_vtx_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, u, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, u2, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, u2, v2, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, u, v2, col);
            }

            unchecked
            {
                quad_vtx_idx += 4;
                quad_count++;
            }
        }

        private static void InitTexQuadRenderResources()
        {
            quad_vertices = new VertexPCT[MAX_QUADS * 4];

            quad_indices = new ushort[MAX_QUADS * 6];

            ushort indice_i = 0;

            for (int i = 0; i < quad_indices.Length; i += 6, indice_i += 4)
            {
                quad_indices[i + 0] = (ushort)(indice_i + 0);
                quad_indices[i + 1] = (ushort)(indice_i + 1);
                quad_indices[i + 2] = (ushort)(indice_i + 2);
                quad_indices[i + 3] = (ushort)(indice_i + 0);
                quad_indices[i + 4] = (ushort)(indice_i + 2);
                quad_indices[i + 5] = (ushort)(indice_i + 3);
            }

            quad_idx_buffer = new IndexBuffer(MemoryBlock.FromArray(quad_indices));
        }
    }
}
