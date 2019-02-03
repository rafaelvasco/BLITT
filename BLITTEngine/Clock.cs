using System;
using System.Diagnostics;

namespace BLITTEngine
{
    public class Clock
    {
        private TimeSpan delta_timespan;

        private float fps_timer;

        internal double FrameDuration;

        private Stopwatch timer;

        private TimeSpan total_timespan;

        internal double TotalTime;

        internal Clock()
        {
            TotalTime = 0;
            DeltaTime = 0;
            Paused = false;
        }

        public double FrameRate
        {
            get => 1 / FrameDuration;
            set => FrameDuration = 1 / value;
        }

        public int FPS { get; private set; }

        public float DeltaTime { get; private set; }

        public bool Paused { get; internal set; }

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
                    FPS = (int) (1 / DeltaTime);
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