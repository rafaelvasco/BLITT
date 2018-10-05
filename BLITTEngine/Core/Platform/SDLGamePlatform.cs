using System;
using System.Diagnostics;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Foundation;

namespace BLITTEngine.Core.Platform
{
    internal partial class SDLGamePlatform : GamePlatform
    {
        private IntPtr window;
        private bool is_fullscreen;

        private int prev_win_w;
        private int prev_win_h;
        private int screen_w;
        private int screen_h;

        public override bool IsFullscreen => is_fullscreen;

        public override void Init(string title, int width, int height, bool fullscreen)
        {
            Console.WriteLine($"BLITT CORE STARTING: RESOLUTION: {width}, {height}");

            prev_win_w = width;
            prev_win_h = height;
            is_fullscreen = fullscreen;

            var initFlags = SDL.InitFlags.Video
                            | SDL.InitFlags.Joystick;

            SDL.SetHint("SDL_WINDOWS_DISABLE_THREAD_NAMING", "1");

            var sw = Stopwatch.StartNew();

            SDL.Init((int) initFlags);

            Console.WriteLine($"Init sdl took: {sw.Elapsed.TotalSeconds}");

            var windowFlags =
                SDL.Window.State.Hidden | SDL.Window.State.OpenGL;

            if (fullscreen)
            {
                windowFlags |= SDL.Window.State.FullscreenDesktop;
            }

            SDL.GL.SetAttribute(SDL.GL.Attribute.ContextMajorVersion, 3);
            SDL.GL.SetAttribute(SDL.GL.Attribute.ContextMinorVersion, 3);

            window = SDL.Window.Create(
                title,
                SDL.Window.PosCentered,
                SDL.Window.PosCentered,
                width, height, windowFlags);

            if (window == IntPtr.Zero)
            {
                SDL.Quit();
                throw new Exception(SDL.GetError());
            }

            if (fullscreen)
            {
                SDL.Display.GetDisplayMode(0, 0, out SDL.Display.Mode dm);

                screen_w = dm.Width;
                screen_h = dm.Height;

                Console.WriteLine($"FULLSCREEN RES: {screen_w},{screen_h}");
            }
            else
            {
                screen_w = prev_win_w;
                screen_h = prev_win_h;
            }

            Console.WriteLine($"Create window took: {sw.Elapsed.TotalSeconds}");

            sw.Stop();

            InitKeyboard();

        }

        public override IntPtr GetRenderSurfaceHandle()
        {
            var info = new SDL.Window.SDL_SysWMinfo();

            SDL.Window.GetWindowWMInfo(window, ref info);

            switch (CurrentPlatform.OS)
            {
                case OS.Windows:
                    return info.info.win.window;
                case OS.Linux:
                    return info.info.x11.window;
                case OS.MacOSX:
                    return info.info.cocoa.window;
            }

            throw new Exception(
                "SDLGamePlatform [GetRenderSurfaceHandle]: " +
                "Invalid OS, could not retrive native renderer surface handle.");

        }

        public override void Quit()
        {
            SDL.Quit();
        }

        public override void PollEvents()
        {
            while(SDL.PollEvent(out var ev) == 1)
            {
                switch (ev.Type)
                {
                    case SDL.EventType.Quit:
                        OnQuit?.Invoke();
                        break;
                    case SDL.EventType.KeyDown:
                        AddKey(ev.Key.Keysym.Sym);
                        break;
                    case SDL.EventType.KeyUp:
                        RemoveKey(ev.Key.Keysym.Sym);
                        break;
                    case SDL.EventType.MouseButtonDown:
                        SetMouseButtonState(ev.Button.Button, down: true);
                        break;
                    case SDL.EventType.MouseButtonup:
                        SetMouseButtonState(ev.Button.Button, down: false);
                        break;
                    case SDL.EventType.MouseWheel:
                        TriggerMouseScroll(ev.Wheel.Y*120);
                        break;
                    case SDL.EventType.JoyDeviceAdded:
                        break;
                    case SDL.EventType.JoyDeviceRemoved:
                        break;
                    case SDL.EventType.JoyButtonDown:
                        break;
                    case SDL.EventType.JoyButtonUp:
                        break;
                    case SDL.EventType.JoyAxisMotion:
                        break;
                    case SDL.EventType.WindowEvent:
                        switch (ev.Window.EventID)
                        {
                            case SDL.Window.EventId.Shown:

                                break;

                            case SDL.Window.EventId.Close:
                                OnQuit?.Invoke();
                                break;
                            case SDL.Window.EventId.SizeChanged:

                                int w = ev.Window.Data1;
                                int h = ev.Window.Data2;

                                if (screen_w != w || screen_h != h)
                                {
                                }

                                screen_w = w;
                                screen_h = h;

                                OnWinResized?.Invoke(ev.Window.Data1, ev.Window.Data2);
                                break;

                        }
                        break;

                }
            }
        }

        public override void GetScreenSize(out int w, out int h)
        {
            w = screen_w;
            h = screen_h;
        }

        public override void SetScreenSize(int w, int h)
        {
            if(is_fullscreen)
            {
                return;
            }

            prev_win_w = w;
            prev_win_h = h;

            SDL.Window.SetSize(window, w, h);
            SDL.Window.SetPosition(window, SDL.Window.PosCentered, SDL.Window.PosCentered);
        }

        public override void SetTitle(string title)
        {
            SDL.Window.SetTitle(window, title);
        }

        public override void ShowCursor(bool show)
        {
            SDL.Mouse.ShowCursor(show ? 1 : 0);
        }

        public override void ShowScreen(bool show)
        {
            SDL.Window.Show(window);
        }

        public override void SetFullscreen(bool enabled)
        {
            if (is_fullscreen != enabled)
            {

                SDL.Window.SetFullscreen(window, enabled ? SDL.Window.State.FullscreenDesktop :  0);

                is_fullscreen = enabled;

                if (!is_fullscreen)
                {
                    SetScreenSize(prev_win_w, prev_win_h);
                }
            }
        }
    }
}