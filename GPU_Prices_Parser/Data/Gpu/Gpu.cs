namespace GPU_Prices_Parser.Data.Gpu
{
    internal class Gpu
    {
        public GpuModel Model { get; }
        public string Name { get; }
        public string FullName { get; }
        public string SerialNumber { get; }
        public decimal Price { get; }
        public StoreName CellingStore { get; }
        
        public Gpu(GpuModel model, string name, string fullName, string serialNumber, decimal price, StoreName cellingStore)
        {
            FullName = fullName;
            Price = price;
            CellingStore = cellingStore;
            Name = name;
            SerialNumber = serialNumber;
            Model = model;
        }
    }
}
