using System.Numerics;

namespace BLITTEngine.Numerics
{
    public static class Matrix3x2Ext
    {
        public static Vector2 TransformPoint(this Matrix3x2 m, Vector2 p)
        {
            return new Vector2(
                p.X * m.M11 + p.Y * m.M12 + m.M21,
                p.X * m.M22 + p.Y * m.M31 + m.M32
            );
        }

        public static Vector2 TransformPoint(this Matrix3x2 m, float x, float y)
        {
            return new Vector2(
                x * m.M11 + y * m.M12 + m.M21,
                x * m.M22 + y * m.M31 + m.M32
            );
        }
        
        public static Rectangle TransformRectangle(this Matrix3x2 m, ref Rectangle rec)
        {
            Vector2 topLeft = TransformPoint(m, rec.TopLeft);
            Vector2 bottomRight = TransformPoint(m, rec.BottomRight);
            
            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        public static void MultiplyBy(ref this Matrix3x2 m, float m0, float m1, float m2, float m3, float m4, float m5)
        {
            float tm0 = m.M11;
            float tm1 = m.M12;
            float tm2 = m.M21;
            float tm3 = m.M22;
            float tm4 = m.M31;
            float tm5 = m.M32;
            
            
            float r0 = tm0 * m0 + tm3 * m1;
            float r1 = tm1 * m0 + tm4 * m1;
            float r2 = tm2 * m0 + tm5 * m1 + m2;
            float r3 = tm0 * m3 + tm3 * m4;
            float r4 = tm1 * m3 + tm4 * m4;
            float r5 = tm2 * m3 + tm5 * m4 + m5;

            m.M11 = r0;
            m.M12 = r1;
            m.M21 = r2;
            m.M22 = r3;
            m.M31 = r4;
            m.M32 = r5;
            
        }

        public static void CreateTransform(Vector2 scale, float rotation, Vector2 translation, out Matrix3x2 result)
        {
            // m11 - m0
            // m12 - m1
            // m21 - m2
            // m22 - m3
            // m31 - m4
            // m32 - m5
            
            //Scale
            result.M11 = scale.X;
            result.M31 = scale.Y;
            result.M12 = result.M21 = result.M22 = result.M32 = 0f;
            
            //Rotate
            float c = Calc.Cos(rotation);
            float s = Calc.Sin(rotation);
            
            result.MultiplyBy(c, -s, 0f, s, c, 0f);
            
            //Translate
            result.MultiplyBy(1f, 0f, translation.X, 0f, 1f, translation.Y);
        }
            
    }
}