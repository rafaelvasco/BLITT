
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameObjects
{
    public class Gradient
    {
        private Color top_left_col;
        private Color top_right_col;
        private Color bottom_left_col;
        private Color bottom_right_col;

        public ref Color TopLeftCol => ref top_left_col;
        public ref Color TopRightCol => ref top_right_col;
        public ref Color BottomLeftCol => ref bottom_left_col;
        public ref Color BottomRightCol => ref bottom_right_col;

        public void Set(Color top_left, Color top_right, Color bottom_left, Color bottom_right)
        {
            top_left_col = top_left;
            top_right_col = top_right;
            bottom_left_col = bottom_left;
            bottom_right_col = bottom_right;
        }

        public void SetVertical(Color top, Color bottom)
        {
            top_left_col = top;
            top_right_col = top;
            bottom_left_col = bottom;
            bottom_right_col = bottom;
        }

        public void SetHorizontal(Color left, Color right)
        {
            top_left_col = left;
            top_right_col = right;
            bottom_left_col = left;
            bottom_right_col = right;
        }
    }
}

