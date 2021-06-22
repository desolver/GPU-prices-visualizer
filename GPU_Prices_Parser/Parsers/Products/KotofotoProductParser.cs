using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class KotofotoProductParser : IProductParser
    {
        public StoreName ParseStore => StoreName.Kotofoto;
        public GpuNote[] ParseAllInfo(GpuModel model, IDocument document)
        {
            throw new System.NotImplementedException();
        }
    }
}
