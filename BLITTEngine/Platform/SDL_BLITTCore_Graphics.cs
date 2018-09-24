using BLITTEngine.Foundation;
using BLITTEngine.Graphics;

namespace BLITTEngine.Platform
{
    internal struct DrawProps
    {
        public int ScreenW;
        public int ScreenH;
        public int PixelW;
        public int PixelH;
    }

    internal partial class SDL_BLITTCore
    {
        private uint backbuffer_texture;
        private Pixmap main_target;

        public override Pixmap RenderTarget => main_target;

        private void InitGraphics(int backbuffer_width, int backbuffer_height)
        {
            GL.LoadFunctions();

            GL.Enable(EnableCap.Texture2D);

            GL.GenTextures(1, out backbuffer_texture);

            GL.BindTexture(TextureTarget.Texture2D, backbuffer_texture);

            main_target = new Pixmap(backbuffer_height, backbuffer_height);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, backbuffer_width, backbuffer_height, 0,
                GLPixelFormat.Rgba, GLPixelType.UnsignedByte, main_target.PixelDataPtr);


            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.Src1Alpha, BlendingFactorDest.OneMinusSrc1Alpha);

            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Nearest);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMinFilter.Nearest);
        }

        public override void SubmitRender(ref DrawProps props)
        {
            GL.Viewport(0, 0, main_target.Width * props.PixelW, main_target.Height * props.PixelH);

            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, props.ScreenW, props.ScreenH, GLPixelFormat.Rgba,
                GLPixelType.UnsignedByte, main_target.PixelDataPtr);
            
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2i(0, 1); GL.Vertex2f(-1, -1);
            GL.TexCoord2i(0, 0); GL.Vertex2f(-1, 1);
            GL.TexCoord2i(1, 0); GL.Vertex2f(1, 1);
            GL.TexCoord2i(1, 1); GL.Vertex2f(1, -1);
            GL.End();

            SDL.GL.SwapWindow(window);
        }
    }
}