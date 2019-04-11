namespace BLITTEngine.Core.Control.Mouse
{
    public struct MouseState
    {
        private MouseButton button_state;

        public bool this[MouseButton button]
        {
            get => (button_state & button) == button;
            set
            {
                if (value)
                {
                    button_state |= button;
                }
                else
                {
                    button_state &= ~button;
                }
            }
        }
    }
}