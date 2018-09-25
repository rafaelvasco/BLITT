namespace BLITTEngine.Graphics
{
    public class Image
    {
        internal uint TextureHandle;
        
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        internal Image(uint texture_handle)
        {
            this.TextureHandle = texture_handle;
        }
    }
}