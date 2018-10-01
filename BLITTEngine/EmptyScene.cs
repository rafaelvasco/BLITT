using BLITTEngine.Draw;

namespace BLITTEngine
{
    public class EmptyScene : Scene
    {
        private Canvas canvas;
        
        public override void Init()
        {
            canvas = new Canvas(Game.Instance.GraphicsDevice);
        }

        public override void Update(float dt)
        {
        }

        public override void Draw()
        {
        }
    }
}