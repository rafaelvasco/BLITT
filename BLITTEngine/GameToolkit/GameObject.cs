namespace BLITTEngine.GameToolkit
{
    public abstract class GameObject
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        
        public float Rotation { get; protected set; }
        
        public float ScaleX { get; protected set; }
        
        public float ScaleY { get; protected set; }

        public abstract void MoveTo(float x, float y);

        public abstract void Move(float dx, float dy);

        public abstract void Rotate(float rot);

        public abstract void SetScale(float sx, float sy);

    }
}