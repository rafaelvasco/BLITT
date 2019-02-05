using BLITTEngine.Core.Resources;

namespace BLITTBuilder
{
    public class ResourceFileInfo
    {
        public string FileName { get; }
        public string FullPath { get; }
        public string Extension { get; }
        public ResourceType Type { get; }

        public ResourceFileInfo(string file_name, string full_path, string extension, ResourceType type)
        {
            this.FileName = file_name;
            this.FullPath = full_path;
            this.Extension = extension;
            this.Type = type;
        }
    }
}