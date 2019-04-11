using System.Numerics;
using BLITTEngine;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameToolkit;
using BLITTEngine.GameToolkit.Animation;

namespace BLITTDemo
{
    public class Demo5 : Scene
    {
        
        
        private Sprite sprite;
        private Sprite sprite2;

        private ShaderProgram color_shader;
        private ShaderProgram scanlines_shader;
        private ShaderProgram current_shader;

        private ShaderParameter colors_shader_tint_color;
        
        private Vector4 scanlines_params;

        private ShaderParameter scanlines_shader_params;

        private Timer timer;

        public override void Load()
        {
            color_shader = Content.Get<ShaderProgram>("color_shader");
            scanlines_shader = Content.Get<ShaderProgram>("scanlines");

            current_shader = color_shader;

            colors_shader_tint_color = color_shader.GetParameter("tint_color");
            
            colors_shader_tint_color.SetValue(Color.White);

            scanlines_shader_params = scanlines_shader.GetParameter("params");
            
            scanlines_params = new Vector4(
                1.0f, // Amount
                0.5f, // Intensity
                0.0f, // Time
                0.0f    
            );
        
            scanlines_shader_params.SetValue(scanlines_params);
            
            sprite = new Sprite(Content.Get<Texture2D>("cat"));
            
            sprite.SetOrigin(0.5f, 0.5f);
            
            sprite2 = new Sprite(Content.Get<Texture2D>("seiken"));
            
            sprite.SetOrigin(0.5f, 0.5f);
            
            timer = new Timer();
            
            timer.Every(20, UpdateColor);


        }

        private void UpdateColor()
        {
            colors_shader_tint_color.SetValue(RandomEx.NextColor());
        }


        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update();

            /*scanlines_timer += dt;

            if (scanlines_timer > 10000)
            {
                scanlines_timer = 0.0f;
            }
            
            scanlines_params.Z = scanlines_timer;
            
            scanlines_shader_params.SetValue(scanlines_params);*/
            
            if (Input.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }
            else if (Input.KeyPressed(Key.P))
            {
                Canvas.SaveScreenShot(@"C:\Users\rafae\Desktop\screenshot.png");
            }
            else if (Input.KeyPressed(Key.D1))
            {
                current_shader = color_shader;
            }
            else if (Input.KeyPressed(Key.D2))
            {
                current_shader = scanlines_shader;
            }
            else if (Input.KeyPressed(Key.Escape))
            {
                Game.Quit();
            }
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            canvas.Begin();
            
            canvas.Clear(Color.Black);
            
            canvas.SetShader(current_shader);
            
            sprite.Draw(canvas, Canvas.Width/2.0f, Canvas.Height/2.0f);
            
            canvas.SetShader(color_shader);
            
            sprite2.DrawEx(canvas, 10, 200, 0f, 0.5f);
            
            canvas.SetShader(null);
            
            //canvas.DrawString(10, 10, $"FPS: {Game.Clock.FPS}, DT: {Game.Clock.DeltaTime}, FrameRate: {Game.Clock.FrameRate}", 0.25f);
            canvas.DrawString(10, 30, "Press: 1: ColorShader; 2: Scanlines Shader", 0.25f);
            
            canvas.End();
            
        }
    }
}