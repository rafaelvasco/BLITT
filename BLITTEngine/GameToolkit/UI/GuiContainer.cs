using System.Collections.Generic;
using BLITTEngine.Core.Graphics;

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
            var panel = new GuiPanel(Gui, this)
            {
                IsContainer = true
            };

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
            var container = new GuiContainer(Gui, this) { IsContainer = true };

            AddWidget(container);

            return container;
        }

        public HorizontalContainer AddHorizontalContainer()
        {
            var container = new HorizontalContainer(Gui, this) { IsContainer = true };

            AddWidget(container);

            return container;
        }

        public VerticalContainer AddVerticalContainer()
        {
            var container = new VerticalContainer(Gui, this) { IsContainer = true };

            AddWidget(container);

            return container;
        }

        protected virtual void DoLayout() { }

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
            
            foreach (var widget in children)
            {
                widget.Draw(canvas, theme);
            }

            canvas.EndClip();
        }

        internal GuiContainer(Gui gui, int width, int height) : base(gui)
        {
            W = width;
            H = height;
            children =
                new List<GuiControl>();
        } 

        internal GuiContainer(Gui gui, GuiContainer parent) : base(gui, parent)
        {
            W = parent.W ;
            H = parent.H;
            children = 
                new List<GuiControl>();
        }
        
        private void AddWidget(GuiControl guiControl)
        {
            children.Add(guiControl);
            DoLayout();
        }
        
        protected readonly List<GuiControl> children;
        
    }
    
    public enum HAlignment
    {
        Left,
        Center, 
        Right,
        Stretch
    }
        
    public enum VAlignment
    {
        Top,
        Center,
        Bottom,
        Stretch
    }

    public class VerticalContainer : GuiContainer
    {
        
        public int ItemSpacing { get; set; } = 10;
        public VAlignment AlignVertical { get; set; } = VAlignment.Top;
        public HAlignment AlignHorizontal { get; set; } = HAlignment.Left;

        internal VerticalContainer(Gui gui, GuiContainer parent) : base(gui, parent)
        {
        }

        protected override void DoLayout()
        {

            int length = children.Count;

            if (length == 0)
            {
                return;
            }

            int total_height = 0;

            for (int i = 0; i < length; i++)
            {
                total_height += children[i].H;
            }

            total_height += (length - 1) * ItemSpacing;

            for (int i = 0; i < length; i++)
            {
                var widget = children[i];

                switch (AlignHorizontal)
                {
                    case HAlignment.Left:

                        widget.X = this.X + this.Padding;
                        
                        break;
                    case HAlignment.Center:

                        widget.X = this.X + this.W / 2 - widget.W / 2; 
                            
                        
                        break;
                    case HAlignment.Right:

                        widget.X = this.X + this.W - widget.W - this.Padding;
                        
                        break;
                    
                    case HAlignment.Stretch:

                        widget.X = this.X + this.Padding;

                        if (!widget.FixedSize)
                        {
                            widget.W = this.W - 2 * Padding;
                        }
                        
                        break;
                }

                switch (AlignVertical)
                {
                    case VAlignment.Top:

                        widget.Y = this.Y + this.Padding;
                        
                        if (i > 0)
                        {
                            widget.Y = children[i - 1].Y + children[i - 1].H + ItemSpacing;
                        }
                        
                        break;
                    case VAlignment.Center:

                        widget.Y = this.Y + this.H / 2 - (total_height)/2;

                        if (i > 0)
                        {
                            widget.Y = children[i - 1].Y + children[i - 1].H + ItemSpacing;
                        }
                        
                        break;
                    case VAlignment.Bottom:

                        widget.Y = this.Y + this.H - total_height - this.Padding;

                        if (i > 0)
                        {
                            widget.Y = children[i - 1].Y + children[i - 1].H + ItemSpacing;
                        }
                        
                        break;
                    
                    case VAlignment.Stretch:

                        widget.LayoutH = (this.H - 2 * Padding - (children.Count - 1) * ItemSpacing) / children.Count;
                        
                        if (!widget.FixedSize)
                        {
                            widget.H = widget.LayoutH;
                        }

                        if (i == 0)
                        {
                            widget.Y = this.Y + this.Padding;
                        }
                        else
                        {
                            widget.Y = children[i - 1].Y + children[i - 1].LayoutH + ItemSpacing;
                        }

                        break;
                }
            }
        }
    }

    public class HorizontalContainer : GuiContainer
    {
        public int ItemSpacing { get; set; } = 10;
        public VAlignment AlignVertical { get; set; } = VAlignment.Top;
        public HAlignment AlignHorizontal { get; set; } = HAlignment.Left;

        internal HorizontalContainer(Gui gui, GuiContainer parent) : base(gui, parent)
        {
        }

        protected override void DoLayout()
        {
            int length = children.Count;

            if (length == 0)
            {
                return;
            }

            int total_width = 0;

            for (int i = 0; i < length; i++)
            {
                total_width += children[i].W;
            }

            int x = this.X;
            int y = this.Y;

            total_width += (length - 1) * ItemSpacing;

            for (int i = 0; i < length; i++)
            {
                var widget = children[i];

                switch (AlignHorizontal)
                {
                    case HAlignment.Left:

                        widget.X = x + this.Padding;
                        
                        if (i > 0)
                        {
                            widget.X = children[i - 1].X + children[i - 1].W + ItemSpacing;
                        }
                        
                        break;
                    case HAlignment.Center:

                        widget.X = x + this.W / 2 - (total_width)/2;

                        if (i > 0)
                        {
                            widget.X = children[i - 1].X + children[i - 1].W + ItemSpacing;
                        }
                        
                        break;
                    
                    case HAlignment.Right:

                        widget.X = x + this.W - total_width - this.Padding;

                        if (i > 0)
                        {
                            widget.X = children[i - 1].X + children[i - 1].W + ItemSpacing;
                        }
                        
                        break;
                    
                    case HAlignment.Stretch:

                        widget.LayoutW = (this.W - 2 * Padding - (children.Count - 1) * ItemSpacing) / children.Count;
                        
                        if (!widget.FixedSize)
                        {
                            widget.W = widget.LayoutW;
                        }

                        if (i == 0)
                        {
                            widget.X = x + this.Padding;
                        }
                        else
                        {
                            widget.X = children[i - 1].X + children[i - 1].LayoutW + ItemSpacing;
                        }
                        
                        break;
                }

                switch (AlignVertical)
                {
                    case VAlignment.Top:

                        widget.Y = y + this.Padding;
                        
                        break;
                    case VAlignment.Center:

                        widget.Y = y + this.H / 2 - widget.H / 2; 
                        
                        break;
                    case VAlignment.Bottom:
                        
                        widget.Y = y + this.H - widget.H - this.Padding;
                        
                        break;

                      
                    case VAlignment.Stretch:

                        widget.Y = y + this.Padding;

                        if (!widget.FixedSize)
                        {
                            widget.H = this.H - 2 * Padding;
                        }

                        break;
                }
            }
            
        }
    }
}