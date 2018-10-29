using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Graphics
{
    public class RenderTarget
    {
        internal FrameBuffer Handle => handle;
        //public Texture2D[] Textures => textures;
        //public Texture2D Texture => textures[0];

        //private Texture2D[] textures;
        private FrameBuffer handle;

        internal RenderTarget(/*Texture2D[] textures*/ int width, int height)
        {
            /*this.textures = textures;

            var attachments = new Texture[textures.Length];

            for(var i = 0; i < textures.Length; ++i)
            {
                attachments[i] = textures[i].InternalTexture;
            }*/

            this.handle = new FrameBuffer(width, height, TextureFormat.BGRA8);
        }
    }
}
