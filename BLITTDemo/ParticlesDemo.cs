using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameObjects;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using BLITTEngine.Temporal;

namespace BLITTDemo
{
    public class ParticlesDemo : Scene
    {
        private Size size;
        private Sprite trail;
        private ParticleEmitter emitter;
        
        private float x = 100.0f, y = 100.0f, dx, dy;
        private const float speed = 130;
        private const float friction = 0.98f;
        private RandomEx random;

        public override void Init()
        {
            size = Game.ScreenSize;

            trail = new Sprite(Content.GetTexture2D("particles"), 32, 32, 32, 32)
            {
                BlendMode = BlendMode.AlphaAdd
            };

            trail.SetOrigin(0.5f, 0.5f);

            var particles_info = new ParticleEmitterInfo()
            {
                MaxParticles = 500,
                Emission = 500,
                Direction = 0,
                Spread = 180,
                GravityMax = 50,
                GravityMin = 2,
                ParticleLifeMin = 0.3f,
                ParticleLifeMax = 0.3f,
                LifeTime = -1.0f,
                RadialAccelMax = 0,
                RadialAccelMin = 0,
                Relative = false,
                SizeStart = 1f,
                SizeEnd = 1f,
                SizeVariation = 0,
                SpeedMax = 50,
                SpeedMin = 50,
                SpinEnd = 0,
                SpinStart = 0,
                SpinVariation = 0,
                TangentialAccelMax = 0,
                TangentialAccelMin = 0,
                ColorStart = Color.Cyan,
                ColorEnd = Color.Fuschia.WithAlpha(0.0f),
                ColorVariation = 0f
            };

            emitter = new ParticleEmitter(particles_info, trail);
            emitter.MoveTo(x, y);
            emitter.Fire();

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

            emitter.MoveTo(x, y);
            emitter.Update(dt);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Begin();
            
            emitter.Render(canvas);

            canvas.End();
        }
    }
}
