using BLITTEngine;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameToolkit.UI;

namespace BLITTDemo
{
    public class Demo7 : Scene
    {
        private Gui gui;
        
        public override void Load()
        {

            gui = new Gui();

            var btn1 = gui.Main.AddButton("Click Me");

            btn1.SetPosition(200, 100);
        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update();

            if (Input.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
                
            }

            if (Input.KeyPressed(Key.Escape))
            {
                Game.Quit();
            }
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            gui.Draw(canvas);

            canvas.FillRect(0, 0, canvas.Width, canvas.Height, Color.RoyalBlue);
            
            canvas.DrawText(200, 200, $"MousePos: {Input.MousePos}", Color.White, 0.25f);


        }
    }
}