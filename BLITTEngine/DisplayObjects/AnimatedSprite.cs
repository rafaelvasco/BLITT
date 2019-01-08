//using BLITTEngine.Core.Graphics;
//using BLITTEngine.Numerics;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;

//namespace BLITTEngine.GameObjects
//{
//    public class AnimatedSprite : GameObject
//    {
//        private const int DEFAULT_FRAME_DELAY = 5;
//        private const float DEFAULT_ORIGIN = 0.5f;

//        public float X;

//        public float Y;

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct FrameProps
//        {
//            public int FrameIndex;
//            public Vector2 FrameDrawOrigin;
//            public int FrameDelay;

//            public FrameProps(int idx, Vector2 origin, int delay)
//            {
//                this.FrameIndex = idx;
//                this.FrameDrawOrigin = origin;
//                this.FrameDelay = delay;
//            }
//        }

//        public class Animation
//        {
//            public readonly string Name;

//            internal int SpriteSheetFrameIdx => sprite_sheet_frames[frame_index];

//            public int FrameIndex
//            {
//                get => frame_index;
//                set
//                {
//                    frame_index = value;

//                    Calc.Clamp(sprite_sheet_frames, ref frame_index);
//                }
//            }

//            private readonly int[] sprite_sheet_frames;

//            private readonly Vector2[] frame_draw_origins;

//            private readonly int[] frame_delays;

//            private readonly int length;

//            private int tick_counter;

//            private int frame_index;

//            internal Animation(string name, params FrameProps[] frame_defs)
//            {
//                this.Name = name;

//                this.sprite_sheet_frames = new int[frame_defs.Length];
//                this.frame_draw_origins = new Vector2[this.sprite_sheet_frames.Length];
//                this.frame_delays = new int[this.sprite_sheet_frames.Length];

//                var idx = 0;

//                foreach (var frame_def in frame_defs)
//                {
//                    this.sprite_sheet_frames[idx] = frame_defs[idx].FrameIndex;
//                    this.frame_draw_origins[idx] = frame_defs[idx].FrameDrawOrigin;
//                    this.frame_delays[idx] = frame_defs[idx].FrameDelay;

//                    idx++;
//                }

//                this.length = sprite_sheet_frames.Length;
//            }

//            internal void SetFrameDelay(int frame_idx, int delay)
//            {
//                Calc.Clamp(sprite_sheet_frames, ref frame_idx);

//                frame_delays[frame_idx] = delay;
//            }

//            internal void SetFrameDrawOrigin(int frame_idx, Vector2 origin)
//            {
//                Calc.Clamp(sprite_sheet_frames, ref frame_idx);

//                frame_draw_origins[frame_idx] = origin;
//            }

//            internal ref Vector2 CurrentOrigin()
//            {
//                return ref frame_draw_origins[frame_index];
//            }

//            internal void Update()
//            {
//                tick_counter += 1;

//                if (tick_counter > frame_delays[frame_index])
//                {
//                    frame_index += 1;

//                    if (frame_index > length - 1)
//                    {
//                        frame_index = 0;
//                    }

//                    tick_counter = 0;
//                }
//            }
//        }

//        public float Scale
//        {
//            get => scale;
//            set
//            {
//                scale = Calc.Max(value, 0.0f);
//            }
//        }

//        private Dictionary<string, Animation> animations;

//        private Animation current_animation;

//        private readonly SpriteSheet sprite_sheet;

//        private float scale;

//        public AnimatedSprite(SpriteSheet sprite_sheet)
//        {
//            this.sprite_sheet = sprite_sheet;
//            this.animations = new Dictionary<string, Animation>();
//            this.scale = 1.0f;
//        }

//        // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

//        public AnimatedSprite AddAnimation(string name, params int[] sprite_sheet_frame_indices)
//        {
//            var frame_defs = new FrameProps[sprite_sheet_frame_indices.Length];

//            for (var i = 0; i < sprite_sheet_frame_indices.Length; i++)
//            {
//                var frame = sprite_sheet_frame_indices[i];
//                var frame_w = sprite_sheet[frame].W;
//                var frame_h = sprite_sheet[frame].H;

//                Vector2 default_origin = new Vector2(DEFAULT_ORIGIN, DEFAULT_ORIGIN);
//                frame_defs[i] = new FrameProps(frame, default_origin, DEFAULT_FRAME_DELAY);
//            }

//            return this.AddAnimation(name, frame_defs);
//        }

//        public AnimatedSprite AddAnimation(string name, params FrameProps[] frame_defs)
//        {
//            var animation = new Animation(name, frame_defs);

//            animations.Add(name, animation);

//            if (current_animation == null)
//            {
//                current_animation = animation;
//            }

//            return this;
//        }

//        public Animation GetAnimation(string name)
//        {
//            return animations.TryGetValue(name, out Animation animation) ? animation : null;
//        }

//        public void SetAnimationFrameDelay(string name, int frame, int delay)
//        {
//            if (animations.TryGetValue(name, out var anim))
//            {
//                anim.SetFrameDelay(frame, delay);
//            }
//        }

//        public void SetAnimationFrameOrigin(string name, int frame, Vector2 origin)
//        {
//            if (animations.TryGetValue(name, out var anim))
//            {
//                anim.SetFrameDrawOrigin(frame, origin);
//            }
//        }

//        public AnimatedSprite SetAnimation(string name)
//        {
//            if (!animations.ContainsKey(name)) return this;

//            current_animation = animations[name];
//            current_animation.FrameIndex = 0;

//            return this;
//        }

//        public override void Update()
//        {
//            current_animation.Update();
//        }

//        public override void Draw()
//        {
//            if (sprite_sheet == null)
//            {
//                return;
//            }

//            var anim = current_animation;

//            var quad = sprite_sheet[anim.SpriteSheetFrameIdx];

//            ref Vector2 origin = ref anim.CurrentOrigin();

//            quad.W *= scale;
//            quad.H *= scale;

//            Vector2 offset = new Vector2(origin.X * quad.W, origin.Y * quad.H);

//            Renderer.AddQuad(sprite_sheet.Texture, X - offset.X, Y - offset.Y, in quad);
//        }
//    }
//}