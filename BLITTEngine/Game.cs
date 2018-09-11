using System;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Platform;
using BLITTEngine.Resources;
using BLITTEngine.Temporal;

namespace BLITTEngine
{
    public struct GameProps
    {
        public string Title;
        public int ScreenWidth;
        public int ScreenHeight;
        public bool Fullscreen;
        public Scene StartingScene;

    }

    public static class Game
    {
        private static bool error;
        private static string error_msg;
        
        internal static GamePlatform Platform;
        
        public static bool Running { get; internal set; }
        public static bool Focused { get; internal set; } = true;
        public static bool ExitOnCloseWindow { get; set; } = true;
        public static Scene CurrentScene { get; private set; }

        public static void Run(GameProps props)
        {

            Console.WriteLine("BLITT is Starting...");
            
            if (Running)
            {
                return;
            }

            CurrentScene = props.StartingScene;

            Running = true;

            Console.WriteLine("Initializing GamePlatform...");

            Platform = new SDLGamePlatform();
            
            Platform.Init(props.Title, props.ScreenWidth, props.ScreenHeight, fullscreen: props.Fullscreen, GraphicsBackend.OpenGL);
            
            Platform.OnQuit += OnPlatformQuit;
            
            Screen.Init(Platform);
            Canvas.Init(Platform);
            Keyboard.Init(Platform);
            Content.Init("Assets");
            
            CurrentScene?.Init();
            
            Platform.ShowWindow(true);
            
            Tick();
        }

        public static void Quit()
        {
            Running = false;
        }

        internal static void ThrowError(string message, params object[] args)
        {
            error = true;

            error_msg = string.Format(message, args);
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

            var graphics = Platform.Graphics;
            
            while (Running)
            {
                Platform.PollEvents();

                if (!Focused)
                {
                    continue;
                }
                
                GameClock.Tick();

                while (GameClock.TotalTime >= GameClock.FrameDuration)
                {
                    Keyboard.Update();

                    CurrentScene?.Update(GameClock.DeltaTime);
                    GameClock.TotalTime -= GameClock.FrameDuration;
                    //Platform.SetWindowTitle(GameClock.FPS.ToString());
                }

                graphics.Clear();
                
                CurrentScene?.Draw();

                if (Screen.ScreenResized)
                {
                    Console.WriteLine("GAME SCREEN RESIZED");
                    Platform.SetWindowSize(Screen.Width, Screen.Height);
                    Screen.ScreenResized = false;
                }
                else if (Screen.ScreenToggledUpdate)
                {
                    Console.WriteLine("GAME SCREEN TOGGLED FS");

                    if (!Platform.IsFullscreen && Screen.Fullscreen)
                    {
                        Platform.SetFullscreen(true);
                    }
                    else
                    {
                        Platform.SetFullscreen(false);

                    }

                    Screen.ScreenToggledUpdate = false;
                }

                graphics.Flip();

                
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