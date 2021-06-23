using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal interface IProductParser
    {
        StoreName ParseStore { get; }
        GpuNote[] ExtractAllInfo(GpuModel model, IDocument document);
    }
}
