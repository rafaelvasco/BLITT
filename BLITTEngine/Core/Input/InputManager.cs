using System;
using BLITTEngine.Core.Input.GamePad;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Input.Mouse;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Platform;

namespace BLITTEngine.Core.Input
{
    public class InputManager
    {
        public static Action<int> OnMouseWheel;

        private GamepadState gp_current_state;
        private GamepadState gp_prev_state;

        private KeyboardState kb_current_state;
        private KeyboardState kb_prev_state;

        private MouseState ms_current_state;
        private MouseState ms_prev_state;

        private readonly GamePlatform platform;

        internal InputManager(GamePlatform game_platform)
        {
            platform = game_platform;
            platform.OnMouseScroll += OnPlatformMouseScroll;
        }

        public GamepadDeadZoneMode GamepadDeadZoneMode
        {
            get => platform.GamepadDeadZoneMode;
            set => platform.GamepadDeadZoneMode = value;
        }

        public Vector2 LeftThumbstickAxis => gp_current_state.Thumbsticks.Left;

        public Vector2 RightThumbstickAxis => gp_current_state.Thumbsticks.Right;

        public float LeftTriggerValue => gp_current_state.Triggers.Left;

        public float RightTriggerValue => gp_current_state.Triggers.Right;

        private void OnPlatformMouseScroll(int value)
        {
            OnMouseWheel?.Invoke(value);
        }

        internal void Update()
        {
            kb_prev_state = kb_current_state;
            kb_current_state = platform.GetKeyboardState();

            ms_prev_state = ms_current_state;
            ms_current_state = platform.GetMouseState();

            gp_prev_state = gp_current_state;
            gp_current_state = platform.GetGamepadState();
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

        public bool ButtonDown(GamepadButton button)
        {
            return gp_current_state[button];
        }

        public bool ButtonPressed(GamepadButton button)
        {
            return gp_current_state[button] && !gp_prev_state[button];
        }

        public bool ButtonReleased(GamepadButton button)
        {
            return !gp_current_state[button] && gp_prev_state[button];
        }

        public void GetMousePosition(ref Point2 pos)
        {
            pos.X = ms_current_state.X;
            pos.Y = ms_current_state.Y;
        }
    }
}