namespace GPU_Prices_Parser.Data.Gpu
{
    internal class Gpu
    {
        public GpuModel Model;
        public string FullName { get; }
        public decimal Price { get; }
        public StoreName CellingStore { get; }
        
        public Gpu(GpuModel model, string fullName, decimal price, StoreName cellingStore)
        {
            FullName = fullName;
            Price = price;
            CellingStore = cellingStore;
            Model = model;
        }
    }
}
