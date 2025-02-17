﻿using BLITTEngine.GameToolkit;

namespace BLITTEngine.Core.Resources
{
    public class Font : Resource
    {
        public Texture2D Texture => font_tex;

        internal const string Header_Tag = "[BTFONT]";
        internal const string Bmp_Tag = "Bitmap=";
        internal const string Char_Tag = "Char=";

        internal readonly Texture2D font_tex;
        internal readonly Sprite[] letters;
        internal readonly float[] pre_spacings;
        internal readonly float[] post_spacings;

        internal Font(Texture2D tex, Sprite[] glyphs, float[] pre, float[] post)
        {
            this.font_tex = tex;
            this.letters = glyphs;
            this.pre_spacings = pre;
            this.post_spacings = post;
        }

        internal override void Dispose()
        {
            this.font_tex.Dispose();
        }
    }
}