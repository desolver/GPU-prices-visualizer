using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class KotofotoProductParser : IProductParser
    {
        public StoreName ParseStore => StoreName.Kotofoto;
        public GpuNote[] ExtractAllInfo(GpuModel model, IDocument document)
        {
            throw new System.NotImplementedException();
        }
    }
}
