using System;
using System.Diagnostics;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Platform;
using BLITTEngine.Resources;
using BLITTEngine.Temporal;

namespace BLITTEngine
{
    public static class Game
    {
        private static bool error = false;
        private static string error_msg = null;
        
        internal static GamePlatform Platform;
        
        public static bool Running { get; internal set; }
        public static bool Focused { get; internal set; } = true;
        public static bool ExitOnCloseWindow { get; set; } = true;
        public static Scene CurrentScene { get; private set; }

        public static void Run(Scene scene = null)
        {
            
            if (Running)
            {
                return;
            }

            CurrentScene = scene;

            Running = true;
            
            Platform = new SDLGamePlatform();
            
            Platform.Init("BLITT", 800, 600, GraphicsBackend.OpenGL);
            
            Platform.OnQuit += OnPlatformQuit;
            
            Platform.GetWindowSize(out int windowW, out int windowH);
            
            Screen.Init(windowW , windowH);
            Canvas.Init(Platform.Graphics);
            
            Keyboard.Init();
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
                    CurrentScene?.Update(GameClock.DeltaTime);
                    GameClock.TotalTime -= GameClock.FrameDuration;
                    //Platform.SetWindowTitle(GameClock.FPS.ToString());
                }
                
                Keyboard.PostUpdate();
                
                graphics.BeginDraw();
                
                CurrentScene?.Draw();
                
                graphics.EndDraw();

                if (Screen.NeedsUpdate)
                {
                    Console.WriteLine("UPDATE SCREEN");
                    
                    if (!Platform.IsFullscreen && Screen.Fullscreen)
                    {
                        Console.WriteLine("GO FULLSCREEN");
                        Platform.SetFullscreen(true);
                    }
                    else
                    {
                        Platform.SetWindowSize(Screen.Width, Screen.Height);
                    }

                    Screen.NeedsUpdate = false;
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