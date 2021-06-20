using GPU_Prices_Parser.Data;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal interface IProductParser
    {
        Gpu ParseInfo(string url);
    }
}
