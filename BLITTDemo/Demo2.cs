using BLITTEngine;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameToolkit;
using BLITTEngine.GameToolkit.Animation;
using BLITTEngine.GameToolkit.Particles;
using PowerArgs;

namespace BLITTDemo
{
    // BLITT DEMO 2 - Game Toolkit : Particle Effect, Sprite, Animated Sprite, SpriteText, TiledSprite, Timer, Random
    public class Demo2 : Scene
    {
        public enum CharDir
        {
            Left = 0,
            Right = 1,
            Up = 2,
            Down = 3
        }
        
        private TiledSprite background;

        private Effect bump_sfx;

        private readonly float friction = 0.98f;

        private Size size;

        private readonly float speed = 4;

        private Sprite sprite;

        private Sprite tweenedSprite;

        private RangedValue tweenedValue;

        private Tweener tweener;

        private Timer timer;

        private int elapsed;

        private MultiframeSprite animated_sprite;

        private ParticleEmitter _trail;

        private ParticleEmitter _explosion;

        private float particle_x = 100.0f, particle_y = 100.0f, particle_dx, particle_dy;

        private float char_x = 100.0f;

        private float char_y = 200.0f;

        private float char_sx = 200.0f;

        private float char_sy = 200.0f;

        private CharDir char_dir = CharDir.Right; 

        public override void Load()
        {
            bump_sfx = Content.Get<Effect>("menu");

            size = Game.ScreenSize;

            background = new TiledSprite(Content.Get<Texture2D>("bg3"), size.W, size.H);
            background.SetScrollSpeed(0.02f, 0.02f);

            sprite = new Sprite(Content.Get<Texture2D>("particles"), 96, 64, 32, 32);

            tweenedSprite = new Sprite(Content.Get<Texture2D>("zazaka"));
            
            tweenedValue = new RangedValue(50, 300);
            
            tweener = new Tweener();
            
            tweener
                .Tween(0, tweenedValue,50)
                .Easing(EasingFunctions.QuadraticInOut)
                .RepeatForever()
                .AutoReverse();
            
            
            sprite.SetColor(Color.Red);
            sprite.SetOrigin(0.5f, 0.5f);

            timer = new Timer();

            animated_sprite = new MultiframeSprite(
                TextureAtlas.FromGrid(
                    Content.Get<Texture2D>("spritesheet"), 4, 4
                )
            );
            
            timer.Every(16, () => animated_sprite.SetColor(RandomEx.NextColor()));
            
            animated_sprite.SetFrameSpeed(5);

            animated_sprite
                .AddAnimation(
                    "idle_up",
                    0
                )
                .AddAnimation(
                    "idle_horiz",
                    
                    4
                )
                .AddAnimation(
                    "idle_down",
                    8
                )
                .AddAnimation(
                    "walk_up",
                    0, 1, 2, 3
                )
                .AddAnimation(
                    "walk_horiz",
                    4, 5, 6, 7
                )
                .AddAnimation(
                    "walk_down",
                    8, 9, 10, 11
                )
                .SetAnimation("idle_horiz");
            
            
            _trail = new ParticleEmitter(
                new Sprite(Content.Get<Texture2D>("particles"), 0, 0, 32, 32),
                new ParticleEmitterProps
                {
                    MaxParticles = 500,
                    Emission = 500,
                    ImmediateFullEmission = false,
                    LifeTime = -1,
                    Relative = true,
                    ParticleLife = new Range<float>(0.5f, 1.0f),
                    InitialPositionDisplacement = new Range<Vector2>(new Vector2(-10, -10), new Vector2(10, 10)),
                    Direction = 0,
                    StartColor = Color.Cyan,
                    EndColor = Color.Fuchsia,
                    StartOpacity = 1.0f,
                    EndOpacity = 0.0f,
                    StartScale = new Range<float>(1.0f, 2.0f),
                    EndScale = 0.5f,
                    Speed = new Range<float>(20.0f, 100.0f),
                    Spread = 0.1f,
                    Gravity = 100.0f,
                    SpinSpeed = 10.0f,
                    RadialAccel = 20.0f,
                    TangentialAccel = 30.0f
                
                 });
            
            _explosion = new ParticleEmitter(
                new Sprite(Content.Get<Texture2D>("particles"), 0, 64, 32, 32),
                new ParticleEmitterProps
                {
                    MaxParticles = 1000,
                    Emission = 200,
                    ImmediateFullEmission = true,
                    LifeTime = 0.2f,
                    Relative = false,
                    ParticleLife = new Range<float>(0.2f, 0.5f),
                    InitialPositionDisplacement = new Range<Vector2>(new Vector2(-10, -10), new Vector2(10, 10)),
                    Direction = 0,
                    Spread = Calc.TWO_PI,
                    StartColor = Color.Red,
                    EndColor = Color.Gray,
                    StartOpacity = 1.0f,
                    EndOpacity = 0.0f,
                    StartScale = 1.0f,
                    EndScale = 0.5f,
                    Speed = new Range<float>(100.0f, 150.0f),
                    Gravity = 500.0f,
                    SpinSpeed = 50.0f,
                    RadialAccel = 50.0f,
                    TangentialAccel = 50.0f
                }
            );
            
            _trail.FireAt(particle_x, particle_y);

        }

        public override void Unload()
        {
            _trail.Dispose();
            _explosion.Dispose();
        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void OnCanvasResize(int width, int height)
        {
            background.SetSize(width, height);
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update();

            elapsed = gameTime.ElapsedGameTime.Milliseconds;
            
            background.Update();
            
            tweener.Update();
            
            if (Input.KeyPressed(Key.Escape)) Game.Quit();
            

            if (Input.KeyPressed(Key.F11))
                Game.ToggleFullscreen();
            else if (Input.KeyPressed(Key.D1))
                Game.ScreenSize = new Size(size.W, size.H);
            else if (Input.KeyPressed(Key.D2))
                Game.ScreenSize = new Size(size.W * 2, size.H * 2);
            else if (Input.KeyPressed(Key.D3)) Game.ScreenSize = new Size(size.W * 3, size.H * 3);

            if (Input.KeyDown(Key.Left)) particle_dx -= speed;

            if (Input.KeyDown(Key.Right)) particle_dx += speed;

            if (Input.KeyDown(Key.Up)) particle_dy -= speed;

            if (Input.KeyDown(Key.Down)) particle_dy += speed;

            particle_dx *= friction;
            particle_dy *= friction;

            if (particle_dx > 0.05f && particle_dx < 0.05f || particle_dx < 0 && particle_dx > -0.05f) particle_dx = 0;

            if (particle_dy > 0.05f && particle_dy < 0.05f || particle_dy < 0.05f && particle_dy > -0.05f)
                particle_dy = 0;

            particle_x += particle_dx;
            particle_y += particle_dy;

            if (particle_x > Canvas.Width - 16)
            {
                particle_x = Canvas.Width - 16;
                particle_dx = -particle_dx;
                PlayBump();
            }
            else if (particle_x < 16)
            {
                particle_x = 16;
                particle_dx = -particle_dx;
                PlayBump();
            }

            if (particle_y > Canvas.Height - 16)
            {
                particle_y = Canvas.Height - 16;
                particle_dy = -particle_dy;
                PlayBump();
            }
            else if (particle_y < 16)
            {
                particle_y = 16;
                particle_dy = -particle_dy;
                PlayBump();
            }
            
            bool up = Input.KeyDown(Key.W);
            bool down = Input.KeyDown(Key.S);
            bool left = Input.KeyDown(Key.A);
            bool right = Input.KeyDown(Key.D);

            if (left)
            {
                if (!up && !down)
                {
                    char_sx = -5.0f;
                    char_dir = CharDir.Left;
                }
                else
                {
                    char_sx = -4.0f;
                }
            }
            else if (right)
            {
                if (!up && !down)
                {
                    char_sx = 5.0f;
                    char_dir = CharDir.Right;
                }
                else
                {
                    char_sx = 4.0f;
                }
            }

            if (up)
            {
                if (!left && !right)
                {
                    char_sy = -5.0f;
                    char_dir = CharDir.Up;
                }
                else
                {
                    char_sy = -4.0f;
                }
            }
            else if (down)
            {
                if (!left && !right)
                {
                    char_sy = 5.0f;
                    char_dir = CharDir.Down;
                }
                else
                {
                    char_sy = 4.0f;
                }
            }

            switch (char_dir)
            {
                case CharDir.Left:
                    animated_sprite.SetAnimation("walk_horiz");
                    animated_sprite.SetFlipH(true);
                    break;
                case CharDir.Right:
                    animated_sprite.SetAnimation("walk_horiz");
                    animated_sprite.SetFlipH(false);
                    break;
                case CharDir.Up:
                    animated_sprite.SetAnimation("walk_up");
                    animated_sprite.SetFlipH(false);
                    break;
                case CharDir.Down:
                    animated_sprite.SetAnimation("walk_down");
                    animated_sprite.SetFlipH(false);
                    break;
            }
            

            if (!left && !right)
            {
                char_sx = 0.0f;
            }
            
            if (!up && !down)
            {
                char_sy = 0.0f;
            }

            char_x += char_sx;
            char_y += char_sy;

            if (char_sx == 0.0f && char_sy == 0.0f)
            {
                switch (char_dir)
                {
                    case CharDir.Left:
                    case CharDir.Right:
                        animated_sprite.SetAnimation("idle_horiz");
                        break;
                    case CharDir.Up:
                        animated_sprite.SetAnimation("idle_up");
                        break;
                    case CharDir.Down:
                        animated_sprite.SetAnimation("idle_down");
                        break;
                }
            }

            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            animated_sprite.Update();
            
            _trail.MoveTo(particle_x, particle_y);
            _trail.Update(dt);
            _explosion.Update(dt);


            if (Input.MousePressed(MouseButton.Left))
            {
                _explosion.FireAt(Input.MousePos.X, Input.MousePos.Y);
            }
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            background.Draw(canvas, 0, 0);
            
            sprite.Draw(canvas, particle_x, particle_y);

            animated_sprite.Draw(canvas, char_x, char_y);
            
            tweenedSprite.SetColor(Color.DodgerBlue);
            tweenedSprite.Draw(canvas, tweenedValue.Value, 200);
            
            _trail.Draw(canvas);
            
            _explosion.Draw(canvas);

            canvas.FillRect(100, 100, 100, 100, Color.DodgerBlue);
            
            canvas.DrawText(10, 10,
                $"Elapsed Update: {elapsed}, Elapsed Draw: {gameTime.ElapsedGameTime.Milliseconds}", Color.DodgerBlue, 0.25f);
            
            
            canvas.DrawText(10, 60, $"Trail Particles: {_trail.ParticlesAlive}", Color.White, 0.25f);
            
            canvas.DrawText(10, 80, $"Explosion Particles: {_explosion.ParticlesAlive}", Color.White, 0.25f);
            
            canvas.DrawText(10, 100, $"Tweener Allocations: {tweener.AllocationCount}", Color.White, 0.25f);
            
            canvas.DrawText(10, 120, $"Draw Calls: {canvas.MaxDrawCalls}", Color.White, 0.25f);

        }

        private void PlayBump()
        {
            MediaPlayer.Play(bump_sfx, 0f, RandomEx.Range(0.1f, 1.0f));
        }
    }
}