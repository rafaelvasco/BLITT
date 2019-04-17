using BLITTEngine;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.GamePad;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameToolkit;

namespace BLITTDemo
{
    // BLITT DEMO 1 - Core low level features: Quad Rendering, Input and Audio
    public class Demo1 : Scene
    {
        private Texture2D ball_texture;

        private Texture2D bg_texture;
        
        private Quad ball_quad;
        
        private Quad bg_quad;

        private Effect bump_sfx;

        private Font font1;
        
        private readonly float friction = 0.87f;

        private Song song;

        private Song song2;

        private readonly float speed = 10;

        private float t;

        private SpriteText ui_text;

        private float x = 100.0f, y = 100.0f, dx, dy;

        public override void Load()
        {
            bump_sfx = Content.Get<Effect>("menu");

            song = Content.Get<Song>("mus1");
            song2 = Content.Get<Song>("mus2");

            font1 = Content.Get<Font>("default_font2");

            ui_text = new SpriteText(font1, "Hello World!\nThis is Blitt Engine !\nNice to meet you!")
            {
                LetterSpacing = 1,
                Proportion = 1f,
                Scale = 0.5f,
                LineSpacing = 0
            };


            ball_texture = Content.Get<Texture2D>("particles");
            ball_quad.Blend = BlendMode.AlphaBlend;

            ball_quad.V0.X = x - 16;
            ball_quad.V0.Y = y - 16;
            ball_quad.V1.X = x + 16;
            ball_quad.V1.Y = y - 16;
            ball_quad.V2.X = x + 16;
            ball_quad.V2.Y = y + 16;
            ball_quad.V3.X = x - 16;
            ball_quad.V3.Y = y + 16;

            ball_quad.V0.Tx = 96.0f / 128.0f;
            ball_quad.V0.Ty = 64.0f / 128.0f;
            ball_quad.V0.Col = 0xFF00A0FF;
            ball_quad.V1.Tx = 128.0f / 128.0f;
            ball_quad.V1.Ty = 64.0f / 128.0f;
            ball_quad.V1.Col = 0xFF00A0FF;
            ball_quad.V2.Tx = 128.0f / 128.0f;
            ball_quad.V2.Ty = 96.0f / 128.0f;
            ball_quad.V2.Col = 0xFF00A0FF;
            ball_quad.V3.Tx = 96.0f / 128.0f;
            ball_quad.V3.Ty = 96.0f / 128.0f;
            ball_quad.V3.Col = 0xFF00A0FF;

            bg_texture = Content.Get<Texture2D>("bg3");
            bg_texture.Filtered = true;
            bg_texture.Tiled = true;
            bg_quad.Blend = BlendMode.AlphaBlend;

            bg_quad.V0.X = 0;
            bg_quad.V0.Y = 0;
            bg_quad.V0.Col = 0xFFFFFFFF;
            bg_quad.V1.X = Canvas.Width;
            bg_quad.V1.Y = 0;
            bg_quad.V1.Col = 0xFFFFFFFF;
            bg_quad.V2.X = Canvas.Width;
            bg_quad.V2.Y = Canvas.Height;
            bg_quad.V2.Col = 0xFFFFFFFF;
            bg_quad.V3.X = 0;
            bg_quad.V3.Y = Canvas.Height;
            bg_quad.V3.Col = 0xFFFFFFFF;

            bg_quad.V0.Tx = 0;
            bg_quad.V0.Ty = 0;
            bg_quad.V1.Tx = Canvas.Width / 64f;
            bg_quad.V1.Ty = 0;
            bg_quad.V2.Tx = Canvas.Width / 64f;
            bg_quad.V2.Ty = Canvas.Height / 64f;
            bg_quad.V3.Tx = 0;
            bg_quad.V3.Ty = Canvas.Height / 64f;
        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.KeyPressed(Key.P)) Canvas.SaveScreenShot(@"C:\Users\rafae\Desktop\blitt_ss.png");

            if (Input.KeyDown(Key.Escape) || Input.ButtonPressed(GamepadButton.Back)) Game.Quit();

            if (Input.KeyPressed(Key.D1)) MediaPlayer.Play(song);

            if (Input.KeyPressed(Key.D2)) MediaPlayer.Play(song2);

            /* if (Control.KeyPressed(Key.Add))
            {
                MediaPlayer.AddSongVolume(2);
            }

            if (Control.KeyPressed(Key.Subtract))
            {
                MediaPlayer.AddSongVolume(-2);
            }*/

            if (Input.KeyPressed(Key.F11)) Game.ToggleFullscreen();

            var movement_gamepad = Input.LeftThumbstickAxis;

            if (Input.KeyDown(Key.Left)) dx -= speed;

            if (Input.KeyDown(Key.Right)) dx += speed;

            if (Input.KeyDown(Key.Up)) dy -= speed;

            if (Input.KeyDown(Key.Down)) dy += speed;

            // UPDATE BACKGROUND
            t += 0.01f;


            bg_quad.V0.Tx = t;
            bg_quad.V0.Ty = t;
            bg_quad.V1.Tx = t + Canvas.Width / 64f;
            bg_quad.V1.Ty = t;
            bg_quad.V2.Tx = t + Canvas.Width / 64f;
            bg_quad.V2.Ty = t + Canvas.Height / 64f;
            bg_quad.V3.Tx = t;
            bg_quad.V3.Ty = t + Canvas.Height / 64f;

            if (t > 1.0f) t = 0;


            // UPDATE BALL

            dx += speed * movement_gamepad.X;
            dy += speed * movement_gamepad.Y;

            dx *= friction;
            dy *= friction;

            if (dx > 0.05f && dx < 0.05f || dx < 0 && dx > -0.05f) dx = 0;

            if (dy > 0.05f && dy < 0.05f || dy < 0.05f && dy > -0.05f) dy = 0;

            x += dx;
            y += dy;

            if (x > Canvas.Width - 16)
            {
                x = Canvas.Width - 16;
                dx = -dx;
                PlayBump();
            }
            else if (x < 16)
            {
                x = 16;
                dx = -dx;
                PlayBump();
            }

            if (y > Canvas.Height - 16)
            {
                y = Canvas.Height - 16;
                dy = -dy;
                PlayBump();
            }
            else if (y < 16)
            {
                y = 16;
                dy = -dy;
                PlayBump();
            }

            ball_quad.V0.X = x - 16;
            ball_quad.V0.Y = y - 16;
            ball_quad.V1.X = x + 16;
            ball_quad.V1.Y = y - 16;
            ball_quad.V2.X = x + 16;
            ball_quad.V2.Y = y + 16;
            ball_quad.V3.X = x - 16;
            ball_quad.V3.Y = y + 16;
        }

        private void PlayBump()
        {
            MediaPlayer.Play(bump_sfx, 0f, RandomEx.Range(0.1f, 1.0f));
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            canvas.DrawQuad(bg_texture, ref bg_quad);

            canvas.DrawQuad(ball_texture, ref ball_quad);
            
            ui_text.Draw(canvas, 0, 0);
            
            canvas.DrawText(20, 100, $"Draw Calls: {canvas.MaxDrawCalls}", Color.White, 0.25f);
            
        }
    }
}