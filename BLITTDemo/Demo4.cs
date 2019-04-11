using BLITTEngine;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameToolkit;

namespace BLITTDemo
{
    public class Demo4 : Scene
    {
        private Mesh mesh;

        private const int n_rows = 16;
        private const int n_cols = 16;
        private float cell_w = 512.0f / (n_cols - 1);
        private float cell_h = 512.0f / (n_rows - 1);

        private float t;

        private int mode ;

        public override void Load()
        {
            mesh = new Mesh(n_cols, n_rows);
            mesh.SetTexture(Content.Get<Texture2D>("awesomeface"));
            mesh.SetBlendMode(BlendMode.AlphaBlend);
            mesh.Clear(Color.White);
          
        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {

            int i, j;

            if (Input.KeyPressed(Key.F11))
            {
                Game.ToggleFullscreen();
            }

            if (Input.KeyPressed(Key.Space))
            {
                if (++mode > 2)
                {
                    mode = 0;
                }

                mesh.Clear(Color.White);

            }

            switch (mode)
            {
                case 0:
                {
                    for (i = 1; i < n_rows - 1; ++i)
                    {
                        for (j = 1; j < n_cols-1; ++j)
                        {
                            mesh.SetDisplacement(j, i, 
                                                 Calc.Cos(t * 10 + (i+j)/2.0f) * 5, 
                                                 Calc.Sin(t * 10 + (i+j)/2.0f) * 5, 
                                                 MeshDisplacementReference.Node);

                        }
                    }

                    break;
                }
                case 1:
                {
                    for (i = 0; i < n_rows; ++i)
                    {
                        for (j = 1; j < n_cols-1; ++j)
                        {
                            mesh.SetDisplacement(j, i, 
                                                 Calc.Cos(t * 5 + j/2.0f) * 15, 
                                                 0, 
                                                 MeshDisplacementReference.Node);
                            

                        }
                    }

                    break;
                }
                case 2:
                {
                    for (i = 0; i < n_rows; ++i)
                    {
                        for (j = 0; j < n_cols; ++j)
                        {
                            float r = Calc.Sqrt(Calc.Pow(j - (float) n_cols / 2, 2) +
                                                Calc.Pow(i - (float) n_rows / 2, 2));

                            float a = r * Calc.Cos(t * 2) * 0.1f;
                            float dx = Calc.Sin(a) * (i * cell_h - 256) + Calc.Cos(a) * (j * cell_w - 256);
                            float dy = Calc.Cos(a) * (i * cell_h - 256) - Calc.Sin(a) * (j * cell_w - 256);

                            mesh.SetDisplacement(j, i, 
                                                 dx, 
                                                 dy, 
                                                 MeshDisplacementReference.Center);

                        }
                    }

                    break;
                }
            }
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            canvas.Begin();

            canvas.Clear(Color.Black);

            mesh.DrawEx(canvas, 112, 52, 0.5f);

            canvas.SetColor(Color.Red);
            
            //canvas.DrawString(5, 15, $"Dt: {Game.Clock.DeltaTime}, FPS: {Game.Clock.FPS}, Use your SPACE!", 0.25f);

            canvas.End();
        }
    }
}
