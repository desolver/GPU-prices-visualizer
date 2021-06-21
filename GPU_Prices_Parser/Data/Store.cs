namespace GPU_Prices_Parser.Data
{
    internal class Store : IData
    {
        public string Name { get; }
        public string[] Urls { get; }
        
        public Store(string name, string[] urls)
        {
            Name = name;
            Urls = urls;
        }
    }
}