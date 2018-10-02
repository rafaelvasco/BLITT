using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Draw;
using BLITTEngine.Numerics;

namespace BLITTDemo
{
    public class Demo1 : Scene
    {

        private Texture2D texture;

        public override void Init()
        {
            texture = Content.Get<Texture2D>("ship");
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Draw(texture, 0, 0);


        }
    }
}