using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using OpenQA.Selenium.Chrome;

namespace GPU_Prices_Parser
{
    internal class WebProvider
    {
        public async Task<IDocument> GetHtmlWithAngleSharp(string url)
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            var document = await context.OpenAsync(url);
            return document;
        }

        public async Task<string> GetHtmlWithSelenium(string url)
        {
            await Task.Run(() => webDriver.Navigate().GoToUrl(url));
            await Task.Delay(2000);
            var document = webDriver.PageSource;
            return document;
        }

        private ChromeDriver webDriver;
        
        public void OpenBrowser() => webDriver = new ChromeDriver();
        public void CloseBrowser() => webDriver.Quit();
    }
}