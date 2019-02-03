using BLITTEngine.Core.Foundation;

namespace BLITTEngine.Core.Resources
{
    public class Effect : Resource
    {
        internal readonly Wav wave;

        internal Effect(Wav wav)
        {
            this.wave = wav;
        }

        internal override void Dispose()
        {
        }
    }
}