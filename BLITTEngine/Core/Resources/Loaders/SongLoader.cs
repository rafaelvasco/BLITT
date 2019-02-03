using System;
using System.IO;
using BLITTEngine.Core.Foundation;
using Microsoft.Win32.SafeHandles;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class SongLoader : BaseLoader
    {
        public override Resource Load(Stream file_stream)
        {
            SafeFileHandle safe_file_handle = ((FileStream) file_stream).SafeFileHandle;

            if (safe_file_handle == null) throw new Exception("Could not load Song Stream");

            IntPtr file_handle = safe_file_handle.DangerousGetHandle();

            var wave_stream = new WavStream();

            wave_stream.loadMem(file_handle, (uint) file_stream.Length);

            var song = new Song(wave_stream);

            return song;
        }
    }
}