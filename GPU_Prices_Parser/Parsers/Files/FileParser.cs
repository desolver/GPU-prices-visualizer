using System;
using System.IO;
using GPU_Prices_Parser.Data;
using Newtonsoft.Json;

namespace GPU_Prices_Parser.Parsers.Files
{
    internal static class FileParser
    {
        public static Gpu ParseGpuFile(string path)
        {
            throw new NotImplementedException();
        }

        public static Store ParseStoreFile(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"Store file {path} doesn't exist");

            var fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Store>(fileContent);
        }
    }
}