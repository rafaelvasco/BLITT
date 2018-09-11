using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo : Scene
    {
        private Image image;
        private Image target;

        public override void Init()
        {
            image = Content.Get<Image>("ball");

            target = Content.CreateImage(320, 240, is_draw_target: true);

            Canvas.Begin(target);
            Canvas.Clear();

            Canvas.SetTint(Color.White);
            Canvas.FillRect(0, 0, target.Width, target.Height);

            Canvas.SetTint(Color.Red);
            Canvas.FillRect(0, 0, target.Width/2, target.Height/2);
            Canvas.FillRect(target.Width / 2, target.Height / 2, target.Width / 2, target.Height / 2);

            Canvas.End();

        }

        public override void Update(float dt)
        {
            if (Keyboard.Pressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Keyboard.Pressed(Key.B))
            {
                Screen.Resize(1024, 768);
            }
            else if (Keyboard.Pressed(Key.S))
            {
                Screen.Resize(800, 600);
            }

            if (Keyboard.Pressed(Key.F11))
            {
                Screen.ToggleFullscreen();
            }
        }

        public override void Draw()
        {
            Canvas.Begin();

            Canvas.SetTint(Color.Orange);
            Canvas.Draw(image, Screen.CenterX, Screen.CenterY);
            
            Canvas.SetTint(Color.Blue);
            Canvas.FillRect(10,10, 100, 100);
            
            Canvas.SetTint(Color.White);
            Canvas.FillCircle(200, 200, 50);

            Canvas.Draw(target, 200, 200);

            Canvas.End();
            
        }
    }
}