using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public class GuiPanel : GuiContainer
    {
        internal override void Draw(Canvas canvas, GuiTheme theme)
        {
            theme.DrawPanel(canvas, this);

            base.Draw(canvas, theme);
            
        }

        internal GuiPanel(Gui gui, GuiContainer parent) : base(gui, parent)
        {
        }
    }
}