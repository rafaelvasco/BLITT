using System.Xml;
using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Input.Mouse;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo : Scene
    {
        private Image image;
        private Image dyn_image;
        private Image target;
        private RandomEx random;
        private float x = Canvas.CenterX;
        private float y = Canvas.CenterY;
        private float dx = 2.0f;
        private float dy = 2.0f;

        public override void Init()
        {
            image = Content.Get<Image>("ball");

            target = Content.CreateImage(320, 240, is_draw_target: true);

            Canvas.BeginTarget(target);

            Canvas.SetTint(Color.White);
            Canvas.FillRect(0, 0, target.Width, target.Height);

            Canvas.SetTint(Color.Red);
            Canvas.FillRect(0, 0, target.Width/2, target.Height/2);
            Canvas.FillRect(target.Width / 2, target.Height / 2, target.Width / 2, target.Height / 2);
            
            Canvas.SetTint(Color.White);
            Canvas.Draw(image, new Rectangle(0, 0, 128, 128) );

            Canvas.EndTarget();

            dyn_image = Content.CreateImage(320, 240, is_draw_target: true);
            
            dyn_image.Fill(Color.Gold);

            random = new RandomEx();

        }

        public override void Update(float dt)
        {

            x += dx;
            y += dy;

            if (x > 540)
            {
                x = 540;
                dx *= -1;
            }
            else if (x < 125)
            {
                x = 125;
                dx *= -1;
            }
            
            if (y > 380)
            {
                y = 380;
                dy *= -1;
            }
            else if (y < 125)
            {
                y = 125;
                dy *= -1;
            }
            
            if (Control.KeyPressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Control.KeyPressed(Key.B))
            {
                Canvas.Resize(1024, 768);
            }
            else if (Control.KeyPressed(Key.S))
            {
                Canvas.Resize(800, 600);
            }

            if (Control.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }

            if(Control.KeyDown(Key.Left))
            {
                Canvas.Translate(-5f, 0);
            }
            else if(Control.KeyDown(Key.Right))
            {
                Canvas.Translate(5f, 0);
            }

            if (Control.MousePressed(MouseButton.Left))
            {
                Game.ToggleFullscreen();
            }
        }

        public override void Draw()
        {

            
            
            Canvas.SetTint(Color.Blue);
            Canvas.FillRect(10,10, 100, 100);
            
            Canvas.SetTint(Color.White);
            Canvas.FillCircle(200, 200, 50);

            Canvas.Draw(target, 200, 200);
            
            Canvas.Draw(dyn_image, 400, 400);
            
            Canvas.SetTint(Color.Orange);
            Canvas.Draw(image, x, y);

            
        }
    }
}