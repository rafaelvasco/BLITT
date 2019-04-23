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

    public enum Orientation : byte
    {
        Horizontal = 0,
        Vertical
    }

    public class Gui
    {
        public GuiContainer Main => root;
        
        public Gui()
        {
            var width = Game.Instance.Canvas.Width;
            var height = Game.Instance.Canvas.Height;
            
            surface = new CanvasSurface(Rect.FromBox(0, 0, width, height));
            
            Game.Instance.Platform.OnWinResized += OnWinResized;

            mouse_state = new GuiMouseState();

            theme = new DefaultTheme(Game.Instance.Canvas.DefaultFont);
            
            root = new GuiContainer(this, width, height);
            
            Game.Instance.Canvas.AddSurface(surface);
        }

        private void OnWinResized(int arg1, int arg2)
        {
            layout_invalidated = true;
            visual_invalidated = true;
            surface_invalidated = true;

        }

        public void Resize(int width, int height)
        {
            root.Resize(width, height);
        }

        public void Update()
        {
            mouse_state.UpdatePosition(Input.MousePos.X, Input.MousePos.Y);

            var leftDown = Input.MouseDown(MouseButton.Left);
            var middleDown = Input.MouseDown(MouseButton.Middle);
            var rightDown = Input.MouseDown(MouseButton.Right);

            mouse_state.MouseLeftDown = leftDown;
            mouse_state.MouseMiddleDown = middleDown;
            mouse_state.MouseRightDown = rightDown;

            root.Update(mouse_state);

            if (layout_invalidated)
            {
                RecalculateSize(root);
                
                RecalculateLayout(root);

                layout_invalidated = false;
            }


        }

        public void Draw(Canvas canvas)
        {
            if (visual_invalidated)
            {
                Console.WriteLine("Gui Redraw");
            
                canvas.SetSurface(surface);

                root.Draw(canvas, theme);

                canvas.SetSurface();

                visual_invalidated = false;
            }
            
        }

        internal void InvalidateVisual()
        {
            visual_invalidated = true;
        }

        internal void InvalidateLayout()
        {
            layout_invalidated = true;
        }

        private void RecalculateSize(GuiContainer container)
        {
            for (int i = 0; i < container.children.Count; i++)
            {
                var control = container.children[i];

                if (control is GuiContainer containerChild)
                {
                    RecalculateSize(containerChild);
                }
            }
            
            container.DoAutoSize();
            
        }
        
        private void RecalculateLayout(GuiContainer container)
        {
            
            container.DoLayout();
            
            for (int i = 0; i < container.children.Count; i++)
            {
                var control = container.children[i];

                if (control is GuiContainer containerChild)
                {
                    RecalculateLayout(containerChild);
                }
            }
            
            
        }
        
        private readonly GuiMouseState mouse_state;
        private readonly GuiTheme theme;
        private readonly GuiContainer root;

        private bool layout_invalidated = true;
        private bool visual_invalidated = true;
        private bool surface_invalidated;
        
        private readonly CanvasSurface surface;


    }
}