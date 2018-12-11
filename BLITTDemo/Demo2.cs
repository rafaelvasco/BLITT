using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Input;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameObjects;
using BLITTEngine.Temporal;

namespace BLITTDemo
{
    public class Demo2 : Scene
    {
        private Size size;

        private Sprite sprite;
        private Sprite trail;

        private ParticleEmitter emitter;

        private Timer timer;

        private float x = 100.0f, y = 100.0f, dx, dy;

        private float speed = 130;
        private float friction = 0.98f;
        private RandomEx random;
        private int color_task;

        public override void Load()
        {
        }

        public override void Init()
        {
            random = new RandomEx();

            // BLITT DEMO 1 - Using Game Objects
            size = Game.ScreenSize;

            sprite = new Sprite(Content.GetTexture2D("particles"), 96, 64, 32, 32);

            sprite.SetColor(Color.Cyan);
            sprite.SetOrigin(0.5f, 0.5f);

            trail = new Sprite(Content.GetTexture2D("particles"), 32, 32, 32, 32)
            {
                BlendMode = BlendMode.AlphaAdd
            };

            trail.SetOrigin(0.5f, 0.5f);
            trail.SetColor(Color.White);

            var particles_info = new ParticleEmitterInfo()
            {
                MaxParticles = 500,
                Emission = 30,
                Direction = 0,
                Spread = 0,
                GravityMax = 2,
                GravityMin = 2,
                ParticleLifeMin = 0.3f,
                ParticleLifeMax = 0.3f,
                LifeTime = -1.0f,
                RadialAccelMax = 0,
                RadialAccelMin = 0,
                Relative = false,
                SizeStart = 1f,
                SizeEnd = 0.0f,
                SizeVariation = 0,
                SpeedMax = 0,
                SpeedMin = 0,
                SpinEnd = 0,
                SpinStart = 0,
                SpinVariation = 0,
                TangentialAccelMax = 0,
                TangentialAccelMin = 0,
                ColorStart = Color.Red,
                ColorEnd = Color.Green.WithAlpha(0.0f),
                ColorVariation = 00f
            };

            emitter = new ParticleEmitter(particles_info, trail);
            emitter.Fire();

            timer = new Timer();

            //color_task = timer.Every(0.5f, () => sprite.SetColor(random.NextColor()));
        }

        public override void End()
        {
        }

        public override void Update(float dt)
        {
            //timer.Update(dt);

            if (Control.KeyPressed(Key.Escape))
            {
                Game.Quit();
            }

            if (Control.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }
            else if (Control.KeyPressed(Key.C))
            {
                timer.Cancel(color_task);
            }
            else if (Control.KeyPressed(Key.T))
            {
                color_task = timer.Every(0.5f, () => sprite.SetColor(random.NextColor()));
            }
            else if (Control.KeyPressed(Key.D))
            {
                timer.After(1.0f, () => sprite.SetColor(Color.Green));
            }
            else if (Control.KeyPressed(Key.D1))
            {
                Game.ScreenSize = new Size(size.W, size.H);
            }
            else if (Control.KeyPressed(Key.D2))
            {
                Game.ScreenSize = new Size(size.W * 2, size.H * 2);
            }
            else if (Control.KeyPressed(Key.D3))
            {
                Game.ScreenSize = new Size(size.W * 3, size.H * 3);
            }

            if (Control.KeyDown(Key.Left))
            {
                dx -= speed * dt;
            }

            if (Control.KeyDown(Key.Right))
            {
                dx += speed * dt;
            }

            if (Control.KeyDown(Key.Up))
            {
                dy -= speed * dt;
            }

            if (Control.KeyDown(Key.Down))
            {
                dy += speed * dt;
            }

            dx *= friction;
            dy *= friction;

            if ((dx > 0.05f && dx < 0.05f) || (dx < 0 && dx > -0.05f))
            {
                dx = 0;
            }

            if ((dy > 0.05f && dy < 0.05f) || (dy < 0.05f && dy > -0.05f))
            {
                dy = 0;
            }

            x += dx;
            y += dy;

            if (x > Canvas.Width - 16)
            {
                x = Canvas.Width - 16;
                dx = -dx;
            }
            else if (x < 16)
            {
                x = 16;
                dx = -dx;
            }

            if (y > Canvas.Height - 16)
            {
                y = Canvas.Height - 16;
                dy = -dy;
            }
            else if (y < 16)
            {
                y = 16;
                dy = -dy;
            }

            emitter.Info.Emission = (int)((dx * dx + dy * dy) * 2);
            emitter.MoveTo(x, y);
            emitter.Update(dt);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Begin();

            sprite.Render(canvas, x, y);

            emitter.Render(canvas);

            canvas.End();
        }
    }
}