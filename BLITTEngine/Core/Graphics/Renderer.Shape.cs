
using BLITTEngine.Numerics;

namespace BLITTEngine.Core.Graphics
{
    internal static unsafe partial class Renderer
    {
        private const int MAX_SHAPE_VERTICES = 100;

        private static VertexPCT[] shape_vertices;
        private static int shape_vertex_idx;
        private static Texture2D shape_tex;

        private static void InitShapeRenderResources()
        {
            var shape_pixmap = new Pixmap(1,1);
            shape_pixmap.Fill(Color.White);

            shape_tex = new Texture2D(shape_pixmap);

            shape_pixmap.Dispose();

            shape_vertices = new VertexPCT[MAX_SHAPE_VERTICES];
        }
    }
}
