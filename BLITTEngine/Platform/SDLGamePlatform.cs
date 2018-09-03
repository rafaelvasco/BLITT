using System;
using BLITTEngine.Foundation;

namespace BLITTEngine.Platform
{
    internal class SDLGamePlatform : GamePlatform
    {
        private SDL.SDL_Window window;
        private IntPtr native_handle;
        private bool is_fullscreen;

        public override bool IsFullscreen => is_fullscreen;
        public override IntPtr NativeDisplayHandle => native_handle;
        
        
        public override void Init(string title, int width, int height)
        {
            var initFlags = SDL.SDL_InitFlags.SDL_INIT_VIDEO
                            | SDL.SDL_InitFlags.SDL_INIT_EVENTS
                            | SDL.SDL_InitFlags.SDL_INIT_JOYSTICK;

            if (SDL.SDL_Init(initFlags) != 0)
            {
                SDL.SDL_Quit();
                throw new Exception(SDL.SDL_GetError());
            }
            
            var windowFlags = 
                SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN 
                | SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
    
            
            window = SDL.SDL_CreateWindow(
                title, 
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                width, height, windowFlags);
            
            if (window == IntPtr.Zero)
            {
                SDL.SDL_Quit();
                throw new Exception(SDL.SDL_GetError());
            }

            native_handle = GetWindowNativeHandle();
        }
        
        private unsafe IntPtr GetWindowNativeHandle()
        {
            SDL.SDL_SysWMinfo wmInfo;

            SDL.SDL_GetVersion(&wmInfo.version);

            SDL.SDL_GetWindowWMInfo(window, &wmInfo);

            if(wmInfo.subsystem == SDL.SDL_SYSWM_TYPE.SDL_SYSWM_WINDOWS)
            {
                return wmInfo.info.win.window;
            }
            else if (wmInfo.subsystem == SDL.SDL_SYSWM_TYPE.SDL_SYSWM_X11)
            {
                return wmInfo.info.x11.window;
            }
            else if (wmInfo.subsystem == SDL.SDL_SYSWM_TYPE.SDL_SYSWM_COCOA)
            {
                return wmInfo.info.cocoa.window;
            }

            return window;
        }


        public override void Quit()
        {
            SDL.SDL_Quit();
        }

        public override unsafe void PollEvents()
        {
            SDL.SDL_Event ev;

            while(SDL.SDL_PollEvent(&ev) == 1)
            {
                switch (ev.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        OnQuit?.Invoke();
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        OnKeyDown?.Invoke((int)ev.keyboard.keysym.sym, (int)ev.keyboard.keysym.scancode);
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        OnKeyUp?.Invoke((int)ev.keyboard.keysym.sym, (int)ev.keyboard.keysym.scancode);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        OnMouseMove?.Invoke(ev.motion.x, ev.motion.y);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        OnMouseButtonDown?.Invoke((int) ev.button.button);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        OnMouseButtonUp?.Invoke((int)ev.button.button);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        OnMouseScroll?.Invoke(ev.wheel.x, ev.wheel.y);
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEADDED:
                        OnJoyDeviceAdd?.Invoke(ev.jdevice.which);
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEREMOVED:
                        OnJoyDeviceRemove?.Invoke(ev.jdevice.which);
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONDOWN:
                        OnJoyButtonDown?.Invoke(ev.jdevice.which, ev.jbutton.button);
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONUP:
                        OnJoyButtonUp?.Invoke(ev.jdevice.which, ev.jbutton.button);
                        break;
                    case SDL.SDL_EventType.SDL_JOYAXISMOTION:
                        OnJoyAxisMove?.Invoke(ev.jdevice.which, ev.jaxis.axis, ev.jaxis.value / (float)short.MaxValue);
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        switch (ev.window.evt)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                                OnQuit?.Invoke();
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                                OnWinResized?.Invoke();
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                                OnWinMinimized?.Invoke();
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
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

            SDL.SDL_SetWindowPosition(window, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED);
            SDL.SDL_SetWindowSize(window, w, h);
        }

        public override void SetWindowTitle(string title)
        {
            SDL.SDL_SetWindowTitle(window, title);
        }

        public override void ShowWindowCursor(bool show)
        {
            SDL.SDL_ShowCursor(show ? 1 : 0);
        }

        public override void ShowWindow(bool show)
        {
            SDL.SDL_ShowWindow(window);
        }

        public override void SetFullscreen(bool enabled)
        {
            if (is_fullscreen != enabled)
            {
                if (SDL.SDL_SetWindowFullscreen(window, enabled ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP: 0) == 0)
                {
                    is_fullscreen = enabled;
                }
            }
        }
    }
}