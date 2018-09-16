using System;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Input.Mouse;

namespace BLITTEngine.Platform
{
    internal abstract class GamePlatform
    {
        public Action OnQuit;
        public Action<int> OnMouseScroll;
       

        public Action<int, int> OnWinResized;
        public Action OnWinMinimized;
        public Action OnWinRestored;
       

        public abstract bool IsFullscreen { get; }
        public abstract IntPtr NativeDisplayHandle { get; }
        public abstract GraphicsModule Graphics { get; }

        public abstract void Init(string title, int width, int height, bool fullscreen, GraphicsBackend graphics_backend);
        public abstract void Quit();
        public abstract void PollEvents();
        public abstract void GetScreenSize(out int w, out int h);
        public abstract void SetScreenSize(int w, int h);
        public abstract void SetTitle(string title);
        public abstract void ShowCursor(bool show);
        public abstract void ShowScreen(bool show);
        public abstract void SetFullscreen(bool enabled);
        public abstract ref KeyboardState GetKeyboardState();
        public abstract ref MouseState GetMouseState();
        
    }
}