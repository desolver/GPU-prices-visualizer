using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPU_Prices_Parser
{
    internal static class FileSaver
    {
        public static async Task SaveDataAsync<T>(T data, string directoryPath, string fileName)
        {
            var content = JsonConvert.SerializeObject(data);
            await SaveFileAsync(content, directoryPath, fileName);
        }
        
        private static async Task SaveFileAsync(string content, string directoryPath, string fileName)
        {
            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);

            await File.WriteAllTextAsync(Path.Combine(directoryPath, fileName), content);
        }
    }
}