using BLITTEngine.Graphics;

namespace BLITTEngine
{
    public abstract class Scene
    {
        public abstract void Update(float dt);
        public abstract void Draw(Canvas canvas);

    }
}