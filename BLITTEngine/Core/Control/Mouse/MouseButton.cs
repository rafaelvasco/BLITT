using System;

namespace BLITTEngine.Core.Control.Mouse
{
    [Flags]
    public enum MouseButton : byte
    {
        None = 0,
        Left = 1 << 1,
        Right = 1 << 2,
        Middle = 1 << 3
    }
}