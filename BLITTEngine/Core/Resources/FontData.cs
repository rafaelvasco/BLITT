using System;
using BLITTEngine.Core.Common;

namespace BLITTEngine.Core.Resources
{
    [Serializable]
    public class FontData
    {
        public string Id;
        
        public PixmapData FontSheet;
        public RectF[] GlyphRects;
        public float[] PreSpacings;
        public float[] PostSpacings;
    }
}