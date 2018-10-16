using BLITTEngine.Core.Graphics;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using System.Numerics;

namespace BLITTEngine.Draw
{
    public enum CanvasPivot
    {
        TopLeft,
        Center
    }

    public enum Blending
    {
        None,
        Alpha,
        Add,
    }

    public class CanvasLayer
    {
        public CanvasPivot Origin
        {
            get => origin;

            set
            {
                if (origin != value)
                {
                    origin = value;

                    UpdateRenderGroup();

                    Modified = true;
                }
            }
        }

        public RectangleI Viewport
        {
            get => viewport;
            set
            {
                if (viewport != value)
                {
                    viewport = value;

                    UpdateRenderGroup();

                    Modified = true;
                }
            }
        }

        public Blending Blending
        {
            get => blending;
            set
            {
                if (blending != value)
                {
                    blending = value;

                    UpdateRenderGroup();

                    Modified = true;
                }
            }
        }

        private CanvasPivot origin;
        private RectangleI viewport;
        private Blending blending;
        internal RenderGroup RenderGroup;
        internal bool Modified;

        internal CanvasLayer(byte id, CanvasPivot origin, RectangleI viewport, Blending blending)
        {
            this.origin = origin;
            this.viewport = viewport;
            this.blending = blending;

            if (RenderGroup == null)
            {
                RenderGroup = new RenderGroup()
                {
                    Id = id
                };
            }

            this.UpdateRenderGroup();
        }

        private void UpdateRenderGroup()
        {
            Matrix4x4 view_proj = Matrix4x4.Identity;

            var hcw = viewport.W / 2;
            var hch = viewport.H / 2;

            switch (origin)
            {
                case CanvasPivot.TopLeft:

                    view_proj = Matrix4x4.CreateOrthographicOffCenter(
                        left: 0,
                        right: viewport.W,
                        bottom: viewport.H,
                        top: 0,
                        0.0f,
                        1.0f
                    );

                    break;

                case CanvasPivot.Center:

                    view_proj = Matrix4x4.CreateOrthographicOffCenter(
                        left: -hcw,
                        right: hcw,
                        bottom: hch,
                        top: -hch,
                        0.0f,
                        1.0f
                    );

                    break;
            }

            var blend_state = RenderState.None;

            switch (blending)
            {
                case Blending.None:
                    blend_state = RenderState.BlendFunction(RenderState.BlendOne, RenderState.BlendZero);

                    break;

                case Blending.Alpha:

                    blend_state = RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInverseSourceAlpha);

                    break;

                case Blending.Add:

                    blend_state = RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);

                    break;
            }

            var render_state = RenderState.ColorWrite | RenderState.AlphaWrite | blend_state;

            RenderGroup.ProjectionMatrix = view_proj;
            RenderGroup.RenderState = render_state;
            RenderGroup.Viewport = viewport;
        }
    }

    public class Canvas
    {
        private CanvasLayer[] layers;

        private byte layer_idx;

        public int Width { get; private set; }

        public int Height { get; private set; }

        private bool ready_to_draw = false;

        internal Canvas(int width, int height)
        {
            Width = width;
            Height = height;

            layers = new CanvasLayer[255];

            CreateLayer(CanvasPivot.Center);
        }

        public void CreateLayer(CanvasPivot origin, RectangleI viewport = default, Blending blending = Blending.Alpha)
        {
            if (viewport.IsEmpty)
            {
                viewport = new RectangleI(0, 0, Width, Height);
            }

            var layer = new CanvasLayer(layer_idx, origin, viewport, blending);

            layers[layer_idx++] = layer;

            Renderer.SetupRenderGroup(layer.RenderGroup);
        }

        public CanvasLayer GetLayer(byte idx)
        {
            return layers[idx];
        }

        public void Clear(Color color)
        {
            Renderer.Clear(in color);
        }

        public void Begin(byte layer = 0)
        {
            ready_to_draw = true;

            var layer_obj = layers[layer];

            if (layer_obj.Modified)
            {
                Renderer.SetupRenderGroup(layer_obj.RenderGroup);

                layer_obj.Modified = false;
            }

            Renderer.Begin(layer_obj.RenderGroup);
        }

        public void End()
        {
            ready_to_draw = false;

            Renderer.End();
        }

        public void Draw(GameObject drawable)
        {
            if (!ready_to_draw)
            {
                return;
            }

            drawable.Draw();
        }

        public void DrawTexture(Texture2D texture, float x, float y, Color color)
        {
            var quad = new Quad()
            {
                U = 0,
                V = 0,
                U2 = 1,
                V2 = 1,
                W = texture.Width,
                H = texture.Height,
                Col = color
            };

            Renderer.AddQuad(texture, x, y, in quad);
        }

        public void DrawTexture(Texture2D texture, float x, float y, Quad quad)
        {
            Renderer.AddQuad(texture, x, y, in quad);
        }
    }
}