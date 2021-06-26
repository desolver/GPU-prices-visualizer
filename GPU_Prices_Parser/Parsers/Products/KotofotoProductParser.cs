using System.Threading.Tasks;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class KotofotoProductParser : ProductParser
    {
        public override StoreName ParseStore { get; }
        protected override string CellSelector { get; }
        protected override string PriceSelector { get; }
        protected override string NameSelector { get; }

        public KotofotoProductParser(WebProvider webProvider) : base(webProvider) { }

        protected override Task<IDocument> GetHtmlDocument(Store store, string url)
        {
            throw new System.NotImplementedException();
        }

        protected override GpuNote ParseCell(GpuModel model, IElement cell)
        {
            throw new System.NotImplementedException();
        }
    }
}
