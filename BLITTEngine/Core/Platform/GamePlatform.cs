using System;
using BLITTEngine.Core.Input.Keyboard;
using BLITTEngine.Core.Input.Mouse;

namespace BLITTEngine.Core.Platform
{
    internal abstract class GamePlatform
    {
        public Action OnQuit;
        public Action<int> OnMouseScroll;
        public Action<int, int> OnWinResized;

        public abstract bool IsFullscreen { get; }

        /* CORE */

        public abstract void Init(string title, int width, int height, bool fullscreen);

        public abstract void Shutdown();

        public abstract void PollEvents();

        public abstract void GetScreenSize(out int w, out int h);

        public abstract void SetScreenSize(int w, int h);

        public abstract void SetTitle(string title);

        public abstract void ShowCursor(bool show);

        public abstract void ShowScreen(bool show);

        public abstract void SetFullscreen(bool enabled);

        public abstract IntPtr GetRenderSurfaceHandle();

        /* INPUT */

        public abstract ref KeyboardState GetKeyboardState();

        public abstract ref MouseState GetMouseState();
    }
}