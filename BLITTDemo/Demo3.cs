//using BLITTEngine;
//using BLITTEngine.Core.Audio;
//using BLITTEngine.Core.Common;
//using BLITTEngine.Core.Control;
//using BLITTEngine.Core.Control.Keyboard;
//using BLITTEngine.Core.Graphics;
//using BLITTEngine.Core.Resources;
//using BLITTEngine.GameToolkit;
//using BLITTEngine.GameToolkit.Animation;
//
//namespace BLITTDemo
//{
//    public class Demo3 : Scene
//    {
//        private int color_task;
//
//        private Effect bump_sfx;
//        
//        private readonly float friction = 0.98f;
//        
//        private Size size;
//
//        private readonly float speed = 5;
//
//        private Sprite sprite;
//        private Sprite sprite2;
//        
//        private Timer timer;
//        
//        private RenderTarget render_target1;
//
//        private Sprite spr_target1;
//        
//        private RenderTarget render_target2;
//
//        private Sprite spr_target2;
//
//        private float x = 100.0f, y = 100.0f, dx, dy;
//
//        public override void Load()
//        {
//            bump_sfx = Content.Get<Effect>("menu");
//            
//            size = Game.ScreenSize;
//
//            sprite = new Sprite(Content.Get<Texture2D>("particles"), 96, 64, 32, 32);
//
//            sprite.SetColor(Color.Cyan);
//            sprite.SetOrigin(0.5f, 0.5f);
//            
//            sprite2 = new Sprite(Content.Get<Texture2D>("particles"), 32, 32, 32, 32);
//
//            sprite2.SetColor(Color.Red);
//            sprite2.SetOrigin(0.5f, 0.5f);
//
//            render_target1 = Content.CreateRenderTarget(320, 240);
//            render_target1.Filtered = true;
//
//
//            spr_target1 = new Sprite(render_target1, 0, 0, 320, 240);
//            
//            render_target2 = Content.CreateRenderTarget(320, 240);
//            render_target2.Filtered = true;
//
//
//            spr_target2 = new Sprite(render_target2, 0, 0, 320, 240);
//
//        }
//
//        public override void Init()
//        {
//            timer = new Timer();
//        }
//
//        public override void End()
//        {
//        }
//
//        public override void Update(GameTime gameTime)
//        {
//            //timer.Update(dt);
//
//            
//            if (Input.KeyPressed(Key.Escape)) Game.Quit();
//
//            if (Input.KeyPressed(Key.F11))
//                Game.ToggleFullscreen();
//            else if (Input.KeyPressed(Key.C))
//                timer.Cancel(color_task);
//            else if (Input.KeyPressed(Key.T))
//                color_task = timer.Every(50, () => sprite.SetColor(RandomEx.NextColor()));
//            else if (Input.KeyPressed(Key.D))
//                timer.After(100, () => sprite.SetColor(Color.Green));
//            else if (Input.KeyPressed(Key.D1))
//                Game.ScreenSize = new Size(size.W, size.H);
//            else if (Input.KeyPressed(Key.D2))
//                Game.ScreenSize = new Size(size.W * 2, size.H * 2);
//            else if (Input.KeyPressed(Key.D3)) Game.ScreenSize = new Size(size.W * 3, size.H * 3);
//
//            if (Input.KeyDown(Key.Left)) dx -= speed;
//
//            if (Input.KeyDown(Key.Right)) dx += speed;
//
//            if (Input.KeyDown(Key.Up)) dy -= speed;
//
//            if (Input.KeyDown(Key.Down)) dy += speed;
//
//            dx *= friction;
//            dy *= friction;
//
//            if (dx > 0.05f && dx < 0.05f || dx < 0 && dx > -0.05f) dx = 0;
//
//            if (dy > 0.05f && dy < 0.05f || dy < 0.05f && dy > -0.05f) dy = 0;
//
//            x += dx;
//            y += dy;
//
//            if (x > render_target1.Width - 16)
//            {
//                x = render_target1.Width - 16;
//                dx = -dx;
//                PlayBump();
//            }
//            else if (x < 16)
//            {
//                x = 16;
//                dx = -dx;
//                PlayBump();
//            }
//
//            if (y > render_target1.Height - 16)
//            {
//                y = render_target1.Height - 16;
//                dy = -dy;
//                PlayBump();
//            }
//            else if (y < 16)
//            {
//                y = 16;
//                dy = -dy;
//                PlayBump();
//            }
//
//        }
//
//        public override void Draw(Canvas canvas, GameTime gameTime)
//        {
//            /* DRAW TO RENDER TARGET */
//
//            canvas.SetSurface(render_target1);
//
//            sprite.Draw(canvas, x, y);
//            
//            canvas.SetSurface(render_target2);
//            
//            sprite2.Draw(canvas, x, y);
//            
//            canvas.SetSurface();
//            
//            /* DRAW SEVERAL COPIES OF RENDER TARGETS TO SCREEN */
//            
//            for (int i = 0; i < 3; i++)
//            {
//                spr_target1.DrawEx(canvas, i * 100.0f, i * 50.0f, i * Calc.PI / 8, 1.0f - i * 0.1f);
//            }
//            
//            for (int i = 3; i < 6; i++)
//            {
//                spr_target2.DrawEx(canvas, i * 100.0f, i * 50.0f, i * Calc.PI / 8, 1.0f - i * 0.1f);
//            }
//            
//            canvas.DrawText(20, 20, $"MousePos: {Input.MousePos}", Color.White, 0.25f);
//
//            //canvas.End();
//            
//        }
//        
//        private void PlayBump()
//        {
//            MediaPlayer.Play(bump_sfx, 0f, RandomEx.Range(0.1f, 1.0f));
//        }
//    }
//}