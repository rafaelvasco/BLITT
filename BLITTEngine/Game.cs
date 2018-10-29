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

        public readonly Clock Clock;

        public Scene CurrentScene { get; private set; }

        public bool Running { get; internal set; }

        public bool Fullscreen
        {
            get => full_screen;
            set
            {
                if (full_screen != value)
                {
                    full_screen = value;
                    Platform.SetFullscreen(full_screen);
                }
            }
        }

        private bool error;
        private string error_msg;
        private bool full_screen;

        /* ========================================================================================================== */

        public Game(GameProps props = default)
        {
            props.Title = props.Title ?? "BLITT!";
            props.CanvasWidth = Calc.Max(64, props.CanvasWidth);
            props.CanvasHeight = Calc.Max(64, props.CanvasHeight);
            full_screen = props.Fullscreen;

            Console.WriteLine(":: Blit Engine Start ::");

            var timer = Stopwatch.StartNew();

            Platform = new SDLGamePlatform();
            Platform.OnQuit += _OnPlatformQuit;
            Platform.OnWinResized += _OnScreenResized;

            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, fullscreen: props.Fullscreen);

            Content.Init(root_path: "Assets");

            Console.WriteLine($" > Platform Init took: {timer.Elapsed.TotalSeconds}");

            Canvas.Init(Platform.GetRenderSurfaceHandle(), props.CanvasWidth, props.CanvasHeight, 2048);

            Console.WriteLine($" > Graphics Init took: {timer.Elapsed.TotalSeconds}");

            Control.Init(Platform);

            Clock = new Clock();

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            Scene.Game = this;
        }

        public void Dispose()
        {
            Content.UnloadAll();
            Canvas.Terminate();
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

        private static void _OnScreenResized(int w, int h)
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

                CurrentScene.Draw();

                Canvas.Flip();
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