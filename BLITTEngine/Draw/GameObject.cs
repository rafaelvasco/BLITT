namespace BLITTEngine.Draw
{
    public abstract class GameObject
    {
        public bool Visible;

        public abstract void Update();

        public abstract void Draw();
    }
}