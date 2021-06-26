using System;
using System.Windows.Forms;
using GPU_Prices_Parser.Data.Gpu;
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
            var stores = FileParser.ParseStoreFiles(StoreDirPath);
            var webProvider = new WebProvider();
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new GpuForm(GpuModelHelper.GetModelList(), stores,
                new ProductParser[]
                    {new CitilinkProductParser(webProvider), new DnsProductParser(webProvider)}));
        }
    }
}