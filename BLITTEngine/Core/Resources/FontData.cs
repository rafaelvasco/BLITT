using System;
using BLITTEngine.Core.Numerics;

namespace BLITTEngine.Core.Resources
{
    [Serializable]
    public class FontData
    {
        public string Id;
        
        public PixmapData FontSheet;
        public Rect[] GlyphRects;
        public float[] PreSpacings;
        public float[] PostSpacings;
    }
}