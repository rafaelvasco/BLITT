using System;
using System.Diagnostics;

namespace BLITTEngine.Temporal
{
    public class Clock
    {
        public double FrameRate
        {
            get => 1 / FrameDuration;
            set => FrameDuration = 1 / value;
        }

        public int FPS => fps;
        
        internal double TotalTime;

        internal double FrameDuration;
        
        public float DeltaTime { get; private set; }
        
        public bool Paused { get; internal set; }
        
        private Stopwatch timer;

        private TimeSpan delta_timespan;

        private TimeSpan total_timespan;

        private int fps;

        private float fps_timer;

        internal Clock()
        {
            TotalTime = 0;
            DeltaTime = 0;
            Paused = false;
            
        }

        internal void Start()
        {
            timer = Stopwatch.StartNew();
        }

        internal void Stop()
        {
            timer.Stop();
        }

        internal void Resume()
        {
            Paused = false;
        }
        
        internal void Pause()
        {
            Paused = true;
        }
        
        internal void Tick()
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