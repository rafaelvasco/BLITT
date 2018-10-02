using System;
using System.Runtime;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Platform;
using BLITTEngine.Draw;
using BLITTEngine.Input;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using BLITTEngine.Temporal;

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
        public static Game Instance { get; private set; }

        internal readonly GamePlatform Platform;
        internal readonly GraphicsDevice GraphicsDevice;

        public readonly Control Control;
        public readonly Content Content;
        public readonly Clock Clock;
        public readonly Canvas Canvas;

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
            Instance = this;

            props.Title = props.Title ?? "BLITT!";
            props.CanvasWidth = Calc.Max(64, props.CanvasWidth);
            props.CanvasHeight = Calc.Max(64, props.CanvasHeight);
            full_screen = props.Fullscreen;

            Console.WriteLine("BLITT IS STARTING");

            Console.WriteLine("INITIALIZING CORE");

            Platform = new SDLGamePlatform();

            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, fullscreen: props.Fullscreen);

            GraphicsDevice = new GraphicsDevice(Platform.GetRenderSurfaceHandle(), props.CanvasWidth, props.CanvasHeight);

            Platform.OnQuit += OnPlatformQuit;
            Platform.OnWinResized += OnScreenResized;

            Canvas = new Canvas(GraphicsDevice);
            Control = new Control(Platform);
            Content = new Content("Assets");
            Clock = new Clock();

            Scene.Content = Content;

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        public void Dispose()
        {
            Content.UnloadAll();
            GraphicsDevice.Dispose();
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

            Tick();
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

        private static void OnScreenResized(int w, int h)
        {
        }

        private void OnPlatformQuit()
        {
            Quit();
        }

        private void Tick()
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

                GraphicsDevice.Begin();

                CurrentScene.Draw(Canvas);

                GraphicsDevice.End();
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