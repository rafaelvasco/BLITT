using BLITTEngine.Core.Graphics;
using BLITTEngine.Numerics;
using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.GameObjects
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Particle
    {
        public Vector2 Loc;
        public Vector2 Vel;
        public float Gravity;
        public float RadialAccel;
        public float TangentialAccel;
        public float Spin;
        public float SpinDelta;
        public float Size;
        public float SizeDelta;
        public uint Color;
        public uint ColorDelta;
        public float Age;
        public float TerminalAge;

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Particle));
    }

    public struct ParticleEmitterInfo
    {
        public int MaxParticles;

        internal Sprite ParticleSprite;
        public int Emission; // Partices per sec
        public float LifeTime;

        public float ParticleLifeMin;
        public float ParticleLifeMax;

        public float Direction;
        public float Spread;
        public bool Relative;

        public float SpeedMin;
        public float SpeedMax;

        public float GravityMin;
        public float GravityMax;

        public float RadialAccelMin;
        public float RadialAccelMax;

        public float TangentialAccelMin;
        public float TangentialAccelMax;

        public float SizeStart;
        public float SizeEnd;
        public float SizeVariation;

        public float SpinStart;
        public float SpinEnd;
        public float SpinVariation;

        public uint ColorStart;
        public uint ColorEnd;

        public float ColorVariation;
        public float AlphaVariation;
    }

    public unsafe class ParticleEmitter : IDisposable
    {
        public ParticleEmitterInfo Info;

        public float Scale
        {
            get => scale;
            set => scale = value;
        }

        public int ParticlesAlive => particles_alive;

        public Vector2 Position => loc;

        public Vector2 Transposition => new Vector2(tx, ty);

        public ParticleEmitter(ParticleEmitterInfo info, Sprite particle_sprite)
        {
            loc.X = prev_loc.X = 0.0f;
            loc.Y = prev_loc.Y = 0.0f;
            tx = ty = 0;
            emission_residue = 0.0f;
            particles_alive = 0;
            Info = info;
            Info.ParticleSprite = particle_sprite;
            scale = 1.0f;
            age = -2.0f;
            particles = new ParticleBuffer(info.MaxParticles);
            random = new RandomEx();
        }

        public void Render(Canvas canvas)
        {
            Particle* par = (Particle*)particles.NativePointer;
            Sprite spr = Info.ParticleSprite;

            for(var i = 0; i < particles_alive; ++i)
            {
                spr.RenderEx(
                    canvas,
                    par->Loc.X * scale + tx,
                    par->Loc.Y * scale + ty,
                    par->Spin * par->Age,
                    par->Size * scale
                );

                par++;
            }

        }

        public void FireAt(float x, float y)
        {
            Stop();
            MoveTo(x, y);
            Fire();
        }

        public void Fire()
        {
            if(Info.LifeTime == -1.0f)
            {
                age = -1.0f;
            }
            else
            {
                age = 0.0f;
            }
        }

        public void Stop(bool kill_particles = false)
        {
            age = -2.0f;

            if(kill_particles)
            {
                particles_alive = 0;
            }
        }

        public void Transpose(float x, float y)
        {
            tx = x;
            ty = y;
        }

        public void MoveTo(float x, float y, bool move_particles = false)
        {
            if (move_particles)
            {
                float dx = x - loc.X;
                float dy = y - loc.Y;

                Particle* particle = (Particle*)particles.NativePointer;

                for(var i = 0; i < particles_alive; ++i)
                {
                    (particle + i)->Loc.X += dx;
                    (particle + i)->Loc.Y += dy;
                }

                prev_loc.X += dx;
                prev_loc.Y += dy;
            }
            else
            {
                if (age == -2.0f)
                {
                    prev_loc.X = x;
                    prev_loc.Y = y;
                }
                else
                {
                    prev_loc.X = loc.X;
                    prev_loc.Y = loc.Y;
                }
            }

            loc.X = x;
            loc.Y = y;
        }

        public void Update(float dt)
        {
            int i;
            float angle;
            float rand_val;
            var inf = Info;
            var part_size = Particle.SizeInBytes;
            var pi2 = Calc.PI_OVER2;

            if (age >= 0)
            {
                age += dt;
                if(age >= inf.LifeTime)
                {
                    age = -2.0f;
                }
            }

            var particle = (Particle*)particles.NativePointer;
            var first_particle = (Particle*)particles.NativePointer;

            // Update Active Particles

            for(i = 0; i < particles_alive; ++i)
            {
                particle->Age += dt;

                if(particle->Age >= particle->TerminalAge)
                {
                    particles_alive--;

                    *(particle) = *(first_particle+particles_alive);

                    i--;

                    continue;
                }

                Vector2 vec_accel = (particle->Loc - loc);
                vec_accel.Normalize();

                Vector2 vec_accel2 = vec_accel;

                vec_accel *= particle->RadialAccel;

                angle = vec_accel2.X;

                vec_accel2.X = -vec_accel2.Y;
                vec_accel2.Y = angle;

                vec_accel2 *= particle->TangentialAccel;


                particle->Vel.X += (vec_accel.X + vec_accel2.Y) * dt;
                particle->Vel.Y += (vec_accel.Y + vec_accel2.Y) * dt + particle->Gravity * dt;

                particle->Loc.X += particle->Vel.X * dt;
                particle->Loc.Y += particle->Vel.Y * dt;

                particle->Spin += particle->SpinDelta * dt;
                particle->Size += particle->SizeDelta * dt;

                // Color TODO

                particle++;
            }

            // Generate New Particles

            if (age != -2.0f)
            {
                if(inf.Emission == 0)
                {
                    goto end;
                }

                /*if (particles_alive >= inf.MaxParticles-1)
                {
                   goto end;
                }*/

                float particles_needed = inf.Emission * dt + emission_residue;
                int particles_created = (int) particles_needed;

                emission_residue = particles_needed - particles_created;

                particle = (Particle*) IntPtr.Add(particles.NativePointer, particles_alive * part_size);

                for(i=0; i < particles_created; ++i)
                {

                    if(particles_alive >= inf.MaxParticles)
                    {
                        break;
                    }

                    rand_val = random.NextFloat();

                    particle->Age = 0.0f;
                    particle->TerminalAge = random.NextFloat(inf.ParticleLifeMin, inf.ParticleLifeMax);

                    particle->Loc.X = prev_loc.X + ((loc.X - prev_loc.X) * rand_val) + random.NextFloat(-2.0f, 2.0f);
                    particle->Loc.Y = prev_loc.Y + ((loc.Y - prev_loc.Y) * rand_val) + random.NextFloat(-2.0f, 2.0f);

                    angle = inf.Direction - pi2 + random.NextFloat(0, inf.Spread) - inf.Spread / 2.0f;

                    if (inf.Relative)
                    {
                        angle += (prev_loc - loc).Angle + pi2;
                    }

                    particle->Vel.X = Calc.Cos(angle);
                    particle->Vel.Y = Calc.Sin(angle);

                    rand_val = random.NextFloat(inf.SpeedMin, inf.SpeedMax);

                    particle->Vel.X *= rand_val;
                    particle->Vel.Y *= rand_val;

                    rand_val = random.NextFloat(inf.GravityMin, inf.GravityMax);

                    particle->Gravity = random.NextFloat(inf.GravityMin, inf.GravityMax);

                    particle->RadialAccel = random.NextFloat(inf.RadialAccelMin, inf.RadialAccelMax);

                    particle->TangentialAccel = random.NextFloat(inf.TangentialAccelMin, inf.TangentialAccelMax);

                    particle->Size = random.NextFloat(inf.SizeStart, inf.SizeStart + (inf.SizeEnd - inf.SizeStart) * inf.SizeVariation);

                    particle->SizeDelta = (inf.SizeEnd - particle->Size) / particle->TerminalAge;

                    particle->Spin = random.NextFloat(inf.SpinStart, inf.SpinStart + (inf.SpinEnd - inf.SpinStart) * inf.SpinVariation);

                    particle->SpinDelta = (inf.SpinEnd - particle->Spin) / particle->TerminalAge;

                    // Color TODO

                    particles_alive++;

                    particle++;
                }
            }

            end: prev_loc = loc;
        }

        public void Dispose()
        {
            this.particles.Dispose();
        }

        private float age;
        private float emission_residue;
        private Vector2 prev_loc;
        private Vector2 loc;
        private float tx;
        private float ty;
        private float scale;
        private int particles_alive;
        private ParticleBuffer particles;
        private RandomEx random;


    }

    internal unsafe class ParticleBuffer : IDisposable
    {

        public Particle[] Particles;
        public readonly IntPtr NativePointer;
        private GCHandle gc_handle;
        private bool disposed;

        public ParticleBuffer(int size)
        {
            Particles = new Particle[size];
            gc_handle = GCHandle.Alloc(Particles, GCHandleType.Pinned);
            NativePointer = Marshal.UnsafeAddrOfPinnedArrayElement(Particles, 0);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                gc_handle.Free();
                Particles = null;
                disposed = true;

            }

            GC.SuppressFinalize(this);
        }

        ~ParticleBuffer()
        {
            Dispose();
        }
    }

}
