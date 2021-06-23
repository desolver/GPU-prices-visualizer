using System;
using System.Linq;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class CitilinkProductParser : IProductParser
    {
        public StoreName ParseStore => StoreName.Citilink;
        
        public GpuNote[] ExtractAllInfo(GpuModel model, IDocument document)
        {
            var cellSelector = "div.ProductCardHorizontal";
            var cells = document
                .QuerySelectorAll(cellSelector)
                .Where(element => element.TextContent.Contains(GpuModelHelper.GetRepresentation(model)))
                .ToArray();
            
            var result = new GpuNote[cells.Length];

            var priceSelector = "span.ProductCardHorizontal__price_current-price";
            var nameSelector = "a.ProductCardHorizontal__title";

            for (int i = 0; i < result.Length; i++)
            {
                var element = cells[i];
                var priceCell = element.QuerySelector(priceSelector)?.TextContent;
                var nameCell = element.QuerySelector(nameSelector)?.TextContent;

                result[i] = new GpuNote(new Gpu(model, nameCell, decimal.Parse(priceCell!), StoreName.Citilink), DateTime.Now);
            }

            return result;
        }
    }
}
