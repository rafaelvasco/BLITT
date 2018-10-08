using BLITTEngine.Core.Graphics;
using BLITTEngine.Numerics;
using System.Numerics;

namespace BLITTEngine.Draw
{
    public enum ViewOrigin
    {
        TopLeft,
        Center
    }

    public sealed class Canvas
    {
        private GraphicsDevice gfx;

        public int Width { get; private set; }

        public int Height { get; private set; }

        private CanvasView[] views;
        private byte view_idx = 1;
        private bool ready_to_draw = false;

        internal Canvas(GraphicsDevice graphics_device, int width, int height)
        {
            views = new CanvasView[255];

            gfx = graphics_device;

            this.Width = width;
            this.Height = height;
        }

        public CanvasView CreateView(ViewOrigin origin, RectangleI viewport = default, Blending blending = Blending.Alpha)
        {
            Matrix4x4 view_proj = Matrix4x4.Identity;

            if(viewport.IsEmpty)
            {
                viewport = new RectangleI(0, 0, this.Width, this.Height);
            }

            var hcw = viewport.W/2;
            var hch = viewport.H/2;

            switch (origin)
            {
                case ViewOrigin.TopLeft:

                    view_proj = Matrix4x4.CreateOrthographicOffCenter(
                        left: 0,
                        right: viewport.W,
                        bottom: viewport.H,
                        top: 0,
                        0.0f,
                        1.0f
                    );

                    break;

                case ViewOrigin.Center:

                    view_proj = Matrix4x4.CreateOrthographicOffCenter(
                        left: -hcw,
                        right: hcw,
                        bottom: hch,
                        top: -hch,
                        0.0f,
                        1.0f
                    );

                    break;
            }



            return views[view_idx] = new CanvasView(view_idx++, viewport, view_proj, blending);
        }

        public void Clear(Color color)
        {
            gfx.Clear(in color);
        }

        public void Begin(CanvasView view)
        {
            ready_to_draw = true;

            gfx.Begin(
                view.Id,
                view.RenderState,
                view.ProjectionMatrix,
                view.Viewport
            );
        }

        public void End()
        {
            ready_to_draw = false;

            gfx.End();
        }

        public void Draw(Texture2D texture, float x, float y)
        {
            if (!ready_to_draw)
            {
                return;
            }

            gfx.AddQuad(texture, x, y);
        }
    }
}