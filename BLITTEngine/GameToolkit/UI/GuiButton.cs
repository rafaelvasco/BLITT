using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Foundation.STB;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public class GuiButton : GuiObject
    {
        public class Props
        {
            public int Id;
            public float X;
            public float Y;
            public float W;
            public float H;
            public Sprite UpSprite;
            public Sprite DownSprite;
            public string Label;
        }

        public override void Update()
        {
        
        }

        public override void Draw(Canvas canvas)
        {
            if (_pressed)
            {
                _down_sprite.Draw(canvas, _rect.X1, _rect.Y1);
            }
            else
            {
                _up_sprite.Draw(canvas, _rect.X1, _rect.Y1);
            }
            
        }

        public override bool OnMouseButton(MouseButton button, bool down)
        {
            if (button == MouseButton.Left && down)
            {
                _old_state = _pressed;
                _pressed = true;

                
                return false;
            }

            if (_trigger)
            {
                _pressed = !_old_state;
            }
            else
            {
                _pressed = false;
            }

            return true;
        }

        public GuiButton(Props props) 
            : base(props.Id, props.X, props.Y, props.W, props.H)
        {
            _static = false;
            _visible = true;
            _enabled = true;
            _pressed = false;
            _trigger = false;
            _label = props.Label;
            _up_sprite = props.UpSprite;
            _down_sprite = props.DownSprite;
        }

        private bool _trigger;
        private bool _pressed;
        private bool _old_state;
        private Sprite _up_sprite;
        private Sprite _down_sprite;
        private string _label;
    }
}