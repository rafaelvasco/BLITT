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

        public override void Init()
        {
            texture = Content.GetTexture2D("ship");

            tw = texture.Width;
            th = texture.Height;
        }

        public override void Update(float dt)
        {
            x += dx;
            y += dy;

            if(x > Canvas.Width - tw)
            {
                dx = -dx;
                x = Canvas.Width - tw;
            }
            else if (x < 0)
            {
                dx = -dx;
                x = 0;
            }

            if (y > Canvas.Height - th)
            {
                dy = -dy;
                y = Canvas.Height - th;
            }
            else if (y < 0)
            {
                dy = -dy;
                y = 0;
            }
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Clear(Color.CornflowerBlue);
            canvas.Draw(texture, x, y);


        }
    }
}