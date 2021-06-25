using System.IO;
using System.Linq;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;
using Newtonsoft.Json;

namespace GPU_Prices_Parser.Parsers.Files
{
    internal static class FileParser
    {
        public static GpuNote ParseGpuFile(string path) => Parse<GpuNote>(path);

        public static Store[] ParseStoreFiles(string storeDirPath)
        {
            if (!Directory.Exists(storeDirPath))
                throw new IOException("Directory with store data doesn't exist");

            var files = Directory.GetFiles(storeDirPath).Where(file => file.Contains("Citilink")).ToArray();
            var stores = new Store[files.Length];

            for (int i = 0; i < files.Length; i++)
                stores[i] = ParseStoreFile(files[i]);

            return stores;
        }
        
        private static Store ParseStoreFile(string path) => Parse<Store>(path);
        
        private static T Parse<T>(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File {path} doesn't exist");

            var fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}