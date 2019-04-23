using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control.GamePad;
using BLITTEngine.Core.Control.Keyboard;
using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Platform;

namespace BLITTEngine.Core.Control
{
    public static class Input
    {
        internal static Canvas Canvas;
        
        public static bool AnyKeyDown => kb_current_state.PressedCount > 0;

        public static bool IsMouseOver { get; internal set; }

        public static Point2 MousePos => new Point2(
            (int)(_mouse_position.X / Canvas.RenderScaleX - Canvas.RenderAreaTopLeft.X / Canvas.RenderScaleX) , 
            (int)(_mouse_position.Y / Canvas.RenderScaleY - Canvas.RenderAreaTopLeft.Y / Canvas.RenderScaleY)); 
        
        public static int MouseWheel => _mouse_wheel;
        
        private static GamepadState gp_current_state;
        private static GamepadState gp_prev_state;

        private static KeyboardState kb_current_state;
        private static KeyboardState kb_prev_state;

        private static MouseState ms_current_state;
        private static MouseState ms_prev_state;

        private static int _mouse_wheel;

        private static Point2 _mouse_position;
        
        private static GamePlatform platform;


        internal static void Init(GamePlatform game_platform)
        {
            platform = game_platform;
            platform.OnMouseScroll += OnPlatformMouseScroll;
            platform.OnMouseOver += OnPlatformMouseOver;
            platform.OnMouseLeave += OnPlatformMouseLeave;
        }

        private static void OnPlatformMouseLeave()
        {
            IsMouseOver = false;
        }

        private static void OnPlatformMouseOver()
        {
            IsMouseOver = true;
        }

        public static GamepadDeadZoneMode GamepadDeadZoneMode
        {
            get => platform.GamepadDeadZoneMode;
            set => platform.GamepadDeadZoneMode = value;
        }

        public static Vector2 LeftThumbstickAxis => gp_current_state.Thumbsticks.Left;

        public static Vector2 RightThumbstickAxis => gp_current_state.Thumbsticks.Right;

        public static float LeftTriggerValue => gp_current_state.Triggers.Left;

        public static float RightTriggerValue => gp_current_state.Triggers.Right;

        private static void OnPlatformMouseScroll(int value)
        {
            _mouse_wheel += value;
        }

        internal static void Update()
        {
            kb_prev_state = kb_current_state;
            kb_current_state = platform.GetKeyboardState();

            ms_prev_state = ms_current_state;
            ms_current_state = platform.GetMouseState();
            
            platform.GetMousePosition(out _mouse_position);

            gp_prev_state = gp_current_state;
            gp_current_state = platform.GetGamepadState();
        }

        internal static void PostUpdate()
        {
            _mouse_wheel = 0;
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
    }
}