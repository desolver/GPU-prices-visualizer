using System;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal class DnsProductParser : ProductParser
    {
        public override StoreName ParseStore => StoreName.DNS;
        protected override string CellSelector => "div.catalog-product";
        protected override string PriceSelector => "div.product-buy__price";
        protected override string NameSelector => "a.catalog-product__name.span";

        public DnsProductParser(WebProvider webProvider) : base(webProvider)
        {
        }

        protected override async Task<IDocument> GetHtmlDocument(string url)
        {
            var html = await WebProvider.GetHtmlWithSelenium(url);
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            return await context.OpenAsync(request => request.Content(html));
        }

        protected override GpuNote ParseCell(GpuModel model, IElement cell)
        {
            var priceSelector = PriceSelector;
            var nameSelector = NameSelector;

            var priceCell = cell.QuerySelector(priceSelector)?.TextContent;
            var nameCell = cell.QuerySelector(nameSelector)?.TextContent;
            var serialNumber = nameCell!.Split(',')[1].TrimStart(' ');

            return new GpuNote(new Gpu(model, nameCell, serialNumber,
                decimal.Parse(priceCell!), ParseStore), DateTime.Now);
        }
    }
}