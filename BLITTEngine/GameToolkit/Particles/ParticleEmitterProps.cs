using BLITTEngine.Core.Common;

namespace BLITTEngine.GameToolkit.Particles
{
    public class ParticleEmitterProps
    {
        public int MaxParticles;
        public int Emission;
        public float LifeTime;
        public bool Relative;
        public Range<float> ParticleLife;
        public Range<Vector2> InitialPositionDisplacement;
        public Color StartColor;
        public Color EndColor;
        public Range<float> StartOpacity;
        public Range<float> EndOpacity;
        public Range<float> StartScale;
        public Range<float> EndScale;
        public Range<float> Direction;
        public Range<float> Spread;
        public Range<float> Speed;
        public Range<float> SpinSpeed;
        public Range<float> Gravity;
        public Range<float> RadialAccel;
        public Range<float> TangentialAccel;

    }
}