using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;
using GPU_Prices_Parser.Parsers.Files;
using GPU_Prices_Parser.Parsers.Products;

namespace GPU_Prices_Parser
{
    internal static class Program
    {
        public const string StoreDirPath = "Input";
        public const string GpuDirPath = "GpuData";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var stores = ParseStoreFiles();
            if (TryParseGpuFiles(out var gpuNotes))
            {
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new GpuForm(stores,
                new IProductParser[]
                    {new CitilinkProductParser(), new DnsProductParser(), new KotofotoProductParser()}, new Filter()));
        }

        private static Store[] ParseStoreFiles()
        {
            if (!Directory.Exists(StoreDirPath))
                throw new IOException("Directory with store data doesn't exist");

            var files = Directory.GetFiles(StoreDirPath).Where(file => file.Contains("Citilink")).ToArray();
            var stores = new Store[files.Length];

            for (int i = 0; i < files.Length; i++)
                stores[i] = FileParser.ParseStoreFile(files[i]);

            return stores;
        }

        private static bool TryParseGpuFiles(out GpuNote[] notes)
        {
            notes = null;
            if (!Directory.Exists(GpuDirPath))
                return false;

            var files = Directory.GetFiles(StoreDirPath);
            if (files.Length == 0) return false;
            
            notes = new GpuNote[files.Length];
            for (int i = 0; i < files.Length; i++) 
                notes[i] = FileParser.ParseGpuFile(files[i]);
            
            return true;
        }
    }
}