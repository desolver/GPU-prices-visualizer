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
            var price = cell.QuerySelector(PriceSelector)?.TextContent;
            price = price!.Substring(0, price.Length - 2);
            
            var fullname = cell.QuerySelector(NameSelector)?.TextContent;
            var pair = GetNameAndSerialNumber(fullname);

            return new GpuNote(new Gpu(model, pair.Item1, fullname, pair.Item2,
                decimal.Parse(price!), ParseStore), DateTime.Now);
        }

        private static (string, string) GetNameAndSerialNumber(string header)
        {
            var nameStringBuilder = new StringBuilder();
            var numberStringBuilder = new StringBuilder();
            for (int i = 0; i < header.Length; i++)
            {
                if (header[i] != '[')
                {
                    nameStringBuilder.Append(header[i]);
                    continue;
                }
                
                i++;
                while (header[i] != ']')
                {
                    numberStringBuilder.Append(header[i]);
                    i++;
                }

                break;
            }

            return (nameStringBuilder.ToString(), numberStringBuilder.ToString());
        }
    }
}