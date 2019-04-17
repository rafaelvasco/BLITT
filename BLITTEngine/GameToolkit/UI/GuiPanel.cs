using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public class GuiPanel : GuiContainer
    {
        public static Size DefaultSize => new Size(200, 200);

        internal override void Draw(Canvas canvas, GuiTheme theme)
        {
            theme.DrawPanel(canvas, this);

            base.Draw(canvas, theme);
            
        }

        internal GuiPanel(Gui gui, GuiContainer parent) : base(gui, parent)
        {
            W = DefaultSize.W;
            H = DefaultSize.H;
        }
    }
}