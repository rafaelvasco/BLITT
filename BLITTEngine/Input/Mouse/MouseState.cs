namespace BLITTEngine.Input.Mouse
{
    public struct MouseState
    {
        private MouseButton button_state;
        
        public int X { get; }

        public int Y { get; }

        public int ScrollValue { get; internal set; }
        
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
        
        internal MouseState(int x, int y, int scroll) : this()
        {
            button_state = 0x0;

            X = x;
            Y = y;

            ScrollValue = scroll;


        }
        
    }
}