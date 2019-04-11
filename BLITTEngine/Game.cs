using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading;
using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Platform;
using BLITTEngine.Core.Resources;
using Utf8Json;

namespace BLITTEngine
{
    public class Game : IDisposable
    {
        public string Title { get; }
             
        public readonly Canvas Canvas;

        public bool IsActive => Platform.IsActive;

        public TimeSpan InactiveSleepTime
        {
            get => _inactiveSleepTime;
            set
            {
                if (value < TimeSpan.Zero)
                {
                    value = TimeSpan.FromSeconds(0.02);
                }

                _inactiveSleepTime = value;
            }
        }

        public TimeSpan MaxElapsedTime
        {
            get => _maxElapsedTime;
            set
            {
                if (value < TimeSpan.Zero)
                {
                    value = TimeSpan.FromMilliseconds(500);
                }

                if (value < _targetElapsedTime)
                {
                    value = _targetElapsedTime;
                }

                _maxElapsedTime = value;
            }
        }

        public TimeSpan TargetElapsedTime
        {
            get => _targetElapsedTime;
            set
            {
                if (value <= TimeSpan.Zero)
                {
                    value = TimeSpan.FromTicks(166667);
                }

                _targetElapsedTime = value;
            }
        }

        public bool IsFixedTimeStep
        {
            get => _isFixedTimestep;
            set => _isFixedTimestep = value;
        }
        
        public readonly ContentManager ContentManager;

        internal readonly GraphicsContext GraphicsContext;

        internal readonly GamePlatform Platform;

        private bool full_screen;
        
        private int requested_screen_h;
        
        private int requested_screen_w;
        
        private bool screen_resize_requested;
        
        private bool toggle_fullscreen_requested;

        private readonly string[] paks_to_preload;

        private bool _isFixedTimestep = true;
        
        private TimeSpan _targetElapsedTime = TimeSpan.FromTicks(166667);

        private TimeSpan _inactiveSleepTime = TimeSpan.FromSeconds(0.02);
        
        private TimeSpan _maxElapsedTime = TimeSpan.FromMilliseconds(500);

        private TimeSpan _accumulatedElapsedTime;

        private readonly GameTime _gameTime = new GameTime();

        private Stopwatch _gameTimer;

        private long _previousTicks;

        private int _updateFrameLag;
        

        /* ========================================================================================================== */

        public Game()
        {
            Instance = this;

            var props = _LoadGameProperties();

            var timer = Stopwatch.StartNew();

            Platform = new SDLGamePlatform();
            Platform.OnQuit += _OnPlatformQuit;
            Platform.OnWinResized += _OnScreenResized;

            Title = props.Title;
            
            Platform.Init(props.Title, props.CanvasWidth, props.CanvasHeight, props.Fullscreen);

            Console.WriteLine($" > Platform Init took: {timer.Elapsed.TotalSeconds.ToString()}");

            Platform.GetScreenSize(out var screen_w, out var screen_h);

            GraphicsContext = new GraphicsContext(Platform.GetRenderSurfaceHandle(), screen_w, screen_h);

            ContentManager = new ContentManager();

            if (props.PreloadResourcePaks != null)
            {
                paks_to_preload = props.PreloadResourcePaks;
            }
            
            Platform.LoadContent();

            Console.WriteLine($" > Load Content took: {timer.Elapsed.TotalSeconds.ToString()}");
            
            Canvas = new Canvas(GraphicsContext, props.CanvasWidth, props.CanvasHeight, 2048);

            Console.WriteLine($" > Canvas Load took: {timer.Elapsed.TotalSeconds.ToString()}");
            
            Input.Init(Platform);
            
            MediaPlayer.Init();

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            /* CONVENIENCE REFERENCES FOR SCENES */
            Scene.Game = this;
            Scene.Content = ContentManager;
            Scene.Canvas = Canvas;
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
            CurrentScene.Unload();
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
            CurrentScene.Update(_gameTime);

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
            StringBuilder error = new StringBuilder();

            string msg = string.Format(message, args);
            
            error.AppendLine("::::::: BLITT Engine ERROR Log ::::::: ");

            error.Append("Message: ");

            error.AppendLine(msg);

            var destination_path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "BLITTEngine", Title, "ERRORLOG.txt");
            
            var file = new FileInfo(destination_path);

            file.Directory?.Create();
            
            File.WriteAllText(file.FullName, error.ToString());
            
            throw new Exception(msg);
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
            
            _gameTimer = Stopwatch.StartNew();

            while (Running)
            {

                RetryTick:
                
                if (!IsActive && _inactiveSleepTime.TotalMilliseconds > 1.0)
                {
                    Thread.Sleep((int) _inactiveSleepTime.TotalMilliseconds);
                }

                var currentTicks = _gameTimer.Elapsed.Ticks;

                _accumulatedElapsedTime += TimeSpan.FromTicks(currentTicks - _previousTicks);

                if (_accumulatedElapsedTime > _maxElapsedTime)
                {
                    _accumulatedElapsedTime = _maxElapsedTime;
                }
                
                _previousTicks = currentTicks;

                if (_isFixedTimestep && _accumulatedElapsedTime < _targetElapsedTime)
                {
                    goto RetryTick;
                }

                if (_isFixedTimestep)
                {
                    _gameTime.ElapsedGameTime = _targetElapsedTime;

                    var stepCount = 0;
                    
                    while (_accumulatedElapsedTime >= _targetElapsedTime && Running)
                    {
                        _gameTime.TotalGameTime += _targetElapsedTime;
                        _accumulatedElapsedTime -= _targetElapsedTime;
                        ++stepCount;
                        
                        Platform.PollEvents();
                        
                        Input.Update();
                        
                        CurrentScene.Update(_gameTime);
                        
                        Input.PostUpdate();

                    }

                    _updateFrameLag += Calc.Max(0, stepCount - 1);

                    if (_gameTime.IsRunningSlowly)
                    {
                        if (_updateFrameLag == 0)
                        {
                            _gameTime.IsRunningSlowly = false;
                        }
                    }
                    else if (_updateFrameLag >= 5)
                    {
                        _gameTime.IsRunningSlowly = true;
                    }

                    if (stepCount == 1 && _updateFrameLag > 0)
                    {
                        _updateFrameLag--;
                    }

                    _gameTime.ElapsedGameTime = TimeSpan.FromTicks(_targetElapsedTime.Ticks * stepCount);
                }
                else
                {
                    _gameTime.ElapsedGameTime = _accumulatedElapsedTime;
                    _gameTime.TotalGameTime += _accumulatedElapsedTime;
                    _accumulatedElapsedTime = TimeSpan.Zero;
                    
                    Platform.PollEvents();
                    
                    Input.Update();
                        
                    CurrentScene.Update(_gameTime);
                        
                    Input.PostUpdate();
                }
                
                CurrentScene.Draw(Canvas, _gameTime);
                
                Canvas.EndRender();
                
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
                $"Gen-0: {gen0.ToString()} | Gen-1: {gen1.ToString()} | Gen-2: {gen2.ToString()}"
            );
#endif
        }
    }
}