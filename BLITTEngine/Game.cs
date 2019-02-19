using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Input;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Platform;
using BLITTEngine.Core.Resources;
using Utf8Json;

namespace BLITTEngine
{
    public class Game : IDisposable
    {
        public readonly Canvas Canvas;

        public readonly Clock Clock;

        public readonly ContentManager ContentManager;

        internal readonly GraphicsContext GraphicsContext;

        public readonly InputManager InputManager;

        internal readonly GamePlatform Platform;

        private bool error;
        
        private string error_msg;
        
        private bool full_screen;
        
        private int requested_screen_h;
        
        private int requested_screen_w;
        
        private bool screen_resize_requested;
        
        private bool toggle_fullscreen_requested;

        private readonly string[] paks_to_preload;

        /* ========================================================================================================== */

        public Game()
        {
            Instance = this;

            var props = _LoadGameProperties();

            var timer = Stopwatch.StartNew();

            Platform = new SDLGamePlatform();
            Platform.OnQuit += _OnPlatformQuit;
            Platform.OnWinResized += _OnScreenResized;
            
            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, props.Fullscreen);

            Console.WriteLine($" > Platform Init took: {timer.Elapsed.TotalSeconds}");

            Platform.GetScreenSize(out var screen_w, out var screen_h);

            GraphicsContext = new GraphicsContext(Platform.GetRenderSurfaceHandle(), screen_w, screen_h);

            ContentManager = new ContentManager();

            if (props.PreloadResourcePaks != null)
            {
                paks_to_preload = props.PreloadResourcePaks;
            }
            
            Platform.LoadContent();

            Console.WriteLine($" > Load Content took: {timer.Elapsed.TotalSeconds}");
            
            Canvas = new Canvas(GraphicsContext, props.CanvasWidth, props.CanvasHeight, 2048);

            Console.WriteLine($" > Canvas Load took: {timer.Elapsed.TotalSeconds}");
            
            
            InputManager = new InputManager(Platform);

            MediaPlayer.Init();

            Clock = new Clock
            {
                FrameRate = props.FrameRate
            };

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            /* CONVENIENCE REFERENCES FOR SCENES */
            Scene.Game = this;
            Scene.Content = ContentManager;
            Scene.Canvas = Canvas;
            Scene.Input = InputManager;
        }

        public static Game Instance { get; private set; }

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
                Platform.GetScreenSize(out var w, out var h);

                return new Size(w, h);
            }
            set
            {
                Platform.GetScreenSize(out var w, out var h);

                if (value.W == w && value.H == h) return;

                screen_resize_requested = true;

                requested_screen_w = value.W;
                requested_screen_h = value.H;
            }
        }

        public void Dispose()
        {
            CurrentScene.End();
            ContentManager.FreeEverything();
            GraphicsContext.Shutdown();
            MediaPlayer.Shutdown();
            Platform.Shutdown();
        }

        public void Start(Scene scene = null)
        {
            if (Running) return;
            
            if (paks_to_preload != null)
            {
                foreach (var pak in paks_to_preload)
                {   
                    ContentManager.LoadContentPack(pak);
                }
            }

            CurrentScene = scene ?? new EmptyScene();
            CurrentScene.Load();
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

        private static GameProperties _LoadGameProperties()
        {
            GameProperties props;
            
            try
            {
                var bytes = File.ReadAllBytes("Config.json");

                props = JsonSerializer.Deserialize<GameProperties>(bytes);

                if (props.Title == null)
                {
                    props.Title = "Game";
                }

                if (props.FrameRate == 0)
                {
                    props.FrameRate = 60;
                }

                if (props.CanvasWidth == 0)
                {
                    props.CanvasWidth = 800;
                }

                if (props.CanvasHeight == 0)
                {
                    props.CanvasHeight = 600;
                }

            }
            catch (FileNotFoundException)
            {
                props = GameProperties.Default();

                var bytes = JsonSerializer.Serialize(props);
                
                File.WriteAllBytes(AppContext.BaseDirectory, bytes);
            }

            return props;
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
            Clock.Start();

            while (Running)
            {
                Platform.PollEvents();

                Clock.Tick();
                InputManager.Update();

                while (Clock.TotalTime >= Clock.FrameDuration)
                {
                    Clock.TotalTime -= Clock.FrameDuration;
                    CurrentScene.Update(Clock.DeltaTime);
                }

                CurrentScene.Draw(Canvas);

                GraphicsContext.SwapBuffers();

                if (toggle_fullscreen_requested)
                {
                    toggle_fullscreen_requested = false;

                    Platform.SetFullscreen(full_screen);
                }
                else if (screen_resize_requested)
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