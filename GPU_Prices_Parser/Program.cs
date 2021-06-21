using System;
using System.IO;
using System.Windows.Forms;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Parsers.Files;

namespace GPU_Prices_Parser
{
    internal static class Program
    {
        private const string StoreDirPath = "Input"; 
        private const string GpuDirPath = "GpuData"; 
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var stores = ParseStoreFiles();
            ParseGpuFiles();
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Form1());
        }

        private static Store[] ParseStoreFiles()
        {
            if (!Directory.Exists(StoreDirPath))
                throw new IOException("Directory with store data doesn't exist");

            var files = Directory.GetFiles(StoreDirPath);
            var stores = new Store[files.Length];

            for (int i = 0; i < files.Length; i++)
                stores[i] = FileParser.ParseStoreFile(files[i]);

            return stores;
        }
        
        private static void ParseGpuFiles()
        {
            if (!Directory.Exists(GpuDirPath))
                return;
            
            var files = Directory.GetFiles(StoreDirPath);
            if (files.Length == 0) return;
        }
    }
}
