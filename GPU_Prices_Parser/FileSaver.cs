using System.IO;
using System.Threading.Tasks;

namespace GPU_Prices_Parser
{
    internal static class FileSaver
    {
        public static async Task Save(string content, string directoryPath, string fileName)
        {
            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);

            await File.WriteAllTextAsync(Path.Combine(directoryPath, fileName), content);
        }
    }
}