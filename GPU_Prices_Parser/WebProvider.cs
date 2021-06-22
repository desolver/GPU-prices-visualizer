using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;

namespace GPU_Prices_Parser
{
    internal class WebProvider
    {
        public event DownloadComplete DownloadCompleted;
        public delegate void DownloadComplete(Store store, IDocument document);

        public async Task SendRequest(string url, Store store)
        {
            //var client = new WebClient();
            //client.DownloadStringAsync(new Uri(url));
            //client.DownloadStringCompleted += (sender, args) => DownloadCompleted?.Invoke(store, args.Result);
            
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            var document = await context.OpenAsync(url);
            DownloadCompleted?.Invoke(store, document);
        }
    }
}