using System.Collections.Generic;
using BLITTEngine;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;
using BLITTEngine.GameToolkit;
using BLITTEngine.GameToolkit.UI;

namespace BLITTDemo
{
    public class Demo6 : Scene
    {
        private Gui gui;
        private TextureAtlas gui_sheet;
        
        public override void Load()
        {
            var atlas = new Dictionary<string, Rect>
            {
               
                {"grey_button00", Rect.FromBox(0,143,190,45)},
                {"grey_button01", Rect.FromBox(0,188,190,49)}
            };
            
            
            
            gui = new Gui();

            gui_sheet = TextureAtlas.FromAtlas(Content.Get<Texture2D>("greySheet"), atlas);

            var btn_props = new GuiButton.Props()
            {
                DownSprite = new Sprite(gui_sheet.Texture, gui_sheet["grey_button00"]),
                UpSprite = new Sprite(gui_sheet.Texture, gui_sheet["grey_button01"]),
                Id = 0,
                X = 100,
                Y = 100,
                W = 190,
                H = 45,
                Label = "Button0"
            };
            
            GuiObject btn = new GuiButton(btn_props);
            
            gui.AddCtrl(btn);

        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update();
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            canvas.Begin();
            
            canvas.Clear(Color.CornflowerBlue);
            
            gui.Draw(canvas);
            
            canvas.DrawString(10, 10, $"MousePos: {Input.MousePosition}", 0.25f);
            
            canvas.End();
        }
    }
}