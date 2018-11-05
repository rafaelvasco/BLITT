using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo1 : Scene
    {
        private Quad quad;

        private float x = 100.0f, y = 100.0f, dx, dy;

        private float speed = 10;
        private float friction = 0.87f;

        public override void Init()
        {
            // BLITT DEMO 1 - Using input, sound and rendering

            quad.Tex = Content.GetTexture2D("particles");
            quad.Blend = BlendMode.AlphaBlend;

            quad.V0.X = x - 16;
            quad.V0.Y = y - 16;
            quad.V1.X = x + 16;
            quad.V1.Y = y - 16;
            quad.V2.X = x + 16;
            quad.V2.Y = y + 16;
            quad.V3.X = x - 16;
            quad.V3.Y = y + 16;

            quad.V0.Tx = 96.0f / 128.0f;
            quad.V0.Ty = 64.0f / 128.0f;
            quad.V0.Col = 0xFF00A0FF;
            quad.V1.Tx = 128.0f / 128.0f;
            quad.V1.Ty = 64.0f / 128.0f;
            quad.V1.Col = 0xFF00A0FF;
            quad.V2.Tx = 128.0f / 128.0f;
            quad.V2.Ty = 96.0f / 128.0f;
            quad.V2.Col = 0xFF00A0FF;
            quad.V3.Tx = 96.0f / 128.0f;
            quad.V3.Ty = 96.0f / 128.0f;
            quad.V3.Col = 0xFF00A0FF;
        }

        public override void Update(float dt)
        {
            if(Control.KeyDown(Key.Escape))
            {
                Game.Quit();
            }

            if(Control.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }

            if(Control.KeyDown(Key.Left))
            {
                dx -= speed;
            }

            if(Control.KeyDown(Key.Right))
            {
                dx += speed;
            }

            if(Control.KeyDown(Key.Up))
            {
                dy -= speed;
            }

            if(Control.KeyDown(Key.Down))
            {
                dy += speed;
            }

            dx *= friction;
            dy *= friction;

            if((dx > 0.05f && dx < 0.05f) || (dx < 0 && dx > -0.05f))
            {
                dx = 0;
            }

            if((dy > 0.05f && dy < 0.05f) || (dy < 0.05f && dy > -0.05f))
            {
                dy = 0;
            }

            x += dx;
            y += dy;

            if(x > Canvas.Width - 16)
            {
                x = Canvas.Width - 16;
                dx = -dx;
            }
            else if(x < 16)
            {
                x = 16;
                dx = -dx;
            }

            if(y > Canvas.Height - 16)
            {
                y = Canvas.Height - 16;
                dy = -dy;
            }
            else if(y < 16)
            {
                y = 16;
                dy = -dy;
            }

            //x = Calc.Floor(x);
            //y = Calc.Floor(y);

            quad.V0.X = (x - 16);
            quad.V0.Y = y - 16;
            quad.V1.X = x + 16;
            quad.V1.Y = y - 16;
            quad.V2.X = x + 16;
            quad.V2.Y = y + 16;
            quad.V3.X = x - 16;
            quad.V3.Y = y + 16;

        }

        public override void Draw(Canvas canvas)
        {
            canvas.Begin();

            canvas.RenderQuad(ref quad);

            canvas.End();
        }
    }
}