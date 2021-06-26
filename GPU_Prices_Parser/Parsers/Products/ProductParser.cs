using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser.Parsers.Products
{
    internal abstract class ProductParser
    {
        public abstract StoreName ParseStore { get; }
        
        protected abstract string CellSelector { get; }
        protected abstract string PriceSelector { get; }
        protected abstract string NameSelector { get; }
        
        protected readonly WebProvider WebProvider;
        
        protected ProductParser(WebProvider webProvider) => WebProvider = webProvider;
        
        protected abstract Task<IDocument> GetHtmlDocument(string url);
        protected abstract GpuNote ParseCell(GpuModel model, IElement cell);
        
        public async Task<GpuNote[]> ExtractAllInfo(GpuModel model, Store store)
        {
            if (store.Name != ParseStore)
                throw new ArgumentException($"{ParseStore} parser not compatible with {store} store.");

            var storeGpu = new List<GpuNote>();
            foreach (var url in store.Urls)
            {
                var document = await GetHtmlDocument(url);
                
                var cellSelector = CellSelector;
                var cells = document
                    .QuerySelectorAll(cellSelector)
                    .Where(element => element.TextContent.Contains(GpuModelHelper.GetRepresentation(model)))
                    .ToArray();
            
                var result = new GpuNote[cells.Length];
                for (int i = 0; i < result.Length; i++) 
                    result[i] = ParseCell(model, cells[i]);

                storeGpu.AddRange(result);
            }
            
            return storeGpu.ToArray();
        }
    }
}