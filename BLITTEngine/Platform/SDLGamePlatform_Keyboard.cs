﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLITTEngine.Input;
using static BLITTEngine.Foundation.SDL;

namespace BLITTEngine.Platform
{
    internal partial class SDLGamePlatform
    {
        private Dictionary<int, Key> key_map;
        private KeyboardState last_kb_state = new KeyboardState();

        private void InitializeKeyboard()
        {
            key_map = new Dictionary<int, Key>
            {
                {(int) SDL_Keycode.SDLK_a, Key.A},
                {(int) SDL_Keycode.SDLK_b, Key.B},
                {(int) SDL_Keycode.SDLK_c, Key.C},
                {(int) SDL_Keycode.SDLK_d, Key.D},
                {(int) SDL_Keycode.SDLK_e, Key.E},
                {(int) SDL_Keycode.SDLK_f, Key.F},
                {(int) SDL_Keycode.SDLK_g, Key.G},
                {(int) SDL_Keycode.SDLK_h, Key.H},
                {(int) SDL_Keycode.SDLK_i, Key.I},
                {(int) SDL_Keycode.SDLK_j, Key.J},
                {(int) SDL_Keycode.SDLK_k, Key.K},
                {(int) SDL_Keycode.SDLK_l, Key.L},
                {(int) SDL_Keycode.SDLK_m, Key.M},
                {(int) SDL_Keycode.SDLK_n, Key.N},
                {(int) SDL_Keycode.SDLK_o, Key.O},
                {(int) SDL_Keycode.SDLK_p, Key.P},
                {(int) SDL_Keycode.SDLK_q, Key.Q},
                {(int) SDL_Keycode.SDLK_r, Key.R},
                {(int) SDL_Keycode.SDLK_s, Key.S},
                {(int) SDL_Keycode.SDLK_t, Key.T},
                {(int) SDL_Keycode.SDLK_u, Key.U},
                {(int) SDL_Keycode.SDLK_v, Key.V},
                {(int) SDL_Keycode.SDLK_w, Key.W},
                {(int) SDL_Keycode.SDLK_x, Key.X},
                {(int) SDL_Keycode.SDLK_y, Key.Y},
                {(int) SDL_Keycode.SDLK_z, Key.Z},
                {(int) SDL_Keycode.SDLK_0, Key.D0},
                {(int) SDL_Keycode.SDLK_1, Key.D1},
                {(int) SDL_Keycode.SDLK_2, Key.D2},
                {(int) SDL_Keycode.SDLK_3, Key.D3},
                {(int) SDL_Keycode.SDLK_4, Key.D4},
                {(int) SDL_Keycode.SDLK_5, Key.D5},
                {(int) SDL_Keycode.SDLK_6, Key.D6},
                {(int) SDL_Keycode.SDLK_7, Key.D7},
                {(int) SDL_Keycode.SDLK_8, Key.D8},
                {(int) SDL_Keycode.SDLK_9, Key.D9},
                {(int) SDL_Keycode.SDLK_KP_0, Key.NumPad0},
                {(int) SDL_Keycode.SDLK_KP_1, Key.NumPad1},
                {(int) SDL_Keycode.SDLK_KP_2, Key.NumPad2},
                {(int) SDL_Keycode.SDLK_KP_3, Key.NumPad3},
                {(int) SDL_Keycode.SDLK_KP_4, Key.NumPad4},
                {(int) SDL_Keycode.SDLK_KP_5, Key.NumPad5},
                {(int) SDL_Keycode.SDLK_KP_6, Key.NumPad6},
                {(int) SDL_Keycode.SDLK_KP_7, Key.NumPad7},
                {(int) SDL_Keycode.SDLK_KP_8, Key.NumPad8},
                {(int) SDL_Keycode.SDLK_KP_9, Key.NumPad9},
                {(int) SDL_Keycode.SDLK_KP_DECIMAL, Key.Decimal},
                {(int) SDL_Keycode.SDLK_KP_ENTER, Key.Enter},
                {(int) SDL_Keycode.SDLK_KP_PLUS, Key.Add},
                {(int) SDL_Keycode.SDLK_KP_MINUS, Key.Subtract},
                {(int) SDL_Keycode.SDLK_KP_MULTIPLY, Key.Multiply},
                {(int) SDL_Keycode.SDLK_KP_DIVIDE, Key.Divide},
                {(int) SDL_Keycode.SDLK_F1, Key.F1},
                {(int) SDL_Keycode.SDLK_F2, Key.F2},
                {(int) SDL_Keycode.SDLK_F3, Key.F3},
                {(int) SDL_Keycode.SDLK_F4, Key.F4},
                {(int) SDL_Keycode.SDLK_F5, Key.F5},
                {(int) SDL_Keycode.SDLK_F6, Key.F6},
                {(int) SDL_Keycode.SDLK_F7, Key.F7},
                {(int) SDL_Keycode.SDLK_F8, Key.F8},
                {(int) SDL_Keycode.SDLK_F9, Key.F9},
                {(int) SDL_Keycode.SDLK_F10, Key.F10},
                {(int) SDL_Keycode.SDLK_F11, Key.F11},
                {(int) SDL_Keycode.SDLK_F12, Key.F12},
                {(int) SDL_Keycode.SDLK_F13, Key.F13},
                {(int) SDL_Keycode.SDLK_F14, Key.F14},
                {(int) SDL_Keycode.SDLK_F15, Key.F15},
                {(int) SDL_Keycode.SDLK_F16, Key.F16},
                {(int) SDL_Keycode.SDLK_F17, Key.F17},
                {(int) SDL_Keycode.SDLK_F18, Key.F18},
                {(int) SDL_Keycode.SDLK_F19, Key.F19},
                {(int) SDL_Keycode.SDLK_F20, Key.F20},
                {(int) SDL_Keycode.SDLK_F21, Key.F21},
                {(int) SDL_Keycode.SDLK_F22, Key.F22},
                {(int) SDL_Keycode.SDLK_F23, Key.F23},
                {(int) SDL_Keycode.SDLK_F24, Key.F24},
                {(int) SDL_Keycode.SDLK_SPACE, Key.Space},
                {(int) SDL_Keycode.SDLK_UP, Key.Up},
                {(int) SDL_Keycode.SDLK_DOWN, Key.Down},
                {(int) SDL_Keycode.SDLK_LEFT, Key.Left},
                {(int) SDL_Keycode.SDLK_RIGHT, Key.Right},
                {(int) SDL_Keycode.SDLK_LALT, Key.LeftAlt},
                {(int) SDL_Keycode.SDLK_RALT, Key.RightAlt},
                {(int) SDL_Keycode.SDLK_LCTRL, Key.LeftControl},
                {(int) SDL_Keycode.SDLK_RCTRL, Key.RightControl},
                {(int) SDL_Keycode.SDLK_LSHIFT, Key.LeftShift},
                {(int) SDL_Keycode.SDLK_RSHIFT, Key.RightShift},
                {(int) SDL_Keycode.SDLK_DELETE, Key.Delete},
                {(int) SDL_Keycode.SDLK_BACKSPACE, Key.Back},
                {(int) SDL_Keycode.SDLK_RETURN, Key.Enter},
                {(int) SDL_Keycode.SDLK_ESCAPE, Key.Escape},
                {(int) SDL_Keycode.SDLK_TAB, Key.Tab},
                {(int) SDL_Keycode.SDLK_END, Key.End},
                {(int) SDL_Keycode.SDLK_HOME, Key.Home},
                {(int) SDL_Keycode.SDLK_UNKNOWN, Key.None}
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Key TranslatePlatformKey(int keyCode)
        {
            if (key_map.TryGetValue(keyCode, out Key key))
            {
                return key;
            }

            return Key.None;
        }

        private void AddKey(int keyCode)
        {
            var key = TranslatePlatformKey(keyCode);

            if (key == Key.None)
            {
                return;
            }

            last_kb_state.SetKey(key);
        }

        public void RemoveKey(int keyCode)
        {
            var key = TranslatePlatformKey(keyCode);

            last_kb_state.ClearKey(key);

        }


        public override ref KeyboardState GetKeyboardState()
        {
            return ref last_kb_state;
        }
    }
}
