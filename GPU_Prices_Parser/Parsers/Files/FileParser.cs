using System.IO;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;
using Newtonsoft.Json;

namespace GPU_Prices_Parser.Parsers.Files
{
    internal static class FileParser
    {
        public static GpuNote ParseGpuFile(string path) => Parse<GpuNote>(path);

        public static Store ParseStoreFile(string path) => Parse<Store>(path);

        private static T Parse<T>(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File {path} doesn't exist");

            var fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}