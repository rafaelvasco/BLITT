using System;
using System.IO;
using BLITTEngine.Core.Foundation;
using Microsoft.Win32.SafeHandles;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class EffectLoader : BaseLoader
    {
        public override Resource Load(Stream file_stream)
        {
            SafeFileHandle safe_file_handle = ((FileStream) file_stream).SafeFileHandle;

            if (safe_file_handle == null) throw new Exception("Could not load Effect Stream");

            IntPtr file_handle = safe_file_handle.DangerousGetHandle();

            var wave = new Wav();

            wave.loadMem(file_handle, (uint) file_stream.Length);

            var effect = new Effect(wave);

            return effect;
        }
    }
}