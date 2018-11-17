using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Platform;
using BLITTEngine.Input;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using BLITTEngine.Temporal;
using System;
using System.Diagnostics;
using System.Runtime;

namespace BLITTEngine
{
    public struct GameProps
    {
        public string Title;
        public int CanvasWidth;
        public int CanvasHeight;
        public bool Fullscreen;
    }

    public class Game : IDisposable
    {
        internal readonly GamePlatform Platform;

        internal readonly GraphicsContext GraphicsContext;

        public readonly Clock Clock;

        public readonly Canvas Canvas;

        public Scene CurrentScene { get; private set; }

        public bool Running { get; internal set; }

        public bool Fullscreen
        {
            get => full_screen;
            set
            {
                if (full_screen == value) return;

                full_screen = value;

                toggle_fullscreen_requested = true;
            }
        }

        public Size ScreenSize
        {
            get
            {
                Platform.GetScreenSize(out int w, out int h);

                return new Size(w, h);
            }
            set
            {
                Platform.GetScreenSize(out int w, out int h);

                if (value.W == w && value.H == h) return;

                screen_resize_requested = true;

                requested_screen_w = value.W;
                requested_screen_h = value.H;
            }
        }

        private bool error;
        private string error_msg;
        private bool full_screen;
        private bool toggle_fullscreen_requested;
        private bool screen_resize_requested;
        private int requested_screen_w;
        private int requested_screen_h;

        /* ========================================================================================================== */

        public Game(GameProps props = default)
        {
            props.Title = props.Title ?? "BLITT!";
            props.CanvasWidth = Calc.Max(64, props.CanvasWidth);
            props.CanvasHeight = Calc.Max(64, props.CanvasHeight);
            full_screen = props.Fullscreen;

            Console.WriteLine(":: Blit Engine Start ::");

            Stopwatch timer = Stopwatch.StartNew();

            Platform = new SDLGamePlatform();
            Platform.OnQuit += _OnPlatformQuit;
            Platform.OnWinResized += _OnScreenResized;

            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, props.Fullscreen);

            Content.Init(root_path:"Assets");

            Console.WriteLine($" > Platform Init took: {timer.Elapsed.TotalSeconds}");

            Platform.GetScreenSize(out int screen_w, out int screen_h);

            GraphicsContext = new GraphicsContext(Platform.GetRenderSurfaceHandle(), screen_w, screen_h);

            Console.WriteLine($" > Graphics Init took: {timer.Elapsed.TotalSeconds}");

            Canvas = new Canvas(GraphicsContext, props.CanvasWidth, props.CanvasHeight, 2048);;

            Control.Init(Platform);

            Clock = new Clock();

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            Scene.Game = this;
            Scene.Canvas = Canvas;
        }

        public void Dispose()
        {
            Content.FreeEverything();;
            GraphicsContext.Shutdown();
            Platform.Quit();
        }

        public void Start(Scene scene = null)
        {
            if (Running)
            {
                return;
            }

            CurrentScene = scene ?? new EmptyScene();
            CurrentScene.Init();

            Running = true;
            
            Platform.ShowScreen(true);
            GraphicsContext.SwapBuffers();

            _Tick();
        }

        public void Quit()
        {
            Running = false;
        }

        public void ShowCursor(bool show)
        {
            Platform.ShowCursor(show);
        }

        public void ToggleFullscreen()
        {
            Fullscreen = !Fullscreen;
        }

        internal void ThrowError(string message, params object[] args)
        {
            error = true;

            error_msg = string.Format(message, args);
        }

        private void _OnScreenResized(int w, int h)
        {
            Canvas.OnScreenResized(w, h);
        }

        private void _OnPlatformQuit()
        {
            Quit();
        }

        private void _Tick()
        {
            Clock.FrameRate = 60;
            Clock.Start();

            while (Running)
            {
                Platform.PollEvents();

                Clock.Tick();
                Control.Update();

                while (Clock.TotalTime >= Clock.FrameDuration)
                {
                    Clock.TotalTime -= Clock.FrameDuration;
                    CurrentScene.Update(Clock.DeltaTime);
                }

                CurrentScene.Draw(Canvas);

                GraphicsContext.SwapBuffers();

                if(toggle_fullscreen_requested)
                {
                    toggle_fullscreen_requested = false;

                    Platform.SetFullscreen(full_screen);
                }
                else if(screen_resize_requested)
                {
                    screen_resize_requested = false;

                    Platform.SetScreenSize(requested_screen_w, requested_screen_h);
                }
            }

#if DEBUG

            var gen0 = GC.CollectionCount(0);
            var gen1 = GC.CollectionCount(1);
            var gen2 = GC.CollectionCount(2);

            Console.WriteLine(
                $"Gen-0: {gen0} | Gen-1: {gen1} | Gen-2: {gen2}"
            );
#endif
        }
    }
}