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

    public class BLITTGame : IDisposable
    {
        internal readonly BLITTCore Core;
        
        public Control Control { get; }
        public Content Content { get; } 
        public Clock Clock { get; }
        public static BLITTGame Instance { get; private set; }
        
        public bool Running { get; internal set; }

        public bool Fullscreen
        {
            get => full_screen;
            set
            {
                if (full_screen != value)
                {
                    full_screen = value;
                    Core.SetFullscreen(full_screen);
                }
            }
        }
        
        private bool error;
        private string error_msg;
        private bool full_screen;
        
        /* ========================================================================================================== */

        public BLITTGame(GameProps props = default)
        {
            Instance = this;
            
            props.Title = props.Title ?? "BLITT!";
            props.CanvasWidth = Calc.Max(64, props.CanvasWidth);
            props.CanvasHeight = Calc.Max(64, props.CanvasHeight);
            full_screen = props.Fullscreen;

            Console.WriteLine("BLITT IS STARTING");
            
            Console.WriteLine("INITIALIZING CORE");

            Core = new SDL_BLITTCore();
            
            Core.Init(props.Title, props.CanvasWidth, props.CanvasHeight, fullscreen: props.Fullscreen);
            
            Core.OnQuit += OnPlatformQuit;
            Core.OnWinResized += OnScreenResized;
            
            Control = new Control(Core);
            Content = new Content(Core, "Assets");
            Clock = new Clock();
            
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }
        
        public void Dispose()
        {
        }

        public void Start()
        {
            if (Running)
            {
                return;
            }
            
            Running = true;
            Core.ShowScreen(true);
            
            Tick();
        }

        public void Quit()
        {
            Running = false;
        }

        public void ShowCursor(bool show)
        {
            Core.ShowCursor(show);
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
                Core.PollEvents();

                Clock.Tick();
                Control.Update();

                while (Clock.TotalTime >= Clock.FrameDuration)
                {
                    Clock.TotalTime -= Clock.FrameDuration;
                }
            }
            
            Content.UnloadAll();
            Core.Quit();
            
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