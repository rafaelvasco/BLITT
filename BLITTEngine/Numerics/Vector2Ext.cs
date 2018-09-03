using System.Numerics;

namespace BLITTEngine.Numerics
{
    public static class Vector2Ext
    {
        public static readonly Vector2 Right = new Vector2(1f, 0f);
        public static readonly Vector2 Left = new Vector2(-1f, 0f);
        public static readonly Vector2 Up = new Vector2(0f, -1f);
        public static readonly Vector2 Down = new Vector2(0f, 1f);

        public static Vector2 Normalized(this Vector2 vec)
        {
            return Normalize(vec);
        }
        
        public static Vector2 Normalize(Vector2 v)
        {
            float len = v.Length();
            
            if (len > 0f)
                return new Vector2(v.X / len, v.Y / len);
            return v;
        }
    }
}