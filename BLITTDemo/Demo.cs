using System.Xml;
using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo : Scene
    {
        private Image image;
        private Image dyn_image;
        private Image target;
        private float timer;
        private RandomEx random;

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
          
            
            if (Keyboard.Pressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Keyboard.Pressed(Key.B))
            {
                Canvas.Resize(1024, 768);
            }
            else if (Keyboard.Pressed(Key.S))
            {
                Canvas.Resize(800, 600);
            }

            if (Keyboard.Pressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }

            if(Keyboard.Down(Key.Left))
            {
                Canvas.Translate(-5f, 0);
            }
            else if(Keyboard.Down(Key.Right))
            {
                Canvas.Translate(5f, 0);
            }
            
            
        }

        public override void Draw()
        {

            Canvas.SetTint(Color.Orange);
            Canvas.Draw(image, Canvas.CenterX, Canvas.CenterY);
            
            Canvas.SetTint(Color.Blue);
            Canvas.FillRect(10,10, 100, 100);
            
            Canvas.SetTint(Color.White);
            Canvas.FillCircle(200, 200, 50);

            Canvas.Draw(target, 200, 200);
            
            Canvas.Draw(dyn_image, 400, 400);

            
        }
    }
}