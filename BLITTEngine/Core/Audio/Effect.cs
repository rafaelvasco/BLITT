using System;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Audio
{
    public class Effect : Resource
    {
        internal IntPtr Handle;

        internal override void Dispose()
        {
            MediaPlayer.FreeEffect(this);  
        }
    }
}
