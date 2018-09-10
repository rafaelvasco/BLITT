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

        

        public override bool IsFullscreen => is_fullscreen;
        public override IntPtr NativeDisplayHandle => native_handle;
        public override GraphicsModule Graphics => sdl_gpu_graphics;


        public override void Init(string title, int width, int height, GraphicsBackend graphics_backend)
        {
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
            
            Console.WriteLine($"Create window took: {sw.Elapsed.TotalSeconds}");

            window_id = SDL_GetWindowID(window);
            native_handle = GetWindowNativeHandle();
            
            sdl_gpu_graphics = new SDLGpuGraphicsModule(window_id, width, height);
            
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
                                OnWinResized?.Invoke();
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

        public override unsafe void GetWindowSize(out int w, out int h)
        {
            int _w = 0;
            int _h = 0;

            SDL.SDL_GetWindowSize(window, &_w, &_h);

            w = _w;
            h = _h;
        }

        public override void SetWindowSize(int w, int h)
        {
            if(is_fullscreen)
            {
                SetFullscreen(false);
            }

            SDL_SetWindowPosition(window, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED);
            SDL_SetWindowSize(window, w, h);
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
                if (SDL_SetWindowFullscreen(window, enabled ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP: 0) == 0)
                {
                    is_fullscreen = enabled;
                }
            }
        }
    }
}