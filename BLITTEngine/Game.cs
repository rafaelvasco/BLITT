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
            
            Platform.Init("BLITT", 800, 600, fullscreen: true, GraphicsBackend.OpenGL);
            
            Platform.OnQuit += OnPlatformQuit;
            
            Platform.GetWindowSize(out int windowW, out int windowH);
            
            Screen.Init(windowW , windowH, Platform.IsFullscreen);
            Canvas.Init(Platform.Graphics);
            
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
                
                graphics.BeginDraw();
                
                CurrentScene?.Draw();
                
                graphics.EndDraw();

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