
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        private const int MAX_QUADS = 10;

        private static ShaderProgram base_texquad_shader;
        private static Texture2D current_texture;
        private static VertexPCT[] quad_vertices;
        private static ushort[] quad_indices;
        private static IndexBuffer index_buffer;
        private static int vertex_idx;
        private static int quad_count;

        public static void AddQuad(Texture2D texture, float x, float y, in Quad quad)
        {
            if (current_texture != texture)
            {
                FlushTexturedQuads();

                current_texture = texture;
            }

            float w = quad.W;
            float h = quad.H;
            float u = quad.U;
            float v = quad.V;
            float u2 = quad.U2;
            float v2 = quad.V2;
            uint col = quad.Col;

            var vidx = vertex_idx;

            fixed (VertexPCT* vertex_ptr = quad_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, u, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, u2, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, u2, v2, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, u, v2, col);
            }

            unchecked
            {
                vertex_idx += 4;
                quad_count++;
            }
        }

        private static void FlushTexturedQuads()
        {
            var vertex_buffer = new TransientVertexBuffer(vertex_idx, VertexPCT.Layout);

            fixed (void* v = quad_vertices)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)vertex_idx * 20);
            }

            base_texquad_shader.SetTexture(current_texture, "texture_2d");

            Bgfx.SetRenderState(cur_render_group.RenderState);

            Bgfx.SetVertexBuffer(vertex_buffer, 0, vertex_idx);

            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count * 6);

            Bgfx.Submit(cur_render_group.Id, base_texquad_shader.Program);

            vertex_idx = 0;
            quad_count = 0;
        }

        private static void InitializeTexQuadRenderBuffers()
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

            index_buffer = new IndexBuffer(MemoryBlock.FromArray(quad_indices));
        }
    }
}
