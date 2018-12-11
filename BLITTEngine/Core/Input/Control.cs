using System;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Input.Mouse;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Platform;

namespace BLITTEngine.Core.Input
{
    public static class Control
    {
        public static Action<int> OnMouseWheel;

        private static GamePlatform platform;

        private static KeyboardState kb_current_state;
        private static KeyboardState kb_prev_state;

        private static MouseState ms_current_state;
        private static MouseState ms_prev_state;

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

        public static void GetPosition(ref Point2 pos)
        {
            pos.X = ms_current_state.X;
            pos.Y = ms_current_state.Y;
        }
    }
}