using System.IO;
using BLITTEngine.Core.Resources;

namespace BLITTBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var folder_path = args[0];

            ResourcePak pak = Builder.BuildPak(folder_path);

            byte[] pak_bytes = BinarySerializer.Serialize(pak);

            var save_path = Path.Combine(folder_path, "resources.pak");
            
            File.WriteAllBytes(save_path, pak_bytes);
        }
    }
}