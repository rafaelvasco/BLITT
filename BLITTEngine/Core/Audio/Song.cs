using System;
using BLITTEngine.Core.Numerics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Audio
{
    public class Song : Resource
    {
        internal IntPtr Handle;

        public int FadeMs
        {
            get => fade_ms;
            set
            {
                fade_ms = value;

                if (fade_ms < 0)
                {
                    fade_ms = 0;
                }
            }
        }

        public float VolumeFactor
        {
            get => volume_factor;

            set
            {
                volume_factor = value;

                volume_factor = Calc.Normalize(volume_factor);

                if (Playing)
                {
                    MediaPlayer.SyncSongVolume();
                }
            }
        }

        public bool Playing { get; internal set; }

        private int fade_ms;

        private float volume_factor = 1.0f;

        internal override void Dispose()
        {
            MediaPlayer.FreeSong(this);
        }
    }
}
