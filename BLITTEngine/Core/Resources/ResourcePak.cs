using System;
using System.Collections.Generic;

namespace BLITTEngine.Core.Resources
{
    
    [Serializable]
    public class ResourcePak
    {
        public readonly Dictionary<string, PixmapData> Images;
        public readonly Dictionary<string, ShaderProgramData> Shaders;
        public readonly Dictionary<string, FontData> Fonts;
        public readonly Dictionary<string, SfxData> Sfx;
        public readonly Dictionary<string, SongData> Songs;
        public readonly Dictionary<string, TextFileData> TextFiles;

        public ResourcePak()
        {
            Images = new Dictionary<string, PixmapData>();
            Shaders = new Dictionary<string, ShaderProgramData>();
            Fonts = new Dictionary<string, FontData>();
            Sfx = new Dictionary<string, SfxData>();
            Songs = new Dictionary<string, SongData>();
            TextFiles = new Dictionary<string, TextFileData>();
        }
        
    }
}