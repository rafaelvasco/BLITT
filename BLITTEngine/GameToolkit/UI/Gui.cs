using System;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit.UI
{
    internal class GuiMouseState
    {
        public int MouseX;
        public int MouseY;
        public int LastMouseX;
        public int LastMouseY;
        public bool MouseLeftDown;
        public bool MouseRightDown;
        public bool MouseMiddleDown;

        public void UpdatePosition(int x, int y)
        {
            LastMouseX = MouseX;
            LastMouseY = MouseY;

            MouseX = x;
            MouseY = y;
        }

        public bool Moved => MouseX != LastMouseX || MouseY != LastMouseY;
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }


    public class Gui
    {
        public GuiContainer Root => root;

       
        public Gui(int x, int y, int width, int height)
        {
            surface = Game.Instance.ContentManager.CreateRenderTarget(width, height);
            
            surface_quad = new Quad(surface)
            {
                V0 = {X = x, Y = y},
                V1 = {X = x + width, Y = y},
                V2 = {X = x + width, Y = y + height},
                V3 = {X = x, Y = y + height},
                Blend = BlendMode.AlphaBlend
            };

            mouse_state = new GuiMouseState();

            theme = new DefaultTheme(Game.Instance.Canvas.DefaultFont);
            
            root = new GuiContainer(this, width, height);
        }

        public void Resize(int width, int height)
        {
            root.W = width;
            root.H = height;

            invalidated = true;
        }

        public void Update()
        {
            mouse_state.UpdatePosition(Input.MousePosition.X, Input.MousePosition.Y);

            var leftDown = Input.MouseDown(MouseButton.Left);
            var middleDown = Input.MouseDown(MouseButton.Middle);
            var rightDown = Input.MouseDown(MouseButton.Right);

            mouse_state.MouseLeftDown = leftDown;
            mouse_state.MouseMiddleDown = middleDown;
            mouse_state.MouseRightDown = rightDown;

            root.Update(mouse_state);

        }

        public void Draw(Canvas canvas)
        {
            if (invalidated)
            {
                canvas.SetRenderTarget(surface);

                if (invalidated)
                {
                    root.Draw(canvas, theme);

                }
                
                canvas.SetRenderTarget();

                invalidated = false;
            }
            
            
            canvas.DrawQuad(surface, ref surface_quad);
            
            
        }

        public void Invalidate()
        {
            invalidated = true;
        } 
        
        private readonly GuiMouseState mouse_state;
        private readonly GuiTheme theme;
        private readonly GuiContainer root;
        private bool invalidated = true;
        private readonly RenderTarget surface;
        private Quad surface_quad;


    }
}