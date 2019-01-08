using System;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameResources;

namespace BLITTEngine.DisplayObjects
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
        public Font Font
        {
            get => current_font;
            set
            {
                if (current_font != value)
                {
                    font_changed = true;

                    current_font = value;

                    if (calculate_boundaries) 
                    {
                        RecalculateMetrics();  
                    }

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

                if (calculate_boundaries && current_font != null)
                {
                    RecalculateMetrics();
                }
            }
        }

        public float Spacing
        {
            get => spacing;
            set
            {
                spacing = value;

                if (calculate_boundaries && current_font != null)
                {
                    RecalculateMetrics();
                }
            }
        }

        public float LineSpacing
        {
            get => line_spacing;
            set
            {
                line_spacing = value;

                if (calculate_boundaries && current_font != null)
                {
                    RecalculateMetrics();
                }
            }
        }

        public float Proportion
        {
            get => proportion;
            set
            {
                proportion = value;

                if (calculate_boundaries && current_font != null)
                {
                    RecalculateMetrics();
                }
            }
        }

        public float Width
        {
            get => text_width;
            set
            {
                if (!multi_line)
                {
                    return;
                }

                text_width = value;

            }
        }

        public float Height => text_height;

        private Font current_font;

        private char[] string_buffer;

        private int string_length;

        private float spacing = 1.0f;

        private float proportion = 1.0f;

        private float scale = 1.0f;

        private float line_spacing = 1.0f;

        private float base_line_height = 0.0f;

        private float text_width = 0.0f;

        private float text_height = 0.0f;

        private bool multi_line;

        private bool calculate_boundaries = false;

        private bool font_changed = true;

        private bool text_changed = true;

        public SpriteText(Font font, string text=null)
        {
            this.string_buffer = new char[256];
            this.string_length = 0;
            this.current_font = font;

            if (text != null)
            {
                SetText(text);
            }
        }

        public void SetText(string text)
        {
            text_changed = true;

            if (text.Length < 257)
            {
                multi_line = false;
                calculate_boundaries = true;

                for (int i = 0; i < text.Length; i++)
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

                for (int i = 0; i < text.Length; i++)
                {
                    string_buffer[i] = text[i];
                }
            }

            string_length = text.Length;

            if ((multi_line || calculate_boundaries) && current_font != null) 
            {
                RecalculateMetrics();  
            }

            text_changed = false;
        }

        public void Draw(Canvas canvas, float x, float y)
        {
            if (string_length == 0)
            {
                return;
            }

            float dx = x;
            float dy = y;

            var letters = current_font.letters;
            var pre_spacings = current_font.pre_spacings;
            var post_spacings = current_font.post_spacings;

            if (!multi_line)
            {
                for (int i = 0; i < string_length; ++i)
                {
                    int ch_idx = string_buffer[i];

                    if (letters[ch_idx] == null)
                    {
                        ch_idx = '?';
                    }

                    if (letters[ch_idx] != null)
                    {
                        dx += pre_spacings[ch_idx] * scale * proportion;
                        letters[ch_idx].DrawEx(canvas, dx, y, 0.0f, scale * proportion, scale);
                        dx += (letters[ch_idx].Width + post_spacings[ch_idx] + spacing) * scale * proportion;
                    }
                }
            }
            else
            {
                canvas.FillRect(x, y, text_width, text_height);

                for (int i = 0; i < string_length; ++i)
                {
                    char ch = string_buffer[i];

                    if (ch == '\n')
                    {
                        dy += (int) (base_line_height * scale) + line_spacing * scale;

                        dx = x;
                    }
                    else
                    {
                        int ch_idx = string_buffer[i];

                        if (letters[ch_idx] == null)
                        {
                            ch_idx = '?';
                        }

                        if (letters[ch_idx] != null)
                        {
                            dx += pre_spacings[ch_idx] * scale * proportion;
                            letters[ch_idx].DrawEx(canvas, dx, dy, 0.0f, scale * proportion, scale);
                            dx += (letters[ch_idx].Width + post_spacings[ch_idx] + spacing) * scale * proportion;
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

                // Find Base Line Height

                if (font_changed)
                {
                    for (int i = 0; i < letters.Length; ++i)
                    {
                        if (letters[i] == null)
                        {
                            continue;
                        }

                        if (letters[i].Height > base_line_height)
                        {
                            base_line_height = letters[i].Height;
                        }
                    }
                }

                if (text_changed)   
                {
                    // Find Text Area Width and Height

                    text_height = base_line_height;

                    float max_width = 0;
                    float cur_width = 0;

                    for (int i = 0; i < string_length; i++)
                    {
                        int ch_idx = string_buffer[i];

                        if (letters[ch_idx] == null)
                        {
                            ch_idx = '?';
                        }

                        if (string_buffer[i] != '\n')
                        {
                            cur_width += letters[ch_idx].Width;

                            if (cur_width > max_width)
                            {
                                max_width = cur_width;
                            }
                        }
                        else
                        {
                            text_height += base_line_height * scale + line_spacing * scale;
                            cur_width = 0;
                        }
                    }

                    text_width = max_width;
                }
            }
        }
    }
}
