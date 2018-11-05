using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    public struct GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;

        public GraphicsInfo(RendererBackend backend, int max_tex_size)
        {
            this.RendererBackend = backend;
            this.MaxTextureSize = max_tex_size;
        }
    }

    internal class TextureInternal
    {
        public readonly Texture Texture
    }

    internal class GraphicsContext
    {
        public readonly GraphicsInfo Info;

        private ShaderProgram current_shader;

        private ShaderProgram base_2d_shader;

        public GraphicsContext()
        {

        }
    }
}
