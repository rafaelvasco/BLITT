namespace BLITTEngine.Core.Resources
{
    public abstract class Resource
    {
        public string Id { get; internal set; }

        internal abstract void Dispose();
    }
}