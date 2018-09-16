using System.Runtime.CompilerServices;
using BLITTEngine.Foundation;
using BLITTEngine.Input;
using BLITTEngine.Input.Mouse;

namespace BLITTEngine.Platform
{
    internal partial class SDLGamePlatform
    {
        private MouseState mouse_state;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MouseButton TranslatePlatformMouseButton(SDL.Mouse.Button button)
        {
            switch (button)
            {
                case SDL.Mouse.Button.Left:

                    return MouseButton.Left;
                
                case SDL.Mouse.Button.Middle:

                    return MouseButton.Middle;
                
                case SDL.Mouse.Button.Right:

                    return MouseButton.Right;
            }

            return MouseButton.None;
        }

        private void SetMouseButtonState(SDL.Mouse.Button sdl_button, bool down)
        {
            MouseButton button = TranslatePlatformMouseButton(sdl_button);
            mouse_state[button] = down;
        }

        private void TriggerMouseScroll(int value)
        {
            OnMouseScroll?.Invoke(value);
        }

        public override ref MouseState GetMouseState()
        {
            return ref mouse_state;

        }
        
    }
    
}