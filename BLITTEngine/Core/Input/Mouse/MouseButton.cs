using System;

namespace BLITTEngine.Core.Input.Mouse
{
    [Flags]
    public enum MouseButton
    {
        None = 0,
        Left = 1 << 1,
        Right = 1 << 2,
        Middle = 1 << 3
    }
}