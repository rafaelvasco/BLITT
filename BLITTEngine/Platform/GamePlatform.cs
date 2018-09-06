using System;
using BLITTEngine.Graphics;

namespace BLITTEngine.Platform
{
    internal abstract class GamePlatform
    {
        public delegate void KeyEvent(int keyCode, int scanCode);
        public delegate void MouseMoveEvent(int x, int y);
        public delegate void MouseButtonEvent(int buttonID);
        public delegate void MouseScrollEvent(int x, int y);
        public delegate void JoyDeviceEvent(int deviceID);
        public delegate void JoyButtonEvent(int deviceID, int buttonID);
        public delegate void JoyAxisEvent(int deviceID, int axisID, float value);

        public Action OnQuit;
        public KeyEvent OnKeyDown;
        public KeyEvent OnKeyUp;
        public MouseMoveEvent OnMouseMove;
        public MouseButtonEvent OnMouseButtonDown;
        public MouseButtonEvent OnMouseButtonUp;
        public MouseScrollEvent OnMouseScroll;
        public JoyDeviceEvent OnJoyDeviceAdd;
        public JoyDeviceEvent OnJoyDeviceRemove;
        public JoyButtonEvent OnJoyButtonDown;
        public JoyButtonEvent OnJoyButtonUp;
        public JoyAxisEvent OnJoyAxisMove;

        public Action OnWinResized;
        public Action OnWinMinimized;
        public Action OnWinRestored;
       

        public abstract bool IsFullscreen { get; }
        public abstract IntPtr NativeDisplayHandle { get; }
        public abstract uint WindowID { get; }

        public abstract void Init(string title, int width, int height, GraphicsBackend graphics_backend);
        public abstract void Quit();
        public abstract void PollEvents();
        public abstract void GetWindowSize(out int w, out int h);
        public abstract void SetWindowSize(int w, int h);
        public abstract void SetWindowTitle(string title);
        public abstract void ShowWindowCursor(bool show);
        public abstract void ShowWindow(bool show);
        public abstract void SetFullscreen(bool enabled);
    }
}