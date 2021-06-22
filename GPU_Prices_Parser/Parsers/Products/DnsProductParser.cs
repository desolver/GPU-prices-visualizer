using System.Linq;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class DnsProductParser : IProductParser
    {
        public StoreName ParseStore => StoreName.DNS;
        public GpuNote[] ParseAllInfo(GpuModel model, IDocument document)
        {
            var cellSelector = "div";
            var cells = document.QuerySelectorAll(cellSelector);
            var count = cells.Length;
            var titles = cells.Select(m => m.TextContent);

            return null;
        }
    }
}
