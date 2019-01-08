using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Audio
{
    public class Effect : Resource
    {
        internal Wav wave;

        internal Effect(Wav wav)
        {
            this.wave = wav;
        }

        internal override void Dispose()
        {
        }
    }
}
