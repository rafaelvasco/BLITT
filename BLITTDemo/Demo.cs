using System;
using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo : Scene
    {
        private Image image;

        public override void Init()
        {
            image = Content.Get<Image>("ball");
            
            Canvas.SetBlitSource(image);
        }

        public override void Update(float dt)
        {
            if (Keyboard.Pressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Keyboard.Down(Key.F11))
            {
                Console.WriteLine("TOGGLE FS");
                //Screen.ToggleFullscreen();
            }
        }

        public override void Draw()
        {
            Canvas.SetTint(Color.Orange);
            Canvas.Blit(Screen.CenterX, Screen.CenterY);
            
            Canvas.SetTint(Color.Blue);
            Canvas.FillRect(10,10, 100, 100);
            
            Canvas.SetTint(Color.White);
            Canvas.FillCircle(200, 200, 50);
            
        }
    }
}