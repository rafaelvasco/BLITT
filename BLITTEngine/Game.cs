using System;
using System.Runtime;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Numerics;
using BLITTEngine.Platform;
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

    public static class Game
    {
        private static bool error;
        private static string error_msg;
        private static bool full_screen;

        internal static GamePlatform Platform;
        
        public static bool Running { get; internal set; }
        public static bool ExitOnCloseWindow { get; set; } = true;
        public static Scene CurrentScene { get; private set; }

        private static bool initialized;

        public static bool Fullscreen
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

        public static void Init(GameProps props = default)
        {
            props.Title = props.Title ?? "BLITT Game";
            props.CanvasWidth = Calc.Max(64, props.CanvasWidth);
            props.CanvasHeight = Calc.Max(64, props.CanvasHeight);
            full_screen = props.Fullscreen;

            Console.WriteLine("BLITT is Starting...");
            
            Console.WriteLine("Initializing GamePlatform...");

            Platform = new SDLGamePlatform();
            
            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, fullscreen: props.Fullscreen, GraphicsBackend.OpenGL);
            
            Platform.OnQuit += OnPlatformQuit;
            Platform.OnWinResized += OnScreenResized;
            
            Canvas.Init(Platform, props.CanvasWidth, props.CanvasHeight);
            Control.Init(Platform);
            Content.Init("Assets");

            initialized = true;
            
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        public static void Run(Scene scene = null)
        {
            if (initialized)
            {
                if (Running)
                {
                    return;
                }

                CurrentScene = scene;

                Running = true;
            
                CurrentScene?.Init();
            
                Canvas.Begin();
            
                CurrentScene?.Draw();
            
                Canvas.End();
            
                Platform.ShowScreen(true);
            
                Tick();
            }
        }

        public static void Quit()
        {
            Running = false;
        }

        public static void ShowCursor(bool show)
        {
            Platform.ShowCursor(show);
        }

        public static void ToggleFullscreen()
        {
            Fullscreen = !Fullscreen;
        }

        internal static void ThrowError(string message, params object[] args)
        {
            error = true;

            error_msg = string.Format(message, args);
        }

        private static void OnScreenResized(int w, int h)
        {
            Canvas.OnScreenResized(w, h);
        }
        
        private static void OnPlatformQuit()
        {
            if (ExitOnCloseWindow)
            {
                Quit();
            }
        }

        private static void Tick()
        {
            GameClock.FrameRate = 60;
            GameClock.Start();

            while (Running)
            {
                Platform.PollEvents();

                GameClock.Tick();
                Control.Update();

                while (GameClock.TotalTime >= GameClock.FrameDuration)
                {
                    CurrentScene?.Update(GameClock.DeltaTime);
                    GameClock.TotalTime -= GameClock.FrameDuration;
                }
                
                Canvas.Begin();
                
                CurrentScene?.Draw();
           
                Canvas.End();

                if(Canvas.SizeChanged)
                {
                    Canvas.SizeChanged = false;

                    if(!Fullscreen)
                    {
                        Platform.SetScreenSize(Canvas.Width, Canvas.Height);
                    }
                }

                
            }
            
            Content.UnloadAll();
            Platform.Quit();
            
            
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