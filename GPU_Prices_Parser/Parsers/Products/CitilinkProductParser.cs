using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class CitilinkProductParser : ProductParser
    {
        public override StoreName ParseStore => StoreName.Citilink;
        protected override string CellSelector => "div.ProductCardHorizontal";
        protected override string PriceSelector => "span.ProductCardHorizontal__price_current-price";
        protected override string NameSelector => "a.ProductCardHorizontal__title";

        public CitilinkProductParser(WebProvider webProvider) : base(webProvider) { }

        protected override async Task<IDocument> GetHtmlDocument(Store store, string url) =>
            await WebProvider.GetHtmlWithAngleSharp(url);

        protected override GpuNote ParseCell(GpuModel model, IElement cell)
        {
            var price = cell.QuerySelector(PriceSelector)?.TextContent;
            var fullName = cell.QuerySelector(NameSelector)?.TextContent;
            var nameAndSNumber = fullName!.Split(',');
            var name = nameAndSNumber[0].TrimEnd(' ');
            var serialNumber = nameAndSNumber[1].TrimStart(' ');

            return new GpuNote(new Gpu(model, name, fullName, serialNumber,
                decimal.Parse(price ?? "0"), ParseStore), DateTime.Now);
        }
    }
}