using System;
using System.Diagnostics;

namespace BLITTEngine.Temporal
{
    public static class GameClock
    {
        public static double FrameRate
        {
            get => 1 / FrameDuration;
            set => FrameDuration = 1 / value;
        }

        public static int FPS => fps;
        
        internal static double TotalTime;

        internal static double FrameDuration;
        
        public static float DeltaTime { get; private set; }
        
        public static bool Paused { get; internal set; }
        
        private static Stopwatch timer;

        private static TimeSpan delta_timespan;

        private static TimeSpan total_timespan;

        private static int fps;

        private static float fps_timer;


        internal static void Start()
        {
            TotalTime = 0;
            DeltaTime = 0;
            Paused = false;
            timer = Stopwatch.StartNew();

            bool b = Stopwatch.IsHighResolution;
        }

        internal static void Stop()
        {
            timer.Stop();
        }

        internal static void Resume()
        {
            Paused = false;
        }
        
        internal static void Pause()
        {
            Paused = true;
        }
        
        internal static void Tick()
        {
            if (!Paused)
            {
                delta_timespan = timer.Elapsed - total_timespan;
                total_timespan = timer.Elapsed;

                TotalTime += delta_timespan.TotalSeconds;

                DeltaTime = (float) delta_timespan.TotalSeconds;

                fps_timer += DeltaTime;

                if (fps_timer > 0.2)
                {
                    fps = (int)(1 / DeltaTime);
                    fps_timer = 0;
                }
            }
            else
            {
                total_timespan = timer.Elapsed;
            }
        }
        
    }
}