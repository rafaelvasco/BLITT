using System;
using System.Runtime.InteropServices;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.Particles
{
    public unsafe class ParticleEmitter : IDisposable
    {
        public ParticleEmitterProps Props { get; }
        public int ParticlesAlive => _particlesAlive;

        public float Scale
        {
            get => _scale;
            set => _scale = value;
        }

        public Vector2 Position => _position;

        public Vector2 Transposition => new Vector2(_transposeX, _transposeY);
        
        
        public ParticleEmitter(Sprite sprite, ParticleEmitterProps props)
        {
            Props = props;
            _sprite = sprite;
            _sprite.SetOrigin(0.5f, 0.5f);
            _sprite.BlendMode = BlendMode.AlphaAdd;
            _scale = 1.0f;
            _age = -2.0f;


            if (props.Emission > props.MaxParticles)
            {
                props.Emission = props.MaxParticles;
            }
            
            _particles = new Particle[props.MaxParticles];
            _gcHandle = GCHandle.Alloc(_particles, GCHandleType.Pinned);
            _particlePointer = Marshal.UnsafeAddrOfPinnedArrayElement(_particles, 0);
            
        }

        ~ParticleEmitter()
        {
            Dispose();
        }

        public void FireAt(float x, float y)
        {
            MoveTo(x, y);
            Fire();
        }

        public void Fire()
        {
            if (Props.LifeTime == -1.0f)
            {
                _age = -1.0f;
            }
            else
            {
                _age = 0.0f;
            }
        }

        public void MoveTo(float x, float y, bool moveParticles=false)
        {
            

            if (!moveParticles)
            {
                if (_age == -2.0f)
                {
                    _previousPosition.X = x;
                    _previousPosition.Y = y;
                }
                else
                {
                    _previousPosition.X = _position.X;
                    _previousPosition.Y = _position.Y;
                }

            }
            else
            {
                var dx = x - _position.X;
                var dy = y - _position.Y;

                var particle = (Particle*)_particlePointer;

                for (int i = 0; i < _particlesAlive; ++i)
                {
                    (particle + i)->Position.X += dx;
                    (particle + i)->Position.Y += dy;
                }

                _previousPosition.X += dx;
                _previousPosition.Y += dy;
            }

            _position.X = x;
            _position.Y = y;
        }

        public void Stop(bool killParticles=false)
        {
            _age = -2.0f;

            if (killParticles)
            {
                _particlesAlive = 0;
            }
        }

        public void Transpose(float x, float y)
        {
            _transposeX = x;
            _transposeY = y;
        }

        public void Update(float elapsedSeconds)
        {
            if (_age >= 0)
            {
                _age += elapsedSeconds;

                if (_age >= Props.LifeTime)
                {
                    _age = -2.0f;
                }
            }

            var particleSize = Particle.SizeInBytes;
            
            float angle;

            var particle = (Particle*) _particlePointer;
            var firstParticle = (Particle*) _particlePointer;

            for (int i = 0; i < _particlesAlive; ++i)
            {
                particle->Age += elapsedSeconds;

                if (particle->Age >= particle->TerminalAge)
                {
                    _particlesAlive--;
                    
                    *particle = *(firstParticle + _particlesAlive);

                    i--;

                    continue;
                }

                var vecAccel = particle->Position - _position;
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

            if (_age != -2.0f)
            {
                
                if (Props.Emission == 0)
                {
                    goto End;
                }

                float particlesNeeded = (Props.Emission * elapsedSeconds) + _emissionResidue;
                
                int particlesCreated = (int) particlesNeeded;

                _emissionResidue = particlesNeeded - particlesCreated;
                
                if (particlesCreated == 0)
                {
                    goto End;
                }

                if (_particlesAlive >= Props.MaxParticles)
                {
                    goto End;
                }
                
                particle = (Particle*) IntPtr.Add(_particlePointer, ParticlesAlive * particleSize);

                for (int i = 0; i < particlesCreated; ++i)
                {
                    particle->Age = 0.0f;
                    particle->TerminalAge = RandomEx.Range(Props.ParticleLife);

                    var initialPositionDisplacement = RandomEx.Range(Props.InitialPositionDisplacement);

                    particle->Position.X = _previousPosition.X +
                                           (_position.X - _previousPosition.X) * RandomEx.NextFloat() +
                                           initialPositionDisplacement.X;
                        
                    particle->Position.Y = _previousPosition.Y +
                                           (_position.Y - _previousPosition.Y) * RandomEx.NextFloat() +
                                           initialPositionDisplacement.Y;

                    var spread = RandomEx.Range(Props.Spread);

                    angle = RandomEx.Range(Props.Direction) - Calc.PI_OVER2 + RandomEx.Range(0, spread) - spread / 2.0f;

                    if (Props.Relative)
                    {
                        angle += (_previousPosition - _position).Angle() + Calc.PI_OVER2;
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

                    _particlesAlive++;

                    particle++;
                }
            }
            
            End: return;
        }

        public void Draw(Canvas canvas)
        {
            var par = (Particle*) _particlePointer;
            var spr = _sprite;

            for (var i = 0; i < _particlesAlive; ++i)
            {
                spr.SetColor(par->Color.WithOpacity(par->Opacity));

                spr.DrawEx(
                    canvas,
                    par->Position.X * _scale + _transposeX,
                    par->Position.Y * _scale + _transposeY,
                    par->Spin * par->Age,
                    par->Scale * _scale
                );

                par++;
            }
        }
        

        public void Dispose()
        {
            if (!_disposed)
            {
                _gcHandle.Free();
                _particles = null;
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
        
        private GCHandle _gcHandle;
        
        private Particle[] _particles;
        
        private readonly IntPtr _particlePointer;
        
        private bool _disposed;
        
        private readonly Sprite _sprite;
        
        private float _age;
        
        private Vector2 _position;
        
        private Vector2 _previousPosition;
        
        private float _scale;
        
        private float _transposeX;
        
        private float _transposeY;
        
        private float _emissionResidue;
        
        private int _particlesAlive;
    }
}