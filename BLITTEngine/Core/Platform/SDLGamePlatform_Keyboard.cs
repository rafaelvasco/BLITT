using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Input.Keyboard;

namespace BLITTEngine.Core.Platform
{
    internal partial class SDLGamePlatform
    {
        private Dictionary<int, Key> key_map;
        private KeyboardState last_kb_state;

        private void InitKeyboard()
        {
            key_map = new Dictionary<int, Key>();

            key_map.Add(8, Key.Back);
            key_map.Add(9, Key.Tab);
            key_map.Add(13, Key.Enter);
            key_map.Add(27, Key.Escape);
            key_map.Add(32, Key.Space);
            key_map.Add(43, Key.Add);
            key_map.Add(48, Key.D0);
            key_map.Add(49, Key.D1);
            key_map.Add(50, Key.D2);
            key_map.Add(51, Key.D3);
            key_map.Add(52, Key.D4);
            key_map.Add(53, Key.D5);
            key_map.Add(54, Key.D6);
            key_map.Add(55, Key.D7);
            key_map.Add(56, Key.D8);
            key_map.Add(57, Key.D9);
            key_map.Add(97, Key.A);
            key_map.Add(98, Key.B);
            key_map.Add(99, Key.C);
            key_map.Add(100, Key.D);
            key_map.Add(101, Key.E);
            key_map.Add(102, Key.F);
            key_map.Add(103, Key.G);
            key_map.Add(104, Key.H);
            key_map.Add(105, Key.I);
            key_map.Add(106, Key.J);
            key_map.Add(107, Key.K);
            key_map.Add(108, Key.L);
            key_map.Add(109, Key.M);
            key_map.Add(110, Key.N);
            key_map.Add(111, Key.O);
            key_map.Add(112, Key.P);
            key_map.Add(113, Key.Q);
            key_map.Add(114, Key.R);
            key_map.Add(115, Key.S);
            key_map.Add(116, Key.T);
            key_map.Add(117, Key.U);
            key_map.Add(118, Key.V);
            key_map.Add(119, Key.W);
            key_map.Add(120, Key.X);
            key_map.Add(121, Key.Y);
            key_map.Add(122, Key.Z);
            key_map.Add(127, Key.Delete);
            key_map.Add(1073741882, Key.F1);
            key_map.Add(1073741883, Key.F2);
            key_map.Add(1073741884, Key.F3);
            key_map.Add(1073741885, Key.F4);
            key_map.Add(1073741886, Key.F5);
            key_map.Add(1073741887, Key.F6);
            key_map.Add(1073741888, Key.F7);
            key_map.Add(1073741889, Key.F8);
            key_map.Add(1073741890, Key.F9);
            key_map.Add(1073741891, Key.F10);
            key_map.Add(1073741892, Key.F11);
            key_map.Add(1073741893, Key.F12);
            key_map.Add(1073741898, Key.Home);
            key_map.Add(1073741901, Key.End);
            key_map.Add(1073741903, Key.Right);
            key_map.Add(1073741904, Key.Left);
            key_map.Add(1073741905, Key.Down);
            key_map.Add(1073741906, Key.Up);
            key_map.Add(1073741908, Key.Divide);
            key_map.Add(1073741909, Key.Multiply);
            key_map.Add(1073741910, Key.Subtract);
            key_map.Add(1073741911, Key.Add);
            key_map.Add(1073741912, Key.Enter);
            key_map.Add(1073741913, Key.NumPad1);
            key_map.Add(1073741914, Key.NumPad2);
            key_map.Add(1073741915, Key.NumPad3);
            key_map.Add(1073741916, Key.NumPad4);
            key_map.Add(1073741917, Key.NumPad5);
            key_map.Add(1073741918, Key.NumPad6);
            key_map.Add(1073741919, Key.NumPad7);
            key_map.Add(1073741920, Key.NumPad8);
            key_map.Add(1073741921, Key.NumPad9);
            key_map.Add(1073741922, Key.NumPad0);
            key_map.Add(1073741923, Key.Decimal);
            key_map.Add(1073741928, Key.F13);
            key_map.Add(1073741929, Key.F14);
            key_map.Add(1073741930, Key.F15);
            key_map.Add(1073741931, Key.F16);
            key_map.Add(1073741932, Key.F17);
            key_map.Add(1073741933, Key.F18);
            key_map.Add(1073741934, Key.F19);
            key_map.Add(1073741935, Key.F20);
            key_map.Add(1073741936, Key.F21);
            key_map.Add(1073741937, Key.F22);
            key_map.Add(1073741938, Key.F23);
            key_map.Add(1073741939, Key.F24);
            key_map.Add(1073742044, Key.Decimal);
            key_map.Add(1073742048, Key.LeftControl);
            key_map.Add(1073742049, Key.LeftShift);
            key_map.Add(1073742050, Key.LeftAlt);
            key_map.Add(1073742052, Key.RightControl);
            key_map.Add(1073742053, Key.RightShift);
            key_map.Add(1073742054, Key.RightAlt);
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