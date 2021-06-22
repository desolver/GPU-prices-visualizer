using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal interface IProductParser
    {
        StoreName ParseStore { get; }
        GpuNote[] ParseAllInfo(GpuModel model, IDocument document);
    }
}
