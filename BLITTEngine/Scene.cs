
using BLITTEngine.Core.Graphics;

namespace BLITTEngine
{
    public abstract class Scene
    {
        public static Game Game {get; internal set;}
        public static Canvas Canvas {get; internal set;}

        public abstract void Init();

        public abstract void Update(float dt);

        public abstract void Draw(Canvas canvas);
    }
}