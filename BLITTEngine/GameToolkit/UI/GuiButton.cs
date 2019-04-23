using System;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public class GuiButton : GuiControl
    {
        public static Size DefaultSize => new Size(100, 30);

        public event EventHandler OnClick;
        public event EventHandler OnPressed;
        public event EventHandler OnReleased;

        public string Label { get; set; } = "Click Me";

        internal override void Update(GuiMouseState mouseState)
        {
            if (this.ContainsPoint(mouseState.MouseX, mouseState.MouseY))
            {
                if (!this.Hovered)
                {
                    this.Hovered = true;
                    Gui.InvalidateVisual();
                }

                if (mouseState.MouseLeftDown && !this.Active)
                {
                    this.Active = true;
                    OnPressed?.Invoke(this, EventArgs.Empty);
                    Gui.InvalidateVisual();
                }
                else if (!mouseState.MouseLeftDown && this.Active)
                {
                    this.Active = false;
                    OnReleased?.Invoke(this, EventArgs.Empty);
                    OnClick?.Invoke(this, EventArgs.Empty);
                    Gui.InvalidateVisual();
                }
            }
            else
            {
                if (this.Hovered)
                {
                    
                    if (this.Active)
                    {
                        this.Active = false;
                        
                    }
                    
                    this.Hovered = false;
                    
                    Gui.InvalidateVisual();
                    
                }
            }
        }

        internal override void Draw(Canvas canvas, GuiTheme theme)
        {
            theme.DrawButton(canvas, this);
        }

        internal GuiButton(Gui gui, GuiContainer parent) : base(gui, parent)
        {
            w = DefaultSize.W;
            h = DefaultSize.H;
        }
    }
}