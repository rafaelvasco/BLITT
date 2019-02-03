using System.IO;

namespace BLITTEngine.Core.Resources.Loaders
{
    public abstract class BaseLoader
    {
        public virtual Resource Load(Stream stream)
        {
            return null;
        }

        public virtual Resource Load(Stream stream1, Stream stream2)
        {
            return null;
        }
    }
}