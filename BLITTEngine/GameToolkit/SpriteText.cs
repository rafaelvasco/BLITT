using System;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public enum TextAlign
    {
        Left,
        Right,
        Center,
        Top,
        Bottom,
        Middle
    }

    public class SpriteText
    {
        private float base_line_height;

        private bool calculate_boundaries;

        private Font current_font;

        private bool font_changed = true;

        private float line_spacing = 1.0f;

        private bool multi_line;

        private float proportion = 1.0f;

        private float scale = 1.0f;

        private float letter_spacing = 1.0f;

        private char[] string_buffer;

        private int string_length;

        private bool text_changed = true;

        private float text_width;

        public SpriteText(Font font, string text = null)
        {
            string_buffer = new char[256];
            string_length = 0;
            current_font = font;

            if (text != null) SetText(text);
        }

        public Font Font
        {
            get => current_font;
            set
            {
                if (current_font != value)
                {
                    font_changed = true;

                    current_font = value;

                    if (calculate_boundaries) RecalculateMetrics();

                    font_changed = false;
                }
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;

                if (calculate_boundaries && current_font != null) RecalculateMetrics();
            }
        }

        public float LetterSpacing
        {
            get => letter_spacing;
            set
            {
                letter_spacing = value;

                if (calculate_boundaries && current_font != null) RecalculateMetrics();
            }
        }

        public float LineSpacing
        {
            get => line_spacing;
            set
            {
                line_spacing = value;

                if (calculate_boundaries && current_font != null) RecalculateMetrics();
            }
        }

        public float Proportion
        {
            get => proportion;
            set
            {
                proportion = value;

                if (calculate_boundaries && current_font != null) RecalculateMetrics();
            }
        }

        public float Width
        {
            get => text_width;
            set
            {
                if (!multi_line) return;

                text_width = value;
            }
        }

        public float Height { get; private set; }

        public void SetText(string text)
        {
            text_changed = true;

            if (text.Length < 257)
            {
                multi_line = false;
                calculate_boundaries = true;

                for (var i = 0; i < text.Length; i++)
                {
                    if (text[i] == '\n')
                    {
                        multi_line = true;
                        calculate_boundaries = true;
                    }

                    string_buffer[i] = text[i];
                }
            }
            else
            {
                Array.Resize(ref string_buffer, text.Length);

                for (var i = 0; i < text.Length; i++) string_buffer[i] = text[i];
            }

            string_length = text.Length;

            if ((multi_line || calculate_boundaries) && current_font != null) RecalculateMetrics();

            text_changed = false;
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            if (string_length == 0) return;

            var dx = x;
            var dy = y;

            var letters = current_font.letters;
            var pre_spacings = current_font.pre_spacings;
            var post_spacings = current_font.post_spacings;

            if (!multi_line)
            {
                for (var i = 0; i < string_length; ++i)
                {
                    int ch_idx = string_buffer[i];

                    if (letters[ch_idx] == null) ch_idx = '?';

                    if (letters[ch_idx] != null)
                    {
                        dx += pre_spacings[ch_idx] * scale * proportion;
                        letters[ch_idx].DrawEx(canvas, dx, y, 0.0f, scale * proportion, scale);
                        dx += (letters[ch_idx].Width + post_spacings[ch_idx] + letter_spacing) * scale * proportion;
                    }
                }
            }
            else
            {
                canvas.DrawRect(x, y, text_width, Height);

                for (var i = 0; i < string_length; ++i)
                {
                    var ch = string_buffer[i];

                    if (ch == '\n')
                    {
                        dy += (int) (base_line_height * scale) + line_spacing * scale;

                        dx = x;
                    }
                    else
                    {
                        int ch_idx = string_buffer[i];

                        if (letters[ch_idx] == null) ch_idx = '?';

                        if (letters[ch_idx] != null)
                        {
                            dx += pre_spacings[ch_idx] * scale * proportion;
                            letters[ch_idx].DrawEx(canvas, dx, dy, 0.0f, scale * proportion, scale);
                            dx += (letters[ch_idx].Width + post_spacings[ch_idx] + letter_spacing) * scale * proportion;
                        }
                    }
                }
            }
        }

        private void RecalculateMetrics()
        {
            if (multi_line)
            {
                var letters = current_font.letters;
                var pre_spacings = current_font.pre_spacings;
                var post_spacings = current_font.post_spacings;

                // Find Base Line Height

                if (font_changed)
                    for (var i = 0; i < letters.Length; ++i)
                    {
                        if (letters[i] == null) continue;

                        if (letters[i].Height > base_line_height) base_line_height = letters[i].Height;
                    }

                if (text_changed)
                {
                    // Find Text Area Width and Height

                    float max_width = 0;
                    float cur_width = 0;

                    var line = 1;

                    for (var i = 0; i < string_length; i++)
                    {
                        int ch_idx = string_buffer[i];

                        if (letters[ch_idx] == null) ch_idx = '?';

                        if (string_buffer[i] != '\n')
                        {
                            cur_width += letters[ch_idx].Width + letter_spacing;
                            Height = letters[ch_idx].Height * line;


                            if (i > 0) cur_width += pre_spacings[ch_idx];

                            if (i < string_length - 1) cur_width += post_spacings[ch_idx];

                            if (cur_width > max_width) max_width = cur_width;
                        }
                        else
                        {
                            line++;
                            cur_width = 0;
                        }
                    }

                    text_width = max_width;
                }
            }
        }
    }
}