using System.IO;
using System.Threading.Tasks;
using GPU_Prices_Parser.Data.Gpu;
using Newtonsoft.Json;

namespace GPU_Prices_Parser
{
    internal static class FileSaver
    {
        public static void SaveGpuInfo(GpuNote[] gpuInfo)
        {
            Parallel.ForEach(gpuInfo, async info =>
            {
                var saveFolderPath = Path.Combine(Program.GpuDirPath,
                    GpuModelHelper.GetRepresentation(info.Gpu.Model), info.Gpu.CellingStore.ToString(),
                    info.DateStamp.ToShortDateString());
                await SaveDataAsync(info, saveFolderPath, info.ToString());
            });
        }
        
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