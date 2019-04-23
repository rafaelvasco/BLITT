using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public abstract class GuiControl
    {

        public int X => x;

        public int Y => y;

        public int W => w;

        public int H => h;

        public GuiDocking Docking
        {
            get => docking;
            set
            {
                if (docking != value)
                {
                    docking = value;
                    
                    Gui.InvalidateVisual();
                    Gui.InvalidateLayout();
                }
            }
        }

        public GuiContainer Parent { get; }

        public int GlobalX => Parent?.GlobalX + X ?? X;
        public int GlobalY => Parent?.GlobalY + Y ?? Y;

        internal int LayoutW;

        internal int LayoutH;

        public bool Hovered { get; protected set; }
        public bool Active { get; protected set; }
        public bool FixedSize { get; set; }

        public RectF BoundingRect => RectF.FromBox(GlobalX, GlobalY, W, H);

        protected GuiControl(Gui gui)
        {
            Gui = gui;
            Parent = null;
        }

        protected GuiControl(Gui gui, GuiContainer parent)
        {
            Gui = gui;
            Parent = parent;
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;

            Gui.InvalidateVisual();
            Gui.InvalidateLayout();
        }

        public void Resize(int newW, int newH)
        {
            if (this.w == newW && this.h == newH) return;
            
            this.w = newW;
            this.h = newH;
            
            Gui.InvalidateVisual();
            Gui.InvalidateLayout();
        }

        public virtual bool ContainsPoint(int px, int py)
        {
            if (
                px < GlobalX ||
                py < GlobalY ||
                px > GlobalX + W ||
                py > GlobalY + H)
            {
                return false;
            }

            return true;
        }

        internal abstract void Update(GuiMouseState mouseState);
        internal abstract void Draw(Canvas canvas, GuiTheme theme);
        
        protected readonly Gui Gui;

        internal int x;
        internal int y;
        internal int w;
        internal int h;

        private GuiDocking docking = GuiDocking.None;
    }
}