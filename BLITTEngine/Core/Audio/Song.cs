using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Audio
{
    public class Song : Resource
    {
        public bool IsPlaying => MediaPlayer.IsPlaying(this);

        public bool IsPaused => MediaPlayer.IsPaused(this);

        internal WavStream song_stream;

        internal Song(WavStream stream)
        {
            song_stream = stream;
        }
        
        internal override void Dispose()
        {
        }
    }
}
