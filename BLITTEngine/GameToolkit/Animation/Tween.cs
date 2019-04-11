using System;
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Common;

namespace BLITTEngine.GameToolkit.Animation
{
    public class Tween
    {
        public RangedValue TweenedValue { get; private set; }

        public float StartValue => TweenedValue.StartValue;

        public float EndValue => TweenedValue.EndValue;
        public int Duration { get; private set; } // Ticks
        public int Delay { get; private set; } // Ticks
        public bool IsPaused { get; set; }
        
        public bool IsRepeating => _remainingRepeats != 0;
        
        public bool IsRepeatingForever => _remainingRepeats < 0;
        public bool IsAutoReverse { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsComplete { get; private set; }
        
        public float TimeRemaining => Duration - _elapsedDuration;
        
        public float Completion => Calc.Clamp(_completion, 0, 1);

        private Func<float, float> _easingFunction;
        
        private bool _isInitialized;
        
        private float _completion;
        
        private int _elapsedDuration;
        
        private int _remainingDelay;
        
        private int _repeatDelay;
        
        private int _remainingRepeats;
        
        private Action<Tween> _onBegin;
        
        private Action<Tween> _onEnd;
        
        internal Tween()
        {
        }

        public void Reset(RangedValue value, int durationTicks, int delay = 0)
        {
            _elapsedDuration = 0;
            TweenedValue = value;
            _isInitialized = false;
            IsComplete = false;
            IsAlive = true;
            IsPaused = false;
            Delay = delay;
            Duration = durationTicks;
            Delay = delay;
            _remainingDelay = delay;
        }

        
        public Tween Easing(Func<float, float> easingFunction) { _easingFunction = easingFunction; return this; }
        public Tween OnBegin(Action<Tween> action) { _onBegin = action; return this; }
        public Tween OnEnd(Action<Tween> action) { _onEnd = action; return this; }
        public Tween Pause() { IsPaused = true; return this; }
        public Tween Resume() { IsPaused = false; return this; }
        
        public Tween Repeat(int count, int repeatDelayTicks = 0)
        {
            _remainingRepeats = count;
            _repeatDelay = repeatDelayTicks;
            return this;
        }

        public Tween RepeatForever(int repeatDelayTicks = 0)
        {
            _remainingRepeats = -1;
            _repeatDelay = repeatDelayTicks;
            return this;
        }

        public Tween AutoReverse()
        {
            if (_remainingRepeats == 0)
            {
                _remainingRepeats = 1;
            }

            IsAutoReverse = true;
            return this;
        }

        public void Cancel()
        {
            _remainingRepeats = 0;
            IsAlive = false;
        }

        public void CancelAndComplete()
        {
            if (IsAlive)
            {
                _completion = 1;
                
                Interpolate(1);

                IsComplete = true;
                
                _onEnd?.Invoke(this);
            }
        }

        public void Update()
        {
            if (IsPaused || !IsAlive) 
            {
                return;
            }

            if (_remainingDelay > 0)
            {
                _remainingDelay -= 1;

                if (_repeatDelay > 0)
                {
                    return;
                }
            }

            if (!_isInitialized)
            {
                _isInitialized = true;
                
                _onBegin?.Invoke(this);
            }

            if (IsComplete)
            {
                IsComplete = false;
                _elapsedDuration = 0;
                _onBegin?.Invoke(this);

                if (IsAutoReverse)
                {
                    Swap();
                }
            }

            _elapsedDuration += 1;

            float n = _completion = (float)_elapsedDuration / Duration;

            if (_easingFunction != null)
            {
                n = _easingFunction(n);
            }

            if (_elapsedDuration >= Duration)
            {
                if (_remainingRepeats != 0)
                {
                    if (_remainingRepeats > 0)
                    {
                        _remainingRepeats--;
                    }

                    _remainingDelay = _repeatDelay;
                }
                else if (_remainingRepeats == 0)
                {
                    IsAlive = false;
                }

                n = _completion = 1;
                IsComplete = true;
            }
            
            Interpolate(n);

            if (IsComplete)
            {
                _onEnd?.Invoke(this);
            }
            
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Interpolate(float n)
        {
            TweenedValue.Value = TweenedValue.StartValue + (TweenedValue.EndValue - TweenedValue.StartValue) * n;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap()
        {
            TweenedValue.Swap();
        }

    }
}