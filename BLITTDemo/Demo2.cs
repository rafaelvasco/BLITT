using System;
using BLITTEngine;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo2 : Scene
    {
        private Image image;
        private Rectangle dst_rect;
        private RectangleI src_rect;
        
        public override void Init()
        {
            image = Content.Get<Image>("ship");
            
            dst_rect = new Rectangle(100, 100, image.Width, image.Height);
            
            src_rect = new RectangleI(0, 0, image.Width, image.Height);
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
            
            if(Control.KeyDown(Key.Left))
            {
                Canvas.Translate(-5f, 0);
            }
            else if(Control.KeyDown(Key.Right))
            {
                Canvas.Translate(5f, 0);
            }
            
            if (Control.KeyPressed(Key.Space))
            {
                if (image.FilterMode == ImageFilterMode.Crisp)
                {
                    image.FilterMode = ImageFilterMode.Smooth;    
                }
                else
                {
                    image.FilterMode = ImageFilterMode.Crisp;
                }
                
            }
            
            if (Control.KeyPressed(Key.W))
            {
                Console.WriteLine("WRAP");
                
                if (image.WrapMode == ImageWrapMode.None)
                {
                    image.WrapMode = ImageWrapMode.Repeat;
                    src_rect = new RectangleI(0, 0, image.Width*4, image.Height*4);
                    
                }
                else
                {
                    image.WrapMode = ImageWrapMode.None;
                    src_rect = new RectangleI(0, 0, image.Width, image.Height);
                }
                
            }
        }

        public override void Draw()
        {
            Canvas.Draw(image, src_rect, dst_rect);
        }
    }
}