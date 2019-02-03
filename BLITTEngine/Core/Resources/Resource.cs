namespace BLITTEngine.Core.Resources
{
    public abstract class Resource
    {
        public string Name { get; internal set; }

        internal abstract void Dispose();
    }
}