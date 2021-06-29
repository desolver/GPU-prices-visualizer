using System.IO;
using System.Linq;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;
using Newtonsoft.Json;

namespace GPU_Prices_Parser.Parsers.Files
{
    internal static class FileParser
    {
        public static GpuNote[] ParseGpuFiles(string path) => ParseFiles<GpuNote>(path);
        
        public static Store[] ParseStoreFiles(string storeDirPath) => ParseFiles<Store>(storeDirPath);

        private static T[] ParseFiles<T>(string path)
        {
            if (!Directory.Exists(path))
                throw new IOException("Directory with store data doesn't exist");
            
            var files = Directory.GetFiles(path).ToArray();
            var results = new T[files.Length];
            
            for (int i = 0; i < files.Length; i++)
                results[i] = Parse<T>(files[i]);

            return results;
        }

        private static T Parse<T>(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File {path} doesn't exist");

            var fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}