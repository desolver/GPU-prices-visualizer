namespace GPU_Prices_Parser.Data
{
    internal class Store : IData
    {
        public string Name { get; }
        public string Url { get; }
        
        public Store(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}