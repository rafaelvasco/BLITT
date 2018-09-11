using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BLITTEngine.Foundation;
using BLITTEngine.Input;
using static BLITTEngine.Foundation.SDL;

namespace BLITTEngine.Platform
{
    internal partial class SDLGamePlatform : GamePlatform
    {
        private SDL_Window window;
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
            prev_win_w = width;
            prev_win_h = height;
            is_fullscreen = fullscreen;
            
            var initFlags = SDL_InitFlags.SDL_INIT_VIDEO
                            | SDL_InitFlags.SDL_INIT_JOYSTICK;
            
            var sw = Stopwatch.StartNew();
            
            if (SDL_Init(initFlags) != 0)
            {
                SDL_Quit();
                throw new Exception(SDL_GetError());
            }
            
            Console.WriteLine($"Init sdl took: {sw.Elapsed.TotalSeconds}");

            var windowFlags =
                SDL_WindowFlags.SDL_WINDOW_HIDDEN;


            switch (graphics_backend)
            {
                case GraphicsBackend.OpenGL:
                    
                    windowFlags |= SDL_WindowFlags.SDL_WINDOW_OPENGL;

                    SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
                    SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
                    SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, (int) SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
                    SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_FLAGS, (int) SDL_GLcontextFlag.SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);
                    SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
                    
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
                windowFlags |= SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
            }
            
            window = SDL_CreateWindow(
                title, 
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                width, height, windowFlags);
            
            if (window == IntPtr.Zero)
            {
                SDL_Quit();
                throw new Exception(SDL_GetError());
            }
            
            if (fullscreen)
            {
                SDL_GetDesktopDisplayMode(0, out SDL_DisplayMode dm);

                screen_w = dm.w;
                screen_h = dm.h;
            
                Console.WriteLine($"FULLSCREEN RES: {screen_w},{screen_h}");
            }
            else
            {
                screen_w = prev_win_w;
                screen_h = prev_win_h;
            }
            
            Console.WriteLine($"Create window took: {sw.Elapsed.TotalSeconds}");

            window_id = SDL_GetWindowID(window);
            native_handle = GetWindowNativeHandle();
            
            sdl_gpu_graphics = new SDLGpuGraphicsModule(window_id, screen_w, screen_h);
            
            Console.WriteLine($"Graphics took: {sw.Elapsed.TotalSeconds}");
            
            sw.Stop();

            InitializeKeyboard();

        }
        
        private unsafe IntPtr GetWindowNativeHandle()
        {
            SDL_SysWMinfo wmInfo;

            SDL_GetVersion(&wmInfo.version);

            SDL_GetWindowWMInfo(window, &wmInfo);

            if(wmInfo.subsystem == SDL_SYSWM_TYPE.SDL_SYSWM_WINDOWS)
            {
                return wmInfo.info.win.window;
            }
            else if (wmInfo.subsystem == SDL_SYSWM_TYPE.SDL_SYSWM_X11)
            {
                return wmInfo.info.x11.window;
            }
            else if (wmInfo.subsystem == SDL_SYSWM_TYPE.SDL_SYSWM_COCOA)
            {
                return wmInfo.info.cocoa.window;
            }

            return window;
        }


        public override void Quit()
        {
            sdl_gpu_graphics.Terminate();
            SDL_Quit();
        }

        public override unsafe void PollEvents()
        {
            SDL_Event ev;

            while(SDL_PollEvent(&ev) == 1)
            {
                switch (ev.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        OnQuit?.Invoke();
                        break;
                    case SDL_EventType.SDL_KEYDOWN:
                        AddKey((int)ev.keyboard.keysym.sym);
                        break;
                    case SDL_EventType.SDL_KEYUP:
                        RemoveKey((int)ev.keyboard.keysym.sym);
                        break;
                    case SDL_EventType.SDL_MOUSEMOTION:
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        break;
                    case SDL_EventType.SDL_MOUSEBUTTONUP:
                        break;
                    case SDL_EventType.SDL_MOUSEWHEEL:
                        break;
                    case SDL_EventType.SDL_JOYDEVICEADDED:
                        break;
                    case SDL_EventType.SDL_JOYDEVICEREMOVED:
                        break;
                    case SDL_EventType.SDL_JOYBUTTONDOWN:
                        break;
                    case SDL_EventType.SDL_JOYBUTTONUP:
                        break;
                    case SDL_EventType.SDL_JOYAXISMOTION:
                        break;
                    case SDL_EventType.SDL_WINDOWEVENT:
                        switch (ev.window.evt)
                        {
                            case SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                                OnQuit?.Invoke();
                                break;
                            case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                                
                                int w = ev.window.data1;
                                int h = ev.window.data2;

                                if (screen_w != w || screen_h != h)
                                {
                                    Console.WriteLine("GRAPHICS RESIZE");
                                    Graphics.Resize(w, h);
                                }
                                
                                screen_w = w;
                                screen_h = h;
                                
                                OnWinResized?.Invoke(ev.window.data1, ev.window.data2);
                                break;
                            case SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                                OnWinMinimized?.Invoke();
                                break;
                            case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                                OnWinRestored?.Invoke();
                                break;
                           
                        }
                        break;
                   
                }
            }
        }

        public override void GetWindowSize(out int w, out int h)
        {
            w = screen_w;
            h = screen_h;
        }

        public override void SetWindowSize(int w, int h)
        {
            if(is_fullscreen)
            {
                return;
            }

            prev_win_w = w;
            prev_win_h = h;

            SDL_SetWindowSize(window, w, h);
            SDL_SetWindowPosition(window, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED);
        }

        public override void SetWindowTitle(string title)
        {
            SDL_SetWindowTitle(window, title);
        }

        public override void ShowWindowCursor(bool show)
        {
            SDL_ShowCursor(show ? 1 : 0);
        }

        public override void ShowWindow(bool show)
        {
            SDL_ShowWindow(window);
        }

        public override void SetFullscreen(bool enabled)
        {
            if (is_fullscreen != enabled)
            {

                SDL_SetWindowFullscreen(window, enabled ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0);

                is_fullscreen = enabled;

                if (!is_fullscreen)
                {
                    SetWindowSize(prev_win_w, prev_win_h);                    
                }
            }
        }
    }
}