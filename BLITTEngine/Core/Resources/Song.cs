using BLITTEngine.Core.Audio;
using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Resources
{
    public class Song : Resource
    {
        public bool IsPlaying => MediaPlayer.IsPlaying(this);

        public bool IsPaused => MediaPlayer.IsPaused(this);

        internal readonly WavStream song_stream;

        internal Song(WavStream stream)
        {
            song_stream = stream;
        }

        internal override void Dispose()
        {
        }
    }
}