using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public abstract class GuiControl
    {

        public int X
        {
            get => x;
            set
            {
                x = value;
                Invalidate();
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                Invalidate();
            }
        }

        public int W
        {
            get => w;
            set
            {
                w = value;
                Invalidate();
            }
        }

        public int H
        {
            get => h;
            set
            {
                h = value;
                Invalidate();
            }
        }

        public GuiContainer Parent { get; }

        public int GlobalX => Parent?.GlobalX + X ?? X;
        public int GlobalY => Parent?.GlobalY + Y ?? Y;

        internal int LayoutW;

        internal int LayoutH;

        public bool Hovered { get; protected set; }
        public bool Active { get; protected set; }
        public bool IsContainer { get; internal set; }
        
        public bool FixedSize { get; set; }

        public Rect BoundingRect => Rect.FromBox(GlobalX, GlobalY, W, H);

       

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

        protected void Invalidate()
        {
            Gui.Invalidate();
        }

        internal abstract void Update(GuiMouseState mouseState);
        internal abstract void Draw(Canvas canvas, GuiTheme theme);
        
        protected readonly Gui Gui;

        private int x;
        private int y;
        private int w;
        private int h;
    }
}