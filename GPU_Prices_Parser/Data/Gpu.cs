namespace GPU_Prices_Parser.Data
{
    internal class Gpu : IData
    {
        public string Name { get; }
        public decimal Price { get; }
        public Store CellingStore { get; }
        
        public Gpu(string name, decimal price, Store cellingStore)
        {
            Name = name;
            Price = price;
            CellingStore = cellingStore;
        }
    }
}
