using BLITTEngine;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Input;
using BLITTEngine.Core.Input.GamePad;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Resources;

namespace BLITTDemo
{
    // BLITT DEMO 1 - Core low level features: Quad Rendering, Input and Audio
    public class Demo1 : Scene
    {
        private Quad quad;

        private Effect bump_sfx;

        private Song song;

        private Song song2;

        private float x = 100.0f, y = 100.0f, dx, dy;

        private float speed = 10;
        private float friction = 0.87f;

        public override void Load()
        {
            bump_sfx = Content.GetEffect("menu");

            song = Content.GetSong("mus1");
            song2 = Content.GetSong("mus2");

            song.FadeMs = 1000;
            song2.FadeMs = 1000;

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

        public override void Init()
        {
        }

        public override void End()
        {

        }

        public override void Update(float dt)
        {
            if(Control.KeyDown(Key.Escape) || Control.ButtonPressed(GamepadButton.Back))
            {
                Game.Quit();
            }

            if (Control.KeyPressed(Key.D1))
            {
                MediaPlayer.PlaySong(song);
            }

            if (Control.KeyPressed(Key.D2))
            {
                MediaPlayer.PlaySong(song2);
            }

            if (Control.KeyPressed(Key.Add))
            {
                MediaPlayer.AddSongVolume(2);
            }

            if (Control.KeyPressed(Key.Subtract))
            {
                MediaPlayer.AddSongVolume(-2);
            }

            if(Control.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }

            var movement_gamepad = Control.LeftThumbstickAxis;

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



            dx += speed * movement_gamepad.X;
            dy += speed * movement_gamepad.Y;

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
                PlayAudio();
            }
            else if(x < 16)
            {
                x = 16;
                dx = -dx;
                PlayAudio();
            }

            if(y > Canvas.Height - 16)
            {
                y = Canvas.Height - 16;
                dy = -dy;
                PlayAudio();
            }
            else if(y < 16)
            {
                y = 16;
                dy = -dy;
                PlayAudio();
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

        private void PlayAudio()
        {
            MediaPlayer.PlayEffectEx(bump_sfx, volume: 40, pan: (x - 320)/320);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Begin();

            canvas.RenderQuad(ref quad);

            canvas.End();
        }
    }
}