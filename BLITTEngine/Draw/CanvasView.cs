using BLITTEngine.Foundation;
using BLITTEngine.Numerics;
using System.Numerics;

namespace BLITTEngine.Draw
{
    public class CanvasView
    {
        internal byte Id { get; }

        internal CanvasView(byte id,
            RectangleI viewport,
            Matrix4x4 projection_matrix,
            Blending blending,
            int clear_color=0)
        {
            this.Id = id;
            this.ProjectionMatrix = projection_matrix;
            this.Viewport = viewport;
            this.ClearColor = clear_color;

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

            RenderState = RenderState.ColorWrite | RenderState.AlphaWrite | blend_state;
        }

        public Matrix4x4 ProjectionMatrix {get; }

        public RenderState RenderState {get; }

        public RectangleI Viewport {get;}

        public int ClearColor {get;}

        private RenderState blend_state;

    }


}
