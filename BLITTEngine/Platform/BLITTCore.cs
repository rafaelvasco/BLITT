using System;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Input.Mouse;

namespace BLITTEngine.Platform
{
    internal abstract class BLITTCore
    {
        public Action OnQuit;
        public Action<int> OnMouseScroll;
        public Action<int, int> OnWinResized;

        public abstract bool IsFullscreen { get; }

        
        /* CORE */
        public abstract void Init(string title, int width, int height, bool fullscreen);
        public abstract void Quit();
        public abstract void PollEvents();
        public abstract void GetScreenSize(out int w, out int h);
        public abstract void SetScreenSize(int w, int h);
        public abstract void SetTitle(string title);
        public abstract void ShowCursor(bool show);
        public abstract void ShowScreen(bool show);
        public abstract void SetFullscreen(bool enabled);
        
        /* INPUT */
        public abstract ref KeyboardState GetKeyboardState();
        public abstract ref MouseState GetMouseState();
        
        /* GRAPHICS */
        
        public abstract Pixmap RenderTarget { get; }
        
        public abstract void SubmitRender(ref DrawProps props);

    }
}