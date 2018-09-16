using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo2 : Scene
    {
        private Image image;
        
        public override void Init()
        {
            image = Content.Get<Image>("BG7");
        }

        public override void Update(float dt)
        {
            if (Control.KeyPressed(Key.Escape))
            {
                Game.Quit();
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
        }

        public override void Draw()
        {
            Canvas.Draw(image, Canvas.CenterX, Canvas.CenterY);
        }
    }
}