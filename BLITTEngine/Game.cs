using System;
using BLITTEngine.Graphics;
using BLITTEngine.Input;
using BLITTEngine.Platform;
using BLITTEngine.Temporal;

namespace BLITTEngine
{
    public static class Game
    {
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
            
            Platform.Init("BLITT", 800, 600);
            
            Platform.OnQuit += OnPlatformQuit;
            
            Platform.PollEvents();
            
            Platform.GetWindowSize(out int windowW, out int windowH);
            
            GraphicsDevice.Init(windowW, windowH, Platform.NativeDisplayHandle);
            
            Keyboard.Init();
            
            Platform.ShowWindow(true);
            
            Tick();
        }

        public static void Quit()
        {
            Running = false;
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

                if (!Focused)
                {
                    continue;
                }
                
                GameClock.Tick();

                while (GameClock.TotalTime >= GameClock.FrameDuration)
                {
                    CurrentScene?.Update(GameClock.DeltaTime);
                    
                    Keyboard.PostUpdate();
                    
                    GameClock.TotalTime -= GameClock.FrameDuration;
                }
                
                CurrentScene?.Draw();

#if DEBUG
                GraphicsDevice.DrawDebugInfo();
#endif
                GraphicsDevice.Flip();
                
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
            
            GraphicsDevice.Shutdown();
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