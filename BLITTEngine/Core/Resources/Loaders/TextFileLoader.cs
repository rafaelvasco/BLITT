using System;
using System.Collections.Generic;
using System.IO;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class TextFileLoader : BaseLoader
    {
        public override Resource Load(Stream stream)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line)) lines.Add(line);
            }

            if (lines.Count == 0) throw new Exception("Text file is empty");

            var text_file_resource = new TextFile(lines);

            return text_file_resource;
        }
    }
}