using System.Runtime.CompilerServices;
using BLITTEngine.Core.Input.Mouse;

namespace BLITTEngine.Core.Platform
{
    internal partial class SDLGamePlatform
    {
        private MouseState mouse_state;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MouseButton TranslatePlatformMouseButton(byte button)
        {
            switch (button)
            {
                case 0:

                    return MouseButton.Left;

                case 2:

                    return MouseButton.Middle;

                case 1:

                    return MouseButton.Right;
            }

            return MouseButton.None;
        }

        private void SetMouseButtonState(byte sdl_button, bool down)
        {
            MouseButton button = TranslatePlatformMouseButton(sdl_button);
            mouse_state[button] = down;
        }

        private void TriggerMouseScroll(int value)
        {
            OnMouseScroll?.Invoke(value);
        }

        public override ref readonly MouseState GetMouseState()
        {
            return ref mouse_state;
        }
    }
}