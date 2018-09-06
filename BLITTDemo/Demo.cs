using System;
using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;

namespace BLITTDemo
{
    public class Demo : Scene
    {
        public override void Update(float dt)
        {
            if (Keyboard.Pressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Keyboard.Pressed(Key.F11))
            {
                //Screen.ToggleFullscreen();
            }
        }

        public override void Draw(Canvas canvas)
        {
        }
    }
}