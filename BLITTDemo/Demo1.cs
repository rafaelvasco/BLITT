using BLITTEngine;
using BLITTEngine.Draw;

namespace BLITTDemo
{
    public class Demo1 : Scene
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