
using System;
using System.Collections.Generic;
using System.Reflection;
using BLITTEngine.Core.Common;

namespace BLITTEngine.GameToolkit.Animation
{
    public class Tweener : IDisposable
    {
        public void Dispose()
        {
        }
        
        public long AllocationCount { get; private set; }
        
        private readonly List<Tween> _activeTweens = new List<Tween>();
        private readonly Dictionary<int, int> _tweenMap = new Dictionary<int, int>();
        private readonly Stack<Tween> _tweenPool = new Stack<Tween>();

        public Tween Tween(int id, RangedValue value, int durationTicks, int delayTicks=0)
        {
            Tween tween = FindTween(id);

            if (tween != null)
            {
                return tween;
            }
            
            Console.WriteLine("Start Tween");
            
            if (_tweenPool.Count > 0)
            {
                tween = _tweenPool.Pop();
                
                tween.Reset(value, durationTicks, delayTicks);

                _activeTweens.Add(tween);
                
                return tween;
            }
            
            tween = new Tween();

            AllocationCount++;
            
            tween.Reset(value, durationTicks, delayTicks);
            
            _activeTweens.Add(tween);
            
            _tweenMap.Add(id, _activeTweens.Count - 1);

            return tween;
        }

        public Tween FindTween(int id)
        {
            return _tweenMap.TryGetValue(id, out var index) ? _activeTweens[index] : null;
        }

        public void Update()
        {
            for (int i = _activeTweens.Count-1; i >= 0; i--)
            {
                var tween = _activeTweens[i];
                
                tween.Update();

                if (!tween.IsAlive)
                {
                    _activeTweens.RemoveAt(i);

                    _tweenMap.Remove(i);
                    
                    _tweenPool.Push(tween);
                }
            }
        }

        public void CancelAll()
        {
            foreach (var tween in _activeTweens)
            {
                tween.Cancel();
            }
        }

        public void CancelAndCompleteAll()
        {
            foreach (var tween in _activeTweens)
            {
                tween.CancelAndComplete();
            }
        }
    }
}