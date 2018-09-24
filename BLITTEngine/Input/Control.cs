using System;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Input.Mouse;
using BLITTEngine.Numerics;
using BLITTEngine.Platform;

namespace BLITTEngine.Input
{
    public class Control
    {
        public Action<int> OnMouseWheel;

        private BLITTCore core;

        private KeyboardState kb_current_state;
        private KeyboardState kb_prev_state;

        private MouseState ms_current_state;
        private MouseState ms_prev_state;

        internal Control(BLITTCore core)
        {
            this.core = core;
            core.OnMouseScroll += OnPlatformMouseScroll;
        }
        
        private void OnPlatformMouseScroll(int value)
        {
            OnMouseWheel?.Invoke(value);
        }

        internal void Update()
        {
            kb_prev_state = kb_current_state;
            kb_current_state = core.GetKeyboardState();

            ms_prev_state = ms_current_state;
            ms_current_state = core.GetMouseState();
        }

        public bool KeyDown(Key key)
        {
            return kb_current_state[key];
        }

        public bool KeyPressed(Key key)
        {
            return kb_current_state[key] && !kb_prev_state[key];
        }

        public bool KeyReleased(Key key)
        {
            return !kb_current_state[key] && kb_prev_state[key];
        }

        public bool MouseDown(MouseButton button)
        {
            return ms_current_state[button];
        }

        public bool MousePressed(MouseButton button)
        {
            return ms_current_state[button] && !ms_prev_state[button];
        }

        public bool MouseReleased(MouseButton button)
        {
            return !ms_current_state[button] && ms_prev_state[button];
        }

        public void GetPosition(ref Point2 pos)
        {
            pos.X = ms_current_state.X;
            pos.Y = ms_current_state.Y;
        }
    }
}
