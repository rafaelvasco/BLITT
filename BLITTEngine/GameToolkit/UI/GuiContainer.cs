using System.Collections.Generic;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameToolkit.UI.Layouts;

namespace BLITTEngine.GameToolkit.UI
{
    public class GuiContainer : GuiControl
    {
        public int Padding { get; set; } = 10;
        
        public GuiButton AddButton(string label)
        {
            var button = new GuiButton(Gui, this) {Label = label};

            AddWidget( button);

            return button;
        }

        public GuiCheckbox AddCheckbox()
        {
            var checkbox = new GuiCheckbox(Gui, this);

            AddWidget(checkbox);

            return checkbox;
        }

        public GuiPanel AddPanel()
        {
            var panel = new GuiPanel(Gui, this);

            AddWidget(panel);

            return panel;
        }

        public GuiSlider AddSlider(int minValue, int maxValue, int step, Orientation orientation = Orientation.Horizontal)
        {
            var slider = new GuiSlider(
                Gui,
                this,
                value: minValue,
                minValue: minValue,
                maxValue: maxValue,
                step: step, orientation:
                orientation);

            AddWidget(slider);

            return slider;
        }

        
        public GuiContainer AddContainer()
        {
            var container = new GuiContainer(Gui, this);

            AddWidget(container);

            return container;
        }

        public HorizontalContainer AddHorizontalContainer()
        {
            var container = new HorizontalContainer(Gui, this);

            AddWidget(container);

            return container;
        }

        public VerticalContainer AddVerticalContainer()
        {
            var container = new VerticalContainer(Gui, this);

            AddWidget(container);

            return container;
        }
        
        internal virtual void DoLayout()
        {
            ProcessDocking();

            for (int i = 0; i < children.Count; i++)
            {
                var control = children[i];

                control.SetPosition(
                    Calc.Clamp(control.X, this.Padding, this.W - this.Padding - control.W), 
                    Calc.Clamp(control.Y, this.Padding, this.H - this.Padding - control.H)
                );

                int newW = control.W;
                int newH = control.H;
                
                if (control.X + control.W > this.W - Padding)
                {
                    newW = this.W - control.X - Padding;
                }
                
                if (control.Y + control.H > this.H - Padding)
                {
                    newH = this.H - control.Y - Padding;
                }
                
                control.Resize(newW, newH);
                
            }
        }

        internal virtual void DoAutoSize()
        {
            if (children.Count == 0)
            {
                return;
            }
            
            int maxW = 2*Padding;
            int maxH = 2*Padding;

            for (int i = 0; i < children.Count; i++)
            {
                var control = children[i];

                if (control.X + control.W  > maxW )
                {
                    maxW = control.X + control.W + Padding;
                }

                if (control.Y + control.H > maxH)
                {
                    maxH = control.Y + control.H + Padding;
                }
            }

            this.w = Calc.Max(maxW, this.w);
            this.h = Calc.Max(maxH, this.h);

        }
        
        protected virtual void ProcessDocking()
        {
            for (int i = 0; i < children.Count; i++)
            {
                var control = children[i];
                
                switch (control.Docking)
                {
                    case GuiDocking.Top:

                        control.Resize(this.W - 2 * this.Padding, control.H);
                        control.x = this.Padding;
                        control.y = this.Padding;
                    
                        break;
                    case GuiDocking.Center:

                        control.Resize(this.W - 2 * this.Padding, this.H - 2 * this.Padding);
                        control.x = this.Padding;
                        control.y = this.Padding;
                        
                        break;
                    case GuiDocking.Bottom:

                        control.Resize(this.W - 2 * this.Padding, control.H);
                        
                        control.x = this.Padding;
                        control.y = this.H - this.Padding - control.h;
                        
                        break;
                    case GuiDocking.Left:
                        
                        control.Resize(control.W, this.H - 2 * this.Padding);

                        control.x = this.Padding;
                        control.y = this.Padding;
                        
                        break;
                    case GuiDocking.Right:
                        
                        control.Resize(control.W, this.H - 2 * this.Padding);

                        control.x = this.W - this.Padding - control.W;
                        control.y = this.Padding;
                        
                        break;
                }
            }
        }

        internal override void Update(GuiMouseState mouseState)
        {
            foreach (var widget in children)
            {
                if ( this.BoundingRect.Intersects(widget.BoundingRect))
                {
                    widget.Update(mouseState);
                }
            }
        }

        internal override void Draw(Canvas canvas, GuiTheme theme)
        {
            var x = this.GlobalX;
            var y = this.GlobalY;
            var w = this.W;
            var h = this.H;

            canvas.BeginClip(x, y, w, h);
            
            canvas.DrawRect(x, y, w, h, Color.Fuchsia);
            
            foreach (var widget in children)
            {
                widget.Draw(canvas, theme);
            }

            canvas.EndClip();
        }

        internal GuiContainer(Gui gui, int width, int height) : base(gui)
        {
            w = width;
            h = height;
            children =
                new List<GuiControl>();
        } 

        internal GuiContainer(Gui gui, GuiContainer parent) : base(gui, parent)
        {
            w = 0;
            h = 0;
            x = 0;
            y = 0;
            children = 
                new List<GuiControl>();
        }
        
        private void AddWidget(GuiControl guiControl)
        {
            children.Add(guiControl);
            
            Gui.InvalidateVisual();
            Gui.InvalidateLayout();
        }
        
        internal readonly List<GuiControl> children;
        
    }
  
}