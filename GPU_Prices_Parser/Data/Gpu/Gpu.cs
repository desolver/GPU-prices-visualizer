namespace GPU_Prices_Parser.Data.Gpu
{
    internal class Gpu
    {
        public GpuModel Model;
        public string FullName { get; }
        public string SerialNumber { get; }
        public decimal Price { get; }
        public StoreName CellingStore { get; }
        
        public Gpu(GpuModel model, string fullName, string serialNumber, decimal price, StoreName cellingStore)
        {
            FullName = fullName;
            Price = price;
            CellingStore = cellingStore;
            SerialNumber = serialNumber;
            Model = model;
        }
    }
}
