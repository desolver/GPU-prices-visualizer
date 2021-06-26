using System;
using System.Text;
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
        protected override string NameSelector => "a.catalog-product__name";

        private int requestCount;
        
        public DnsProductParser(WebProvider webProvider) : base(webProvider) { }

        protected override async Task<IDocument> GetHtmlDocument(Store store, string url)
        {
            if (requestCount == 0) WebProvider.OpenBrowser();
            
            var html = await WebProvider.GetHtmlWithSelenium(url);
            requestCount++;

            if (requestCount == store.Urls.Length)
            {
                requestCount = 0;
                WebProvider.CloseBrowser();
            }
            
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            return await context.OpenAsync(request => request.Content(html));
        }

        protected override GpuNote ParseCell(GpuModel model, IElement cell)
        {
            var priceCell = cell.QuerySelector(PriceSelector)?.TextContent;
            priceCell = priceCell!.Substring(0, priceCell.Length - 2);
            
            var nameCell = cell.QuerySelector(NameSelector)?.TextContent;
            var serialNumber = GetSerialNumber(nameCell);

            return new GpuNote(new Gpu(model, nameCell, serialNumber,
                decimal.Parse(priceCell!), ParseStore), DateTime.Now);
        }

        private static string GetSerialNumber(string header)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < header.Length; i++)
            {
                if (header[i] != '[') continue;
                
                i++;
                while (header[i] != ']')
                {
                    stringBuilder.Append(header[i]);
                    i++;
                }

                break;
            }

            return stringBuilder.ToString();
        }
    }
}