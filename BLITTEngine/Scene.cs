using BLITTEngine.Draw;

namespace BLITTEngine
{
    public abstract class Scene
    {
        public abstract void Init();

        public abstract void Update(float dt);

        public abstract void Draw(Canvas canvas);
    }
}