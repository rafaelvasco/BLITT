using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Draw;
using BLITTEngine.Numerics;

namespace BLITTDemo
{
    public class Demo1 : Scene
    {
        private float x = 50;
        private float y = 50;

        private float dx = 5;
        private float dy = 5;

        private int tw;
        private int th;


        private Texture2D texture;

        private CanvasView scene_view;
        private CanvasView gui_view;

        public override void Init()
        {
            texture = Content.GetTexture2D("ship");

            tw = texture.Width;
            th = texture.Height;

            scene_view = Canvas.CreateView(ViewOrigin.Center);

            gui_view = Canvas.CreateView(ViewOrigin.TopLeft, blending: Blending.Alpha);

        }

        public override void Update(float dt)
        {
            x += dx;
            y += dy;

            var hcw = Canvas.Width/2;
            var hch = Canvas.Height /2;

            if(x > hcw - tw)
            {
                dx = -dx;
                x = hcw - tw;
            }
            else if (x < -hcw)
            {
                dx = -dx;
                x = -hcw;
            }

            if (y > hch - th)
            {
                dy = -dy;
                y = hch - th;
            }
            else if (y < -hch)
            {
                dy = -dy;
                y = -hch;
            }
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Clear(Color.CornflowerBlue);

            canvas.Begin(scene_view);

            canvas.Draw(texture, x, y);

            canvas.End();

            canvas.Begin(gui_view);

            canvas.Draw(texture, 100, 100);

            canvas.End();




        }
    }
}