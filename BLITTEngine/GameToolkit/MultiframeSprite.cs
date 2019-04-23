using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.GameToolkit
{
    public class MultiframeSprite : Sprite
    {
        public bool Paused { get; set; }

        private const int DEFAULT_FRAME_DELAY = 5;

        public enum AnimationMode : byte
        {
            OneTime = 0,
            Loop,
            PingPong
        }
        
        public MultiframeSprite(TextureAtlas sprite_sheet) : base(
            sprite_sheet.Texture, 0, 0, 0, 0
        )
        {
            
            _animation_map = new Dictionary<string, int[]>();
            _animation_modes = new Dictionary<string, AnimationMode>();
            _frame_origins = new Vector2[sprite_sheet.Count];
            _frame_delays = new float[sprite_sheet.Count];
            _quads = new Quad[sprite_sheet.Count];
            
            for (int i = 0; i < sprite_sheet.Count; ++i)
            {
                _frame_origins[i] = new Vector2(0.5f, 0.5f);
                _frame_delays[i] = DEFAULT_FRAME_DELAY;
                _quads[i] = sprite_sheet[i];

            }

            _quad_index = 0;
            _current_mode = AnimationMode.Loop;
            
            _quad = _quads[_quad_index];
             
            Width = _quads[_quad_index].Width;
            Height = _quads[_quad_index].Height;
            
            SetOrigin(0.5f, 0.5f);
            
            UpdateFrame();

        }

        public MultiframeSprite AddAnimation(string name, params int[] indices)
        {
            _animation_map.Add(name, indices);
            _animation_modes.Add(name, AnimationMode.Loop);
            
            return this;
        }

        public void SetAnimation(string name)
        {
            if (name == _current_anim)
            {
                return;
            }
            
            if (_animation_map.TryGetValue(name, out var frames))
            {
                _current_anim = name;
                
                Paused = false;

                _frame_index = 0;
                
                _quad_index = frames[_frame_index];

                _current_frames = frames;
                
                _current_mode = _animation_modes[name];
                
                UpdateFrame();
                
            }
        }

        public void SetFrame(int index)
        {
            Paused = true;

            _quad_index = Calc.Clamp(index, 0, _quads.Length - 1);

            UpdateFrame();

        }

        public void SetFrame(string animation, int index)
        {
            Paused = true;
            
            if (_animation_map.TryGetValue(animation, out var frames))
            {
                _quad_index = frames[index];
                
                UpdateFrame();
            }
        }

        public void SetFrameSpeed(int ticks)
        {
            for (int i = 0; i < _frame_delays.Length; ++i)
            {
                _frame_delays[i] = ticks;
            }
        }

        public void SetFrameSpeed(string animation, int ticks, int index=-1)
        {
            if (_animation_map.TryGetValue(animation, out var frames))
            {
                if (index != -1)
                {
                    var idx = Calc.Clamp(index, 0, frames.Length - 1);

                    _frame_delays[frames[idx]] = ticks;
                }
                else
                {
                    for (int i = 0; i < frames.Length; ++i)
                    {
                        _frame_delays[frames[i]] = ticks;
                    }
                }
                
            }
        }
        
        public void SetFrameOrigin(Vector2 origin)
        {
            for (int i = 0; i < _frame_origins.Length; ++i)
            {
                _frame_origins[i] = origin;
            }
        }

        public void SetFrameOrigin(string animation, int index=-1, Vector2 origin=default)
        {
            if (_animation_map.TryGetValue(animation, out var frames))
            {
                if (index != -1)
                {
                    var idx = Calc.Clamp(index, 0, frames.Length - 1);

                    _frame_origins[frames[idx]] = origin;
                }
                else
                {
                    for (int i = 0; i < frames.Length; ++i)
                    {
                        _frame_origins[frames[i]] = origin;
                    }
                }
                
            }
        }

        public new void SetColor(Color color)
        {
            _color = color.ABGR;
            
            _quad.V0.Col = _color;
            _quad.V1.Col = _color;
            _quad.V2.Col = _color;
            _quad.V3.Col = _color;
        }
        
        public void SetAnimMode(string animation_name, AnimationMode mode)
        {
            if (_animation_modes.TryGetValue(animation_name, out _))
            {
                _animation_modes[animation_name] = mode;
            }
        }

        public void SetPaused(bool paused)
        {
            Paused = paused;
        }
        
        public void Update()
        {
            if (Paused || _current_frames == null || _current_frames.Length == 1)
            {
                return;
            }

            _timer += 1;
            
            if (!(_timer > _frame_delays[_quad_index])) return;

            _frame_index += _anim_direction;

            int max_idx = _current_frames.Length - 1;
            
            switch (_current_mode)
            {
                case AnimationMode.OneTime:

                    if (_frame_index > max_idx)
                    {
                        _frame_index = max_idx;
                        Paused = true;
                    }
                    
                    break;
                case AnimationMode.Loop:
                    
                    if (_frame_index > max_idx)
                    {
                        _frame_index = 0;
                    }
                    
                    break;
                case AnimationMode.PingPong:
                    
                    if (_anim_direction > 0 && _frame_index > max_idx)
                    {
                        _frame_index = max_idx;
                        _anim_direction = -1;
                    }
                    else if (_anim_direction < 0 && _frame_index < 0)
                    {
                        _frame_index = 0;
                        _anim_direction = 1;
                    }
                    
                    break;
            }

            _quad_index = _current_frames[_frame_index];

            UpdateFrame();

            _timer = 0;

        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFrame()
        {
                
            _quad = _quads[_quad_index];
            
            Width = _quad.Width;
            Height = _quad.Height;
            
            ref var orig = ref _frame_origins[_quad_index];
            
            SetOrigin(ref orig);
            
            var f_x = _flip_x;
            var f_y = _flip_y;
            
            var f_orig = _orig_flip;
            
            _flip_x = false;
            _flip_y = false;
            
            SetFlip(f_x, f_y, f_orig);
            
            SetColor(_color);
        }
        
        private AnimationMode _current_mode;

        private int[] _current_frames;

        //private readonly TextureAtlas _sprite_sheet;

        private int _quad_index;

        private int _frame_index;

        private readonly Dictionary<string, int[]> _animation_map;

        private readonly Vector2[] _frame_origins;

        private readonly float[] _frame_delays;

        private readonly Quad[] _quads;

        private readonly Dictionary<string, AnimationMode> _animation_modes;

        private string _current_anim;

        private int _anim_direction = 1;

        private float _timer;

        private uint _color = 0xFFFFFFFF;

    }

}