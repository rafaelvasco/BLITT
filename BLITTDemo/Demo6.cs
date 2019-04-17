using System;
using System.Transactions;
using BLITTEngine;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameToolkit.UI;

namespace BLITTDemo
{
    public class Demo6 : Scene
    {
        // Gui Demo
        
        //private TextureAtlas gui_sheet;

        private Gui _gui;
        private bool btn_down;
        private bool check_checked;
        private float slider_value;
        
        public override void Load()
        {
            /*var atlas = new Dictionary<string, Rect>
            {
                {"grey_button00", Rect.FromBox(0,143,190,45)},
                {"grey_button01", Rect.FromBox(0,188,190,49)}
            };*/

            //gui_sheet = TextureAtlas.FromAtlas(Content.Get<Texture2D>("greySheet"), atlas);
    
            _gui = new Gui(0, 0, Game.ScreenSize.W, Game.ScreenSize.H);

            var panel = _gui.Root.AddPanel();

            panel.X = 10;
            panel.Y = 10;
            panel.W = Game.ScreenSize.W - 20;
            panel.H = Game.ScreenSize.H - 20;

            var container = panel.AddVerticalContainer();

            container.AlignVertical = VAlignment.Top;
            container.AlignHorizontal = HAlignment.Stretch;

            
            var btn = container.AddButton("Click Me");
            btn.OnPressed += BtnOnOnPressed;
            btn.OnReleased += BtnOnOnReleased;
            
            var check = container.AddCheckbox();

            check.OnCheck += CheckOnOnCheck;

            var slider = container.AddSlider(0, 100, 1, Orientation.Horizontal);
            
            slider.OnValueChange += SliderOnOnValueChange;

            var panel2 = container.AddPanel();
            
            var horiz_container = panel2.AddHorizontalContainer();

            horiz_container.AlignVertical = VAlignment.Center;
            horiz_container.AlignHorizontal = HAlignment.Center;

            horiz_container.AddButton("Button1");
            horiz_container.AddButton("Button2");
            horiz_container.AddButton("Button3");
        }

        private void SliderOnOnValueChange(object sender, int value)
        {
            slider_value = value;
        }

        private void CheckOnOnCheck(object sender, bool check)
        {
            check_checked = check;
        }

        private void BtnOnOnReleased(object sender, EventArgs e)
        {
            btn_down = false;
        }

        private void BtnOnOnPressed(object sender, EventArgs e)
        {
            btn_down = true;
        }

        public override void Init()
        {
        }

        public override void End()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _gui.Update();
           
        }

        public override void Draw(Canvas canvas, GameTime gameTime)
        {
            _gui.Draw(canvas);
            
            
            //canvas.DrawText(20, 10, $"Draw Calls: {canvas.MaxDrawCalls}", Color.White, 0.25f);
            //canvas.DrawText(20, 30, $"Button Down: {btn_down}", Color.White, 0.25f);
            //canvas.DrawText(20, 50, $"Check Checked: {check_checked}", Color.White, 0.25f);
            //canvas.DrawText(20, 70, $"Slider Value: {slider_value}", Color.White, 0.25f);
        }
    }
}