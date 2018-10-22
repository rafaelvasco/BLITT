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

            Renderer.Flush();
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
            if (!ready_to_draw)
            {
                return;
            }

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
            if (!ready_to_draw)
            {
                return;
            }

            Renderer.AddQuad(texture, x, y, in quad);
        }

        public void FillRect(float x, float y, float w, float h, Color color)
        {
            if (!ready_to_draw)
            {
                return;
            }

            Renderer.AddRect(x, y, w, h, color);
        }

        public void FillGradient(float x, float y, float w, float h, Gradient gradient)
        {
            if (!ready_to_draw)
            {
                return;
            }

            uint col1 = gradient.TopLeftCol;
            uint col2 = gradient.TopRightCol;
            uint col3 = gradient.BottomLeftCol;
            uint col4 = gradient.BottomRightCol;

            Renderer.AddRect(x, y, w, h, col1, col2, col3, col4);
        }

        public void DrawRect(float x, float y, float w, float h, int line_width, Color color)
        {
            if (!ready_to_draw)
            {
                return;
            }

            Renderer.AddLine(x, y, x+w, y, line_width, color);
            Renderer.AddLine(x+w, y, x+w, y+h, line_width, color);
            Renderer.AddLine(x, y+h, x+w, y+h, line_width, color);
            Renderer.AddLine(x, y, x, y+h, line_width, color);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float line_width, Color color)
        {
            if (!ready_to_draw)
            {
                return;
            }

            Renderer.AddLine(x1, y1, x2, y2, line_width, color);
        }

        public void DrawText(string text, float x, float y, Font font, float scale = 1.0f)
        {
            if (!ready_to_draw)
            {
                return;
            }

            for(var i = 0; i < text.Length; ++i)
            {
                Quad quad = font[text[i]];

                Renderer.AddQuad(font.Texture, x + (i * quad.W) + quad.U * font.Texture.Width, y, in quad);
            }


        }
    }
}