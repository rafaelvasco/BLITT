using System;
using System.Runtime.InteropServices;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.Particles
{
    public unsafe class ParticleEmitter : IDisposable
    {
        public ParticleEmitterProps Props { get; }
        public int ParticlesAlive => particles_alive;

        public float Scale
        {
            get => scale;
            set => scale = value;
        }

        public Vector2 Position => position;

        public Vector2 Transposition => new Vector2(transpose_x, transpose_y);
        
        
        public ParticleEmitter(Sprite sprite, ParticleEmitterProps props)
        {
            Props = props;
            this.sprite = sprite;
            this.sprite.SetOrigin(0.5f, 0.5f);
            this.sprite.SetBlendMode(BlendMode.AlphaAdd);
            scale = 1.0f;
            age = -2.0f;


            if (props.Emission > props.MaxParticles)
            {
                props.Emission = props.MaxParticles;
            }
            
            particles = new Particle[props.MaxParticles];
            gc_handle = GCHandle.Alloc(particles, GCHandleType.Pinned);
            particle_pointer = Marshal.UnsafeAddrOfPinnedArrayElement(particles, 0);
            
        }

        ~ParticleEmitter()
        {
            Dispose();
        }

        public void FireAt(float x, float y)
        {
            if (age != 1.0f)
            {
                Stop();
            }
            
            MoveTo(x, y);
            Fire();
        }

        public void Fire()
        {
            if (Props.LifeTime == -1.0f)
            {
                age = -1.0f;
            }
            else
            {
                age = 0.0f;
            }
        }

        public void MoveTo(float x, float y, bool moveParticles=false)
        {
            

            if (!moveParticles)
            {
                if (age == -2.0f)
                {
                    previous_position.X = x;
                    previous_position.Y = y;
                }
                else
                {
                    previous_position.X = position.X;
                    previous_position.Y = position.Y;
                }

            }
            else
            {
                var dx = x - position.X;
                var dy = y - position.Y;

                var particle = (Particle*)particle_pointer;

                for (int i = 0; i < particles_alive; ++i)
                {
                    (particle + i)->Position.X += dx;
                    (particle + i)->Position.Y += dy;
                }

                previous_position.X += dx;
                previous_position.Y += dy;
            }

            position.X = x;
            position.Y = y;
        }

        public void Stop(bool killParticles=false)
        {
            age = -2.0f;
            first_burst = false;

            if (killParticles)
            {
                particles_alive = 0;
            }
        }

        public void Transpose(float x, float y)
        {
            transpose_x = x;
            transpose_y = y;
        }

        public void Update(float elapsedSeconds)
        {
            if (age >= 0)
            {
                age += elapsedSeconds;

                if (age >= Props.LifeTime)
                {
                    age = -2.0f;
                    first_burst = false;
                }
            }

            var particleSize = Particle.SizeInBytes;
            
            float angle;

            var particle = (Particle*) particle_pointer;
            var firstParticle = (Particle*) particle_pointer;

            for (int i = 0; i < particles_alive; ++i)
            {
                particle->Age += elapsedSeconds;

                if (particle->Age >= particle->TerminalAge)
                {
                    particles_alive--;
                    
                    *particle = *(firstParticle + particles_alive);

                    i--;

                    continue;
                }

                var vecAccel = particle->Position - position;
                vecAccel.Normalize();

                var vecAccel2 = vecAccel;

                vecAccel *= particle->RadialAccel;

                angle = vecAccel2.X;

                vecAccel2.X = -vecAccel2.Y;
                vecAccel2.Y = angle;

                vecAccel2 *= particle->TangentialAccel;

                particle->Velocity.X += (vecAccel.X + vecAccel2.Y) * elapsedSeconds;
                particle->Velocity.Y +=
                    (vecAccel.Y + vecAccel2.Y) * elapsedSeconds + particle->Gravity * elapsedSeconds;
                
                particle->Position += particle->Velocity * elapsedSeconds;

                particle->Spin += particle->SpinSpeed * elapsedSeconds;

                var ageFactor = particle->Age / particle->TerminalAge;

                particle->Scale = particle->StartScale + (particle->EndScale - particle->StartScale) * ageFactor;

                particle->Opacity =
                    particle->StartOpacity + (particle->EndOpacity - particle->StartOpacity) * ageFactor;

                particle->Color = Color.Lerp(particle->StartColor, particle->EndColor, ageFactor);
                
                particle++;
            }
            
            
            // Generate New Particles

            if (age != -2.0f) // Age -2.0 = Stopped, Age = -1.0 = Infinite, Age > 0.0f = Finite
            {
                
                if (Props.Emission == 0)
                {
                    goto End;
                }

                if (Props.ImmediateFullEmission && age > 0.0f && first_burst)
                {
                    goto End;
                }

                int particlesCreated;

                if (Props.ImmediateFullEmission)
                {
                    particlesCreated = Props.Emission;
                    first_burst = true;
                }
                else
                {
                    float particlesNeeded = Props.Emission * elapsedSeconds + emission_residue;

                    particlesCreated = (int) particlesNeeded;

                    emission_residue = particlesNeeded - particlesCreated;
                }
                
                if (particlesCreated == 0)
                {
                    goto End;
                }

                if (particles_alive + particlesCreated >= Props.MaxParticles)
                {
                    particlesCreated = Props.MaxParticles - particles_alive;

                    if (particlesCreated == 0)
                    {
                        goto End;
                    }
                }
                
                particle = (Particle*) IntPtr.Add(particle_pointer, ParticlesAlive * particleSize);

                for (int i = 0; i < particlesCreated; ++i)
                {
                    particle->Age = 0.0f;
                    particle->TerminalAge = RandomEx.Range(Props.ParticleLife);

                    var initialPositionDisplacement = RandomEx.Range(Props.InitialPositionDisplacement);

                    particle->Position.X = previous_position.X +
                                           (position.X - previous_position.X) * RandomEx.NextFloat() +
                                           initialPositionDisplacement.X;
                        
                    particle->Position.Y = previous_position.Y +
                                           (position.Y - previous_position.Y) * RandomEx.NextFloat() +
                                           initialPositionDisplacement.Y;

                    var spread = RandomEx.Range(Props.Spread);

                    angle = RandomEx.Range(Props.Direction) - Calc.PI_OVER2 + RandomEx.Range(0, spread) - spread / 2.0f;

                    if (Props.Relative)
                    {
                        angle += (previous_position - position).Angle() + Calc.PI_OVER2;
                    }
                    
                    var speed = RandomEx.Range(Props.Speed);
                    
                    particle->Velocity.X = Calc.Cos(angle) * speed;
                    particle->Velocity.Y = Calc.Sin(angle) * speed;

                    particle->Gravity = RandomEx.Range(Props.Gravity);

                    particle->RadialAccel = RandomEx.Range(Props.RadialAccel);

                    particle->SpinSpeed = RandomEx.Range(Props.SpinSpeed);
                    
                    particle->TangentialAccel = RandomEx.Range(Props.TangentialAccel);

                    particle->StartScale = RandomEx.Range(Props.StartScale);

                    particle->EndScale = RandomEx.Range(Props.EndScale);

                    particle->Scale = particle->StartScale;

                    particle->StartOpacity = RandomEx.Range(Props.StartOpacity);

                    particle->EndOpacity = RandomEx.Range(Props.EndOpacity);

                    particle->Opacity = particle->StartOpacity;

                    RandomEx.NextColor(out particle->StartColor, Props.StartColor);
                    RandomEx.NextColor(out particle->EndColor, Props.EndColor);

                    particle->Color = particle->StartColor;

                    particles_alive++;

                    particle++;
                }
            }
            
            End: return;
        }

        public void SetBlendMode(BlendMode blend)
        {
            sprite.SetBlendMode(blend);
        }
            
        
        public void Draw(Canvas canvas)
        {
            var par = (Particle*) particle_pointer;
            var spr = sprite;
            
            for (var i = 0; i < particles_alive; ++i)
            {
                spr.SetColor(par->Color.WithOpacity(par->Opacity));

                spr.DrawEx(
                    canvas,
                    par->Position.X * scale + transpose_x,
                    par->Position.Y * scale + transpose_y,
                    par->Spin * par->Age,
                    par->Scale * scale
                );

                par++;
            }
            
        }
        

        public void Dispose()
        {
            if (!disposed)
            {
                gc_handle.Free();
                particles = null;
                disposed = true;
            }

            GC.SuppressFinalize(this);
        }
        
        private GCHandle gc_handle;
        
        private Particle[] particles;
        
        private readonly IntPtr particle_pointer;

        private BlendMode blend_mode = BlendMode.AlphaAdd;
        
        private bool disposed;
        
        private readonly Sprite sprite;
        
        private float age;
        
        private Vector2 position;
        
        private Vector2 previous_position;
        
        private float scale;
        
        private float transpose_x;
        
        private float transpose_y;
        
        private float emission_residue;
        
        private int particles_alive;

        private bool first_burst;
    }
}