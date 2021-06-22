using System;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Graph;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class CitilinkProductParser : IProductParser
    {
        public StoreName ParseStore => StoreName.Citilink;
        public GpuNote[] ParseAllInfo(GpuModel model, IDocument document)
        {
            // var cellSelector = "div.ProductCardHorizontal";
            // var cells = document
            //     .QuerySelectorAll(cellSelector)
            //     .Select(m => m.TextContent)
            //     .Where(text => text.Contains(gpuName));
            
            return new []
            {
                new GpuNote(new Gpu(model, "RTX 3060 ti iCHILL", 1522, StoreName.Citilink), DateTime.Today),
                new GpuNote(new Gpu(model, "Asus ROG RTX 3070", 1900, StoreName.Citilink), DateTime.Today)
            };
        }
    }
}
