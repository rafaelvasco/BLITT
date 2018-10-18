
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        private const int MAX_QUADS = 10;

        private static ShaderProgram texq_base_shader;
        private static Texture2D current_texture;
        private static VertexPCT[] texq_vertices;
        private static ushort[] texq_indices;
        private static IndexBuffer texq_idx_buffer;
        private static int texq_vertex_idx;
        private static int texq_count;


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

            var vidx = texq_vertex_idx;

            fixed (VertexPCT* vertex_ptr = texq_vertices)
            {
                *(vertex_ptr + vidx++) = new VertexPCT(x, y, u, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y, u2, v, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x + w, y + h, u2, v2, col);
                *(vertex_ptr + vidx++) = new VertexPCT(x, y + h, u, v2, col);
            }

            unchecked
            {
                texq_vertex_idx += 4;
                texq_count++;
            }
        }

        private static void FlushTexturedQuads()
        {
            if(texq_vertex_idx == 0)
            {
                return;
            }

            var vertex_buffer = new TransientVertexBuffer(texq_vertex_idx, VertexPCT.Layout);

            fixed (void* v = texq_vertices)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)texq_vertex_idx * 20);
            }

            texq_base_shader.SetTexture(current_texture, "texture_2d");

            Bgfx.SetRenderState(cur_render_group.RenderState);

            Bgfx.SetIndexBuffer(texq_idx_buffer, 0, texq_count * 6);

            Bgfx.SetVertexBuffer(vertex_buffer, 0, texq_vertex_idx);

            Bgfx.Submit(cur_render_group.Id, texq_base_shader.Program);

            texq_vertex_idx = 0;
            texq_count = 0;
        }

        private static void InitTexQuadRenderResources()
        {
            texq_vertices = new VertexPCT[MAX_QUADS * 4];

            texq_indices = new ushort[MAX_QUADS * 6];

            ushort indice_i = 0;

            for (int i = 0; i < texq_indices.Length; i += 6, indice_i += 4)
            {
                texq_indices[i + 0] = (ushort)(indice_i + 0);
                texq_indices[i + 1] = (ushort)(indice_i + 1);
                texq_indices[i + 2] = (ushort)(indice_i + 2);
                texq_indices[i + 3] = (ushort)(indice_i + 0);
                texq_indices[i + 4] = (ushort)(indice_i + 2);
                texq_indices[i + 5] = (ushort)(indice_i + 3);
            }

            texq_idx_buffer = new IndexBuffer(MemoryBlock.FromArray(texq_indices));
        }
    }
}
