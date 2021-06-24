using System;

namespace GPU_Prices_Parser.Data
{
    internal class Store
    {
        public StoreName Name { get; }
        public string[] Urls { get; }
        
        public Store(StoreName name, string[] urls)
        {
            Name = name;
            Urls = urls;
        }

        public override string ToString() => Name.ToString();

        public override bool Equals(object other)
        {
            if (!(other is Store otherStore))
                return false;

            return Name == otherStore.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Name, Urls);
        }
    }
}