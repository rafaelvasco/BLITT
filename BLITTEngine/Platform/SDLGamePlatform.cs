using System;
using System.Diagnostics;
using BLITTEngine.Foundation;

namespace BLITTEngine.Platform
{
    internal partial class SDLGamePlatform : GamePlatform
    {
        private IntPtr window;
        private uint window_id;
        private IntPtr native_handle;
        private bool is_fullscreen;
        private SDLGpuGraphicsModule sdl_gpu_graphics;

        private int prev_win_w;
        private int prev_win_h;
        private int screen_w;
        private int screen_h;

        public override bool IsFullscreen => is_fullscreen;
        public override IntPtr NativeDisplayHandle => native_handle;
        public override GraphicsModule Graphics => sdl_gpu_graphics;


        public override void Init(string title, int width, int height, bool fullscreen, GraphicsBackend graphics_backend)
        {
            Console.WriteLine($"GamePlatform Starting: Graphics Backend: {graphics_backend}, Base Screen W: {width}, Base Screen W: {height}");

            prev_win_w = width;
            prev_win_h = height;
            is_fullscreen = fullscreen;
            
            var initFlags = SDL.InitFlags.Video
                            | SDL.InitFlags.Joystick;
            
            var sw = Stopwatch.StartNew();
            
            SDL.Init((int) initFlags);
            
            Console.WriteLine($"Init sdl took: {sw.Elapsed.TotalSeconds}");

            var windowFlags =
                SDL.Window.State.Hidden;

            switch (graphics_backend)
            {
                case GraphicsBackend.OpenGL:
                    
                    windowFlags |= SDL.Window.State.OpenGL;
                    break;
                case GraphicsBackend.Vulkan:
                    throw new Exception("Vulkan Graphics Backend is not implemented yet.");
                    break;
                case GraphicsBackend.Direct3D:
                    throw new Exception("Direct3D12 Graphics Backend is not implemented yet.");
                    break;
            }

            if (fullscreen)
            {
                windowFlags |= SDL.Window.State.FullscreenDesktop;
            }
            
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


            window_id = SDL.Window.GetWindowID(window);
            native_handle = GetWindowNativeHandle();
            
            sdl_gpu_graphics = new SDLGpuGraphicsModule(window_id, screen_w, screen_h);

            Console.WriteLine($"Graphics took: {sw.Elapsed.TotalSeconds}");
            
            sw.Stop();

            InitializeKeyboard();

        }
        
        private IntPtr GetWindowNativeHandle()
        {
            SDL.Window.SDL_SysWMinfo wmInfo = new SDL.Window.SDL_SysWMinfo();

            SDL.Window.GetWindowWMInfo(window, ref wmInfo);

            if(wmInfo.subsystem == SDL.Window.SysWMType.Windows)
            {
                return wmInfo.info.win.window;
            }
            else if (wmInfo.subsystem == SDL.Window.SysWMType.X11)
            {
                return wmInfo.info.x11.window;
            }
            else if (wmInfo.subsystem == SDL.Window.SysWMType.Cocoa)
            {
                return wmInfo.info.cocoa.window;
            }

            return window;
        }

        public override void Quit()
        {
            sdl_gpu_graphics.Terminate();
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

                                Graphics.Resize(screen_w, screen_h);
                                break;

                            case SDL.Window.EventId.Close:
                                OnQuit?.Invoke();
                                break;
                            case SDL.Window.EventId.SizeChanged:
                                
                                int w = ev.Window.Data1;
                                int h = ev.Window.Data2;

                                if (screen_w != w || screen_h != h)
                                {
                                    Console.WriteLine($"GRAPHICS RESIZE: {w}, {h}");
                                    Graphics.Resize(w, h);
                                }

                                screen_w = w;
                                screen_h = h;
                                
                                OnWinResized?.Invoke(ev.Window.Data1, ev.Window.Data2);
                                break;
                            case SDL.Window.EventId.Minimized:
                                OnWinMinimized?.Invoke();
                                break;
                            case SDL.Window.EventId.Restored:
                                OnWinRestored?.Invoke();
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