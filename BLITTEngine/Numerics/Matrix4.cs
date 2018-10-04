using System.Runtime.InteropServices;

namespace BLITTEngine.Numerics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4
    {
        public float M11, M12, M13, M14;
        public float M21, M22, M23, M24;
        public float M31, M32, M33, M34;
        public float M41, M42, M43, M44;

        public static Matrix4 Identity
        {
            get { return identity; }
        }

        private static Matrix4 identity = new Matrix4(1f, 0f, 0f, 0f,
                                                    0f, 1f, 0f, 0f,
                                                    0f, 0f, 1f, 0f,
                                                    0f, 0f, 0f, 1f);



        public Matrix4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
                      float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix4 result)
        {
			result.M11 = 2.0f / (right - left);
            result.M12 = result.M13 = result.M14 = 0.0f;

            result.M22 = 2.0f / (top - bottom);
            result.M21 = result.M23 = result.M24 = 0.0f;

            result.M33 = 1.0f / (zNearPlane - zFarPlane);
            result.M31 = result.M32 = result.M34 = 0.0f;

            result.M41 = (left + right) / (left - right);
            result.M42 = (top + bottom) / (bottom - top);
            result.M43 = zNearPlane / (zNearPlane - zFarPlane);
            result.M44 = 1.0f;
		}

    }
}