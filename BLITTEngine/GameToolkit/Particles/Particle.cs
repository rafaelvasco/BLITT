using System.Runtime.InteropServices;
using BLITTEngine.Core.Common;

namespace BLITTEngine.GameToolkit.Particles
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Gravity;
        public float RadialAccel;
        public float TangentialAccel;
        public Color StartColor;
        public Color Color;
        public Color EndColor;
        public float StartScale;
        public float Scale;
        public float EndScale;
        public float StartOpacity;
        public float Opacity;
        public float EndOpacity;
        public float Spin;
        public float SpinSpeed;
        public float Age;
        public float TerminalAge;

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Particle));
    }
}