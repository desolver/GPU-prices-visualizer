using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GPU_Prices_Parser.Data.Gpu;
using Newtonsoft.Json;

namespace GPU_Prices_Parser
{
    internal class SimilarGpuFilesReader
    {
        public static async Task<IEnumerable<GpuNote>> Find(string serialNumber, string storePath)
        {
            var readTasks = Directory
                .GetDirectories(storePath)
                .Select(Directory.GetFiles)
                .SelectMany(files => { return files.Select(file => File.ReadAllTextAsync(file)); }).ToArray();

            await Task.WhenAll(readTasks);

            return readTasks
                .Select(task => task.Result)
                .Where(fileContent => fileContent.Contains(serialNumber))
                .Select(fileContent => (GpuNote) JsonConvert.DeserializeObject<GpuNote>(fileContent));
        }
    }
}