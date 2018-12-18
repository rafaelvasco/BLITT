using System;
using BLITTEngine.Core.Input.GamePad;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Input.Mouse;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Platform;

namespace BLITTEngine.Core.Input
{
    public static class Control
    {
        public static Action<int> OnMouseWheel;

        public static GamepadDeadZoneMode GamepadDeadZoneMode
        {
            get => platform.GamepadDeadZoneMode;
            set => platform.GamepadDeadZoneMode = value;
        }

        public static Vector2 LeftThumbstickAxis => gp_current_state.Thumbsticks.Left;

        public static Vector2 RightThumbstickAxis => gp_current_state.Thumbsticks.Right;

        public static float LeftTriggerValue => gp_current_state.Triggers.Left;

        public static float RightTriggerValue => gp_current_state.Triggers.Right;

        private static GamePlatform platform;

        private static KeyboardState kb_current_state;
        private static KeyboardState kb_prev_state;

        private static MouseState ms_current_state;
        private static MouseState ms_prev_state;

        private static GamepadState gp_current_state;
        private static GamepadState gp_prev_state;


        internal static void Init(GamePlatform game_platform)
        {
            platform = game_platform;
            platform.OnMouseScroll += OnPlatformMouseScroll;
        }

        private static void OnPlatformMouseScroll(int value)
        {
            OnMouseWheel?.Invoke(value);
        }

        internal static void Update()
        {
            kb_prev_state = kb_current_state;
            kb_current_state = platform.GetKeyboardState();

            ms_prev_state = ms_current_state;
            ms_current_state = platform.GetMouseState();

            gp_prev_state = gp_current_state;
            gp_current_state = platform.GetGamepadState();
        }

        public static bool KeyDown(Key key)
        {
            return kb_current_state[key];
        }

        public static bool KeyPressed(Key key)
        {
            return kb_current_state[key] && !kb_prev_state[key];
        }

        public static bool KeyReleased(Key key)
        {
            return !kb_current_state[key] && kb_prev_state[key];
        }

        public static bool MouseDown(MouseButton button)
        {
            return ms_current_state[button];
        }

        public static bool MousePressed(MouseButton button)
        {
            return ms_current_state[button] && !ms_prev_state[button];
        }

        public static bool MouseReleased(MouseButton button)
        {
            return !ms_current_state[button] && ms_prev_state[button];
        }

        public static bool ButtonDown(GamepadButton button)
        {
            return gp_current_state[button];
        }

        public static bool ButtonPressed(GamepadButton button)
        {
            return gp_current_state[button] && !gp_prev_state[button];
        }

        public static bool ButtonReleased(GamepadButton button)
        {
            return !gp_current_state[button] && gp_prev_state[button];
        }

        public static void GetMousePosition(ref Point2 pos)
        {
            pos.X = ms_current_state.X;
            pos.Y = ms_current_state.Y;
        }
    }
}