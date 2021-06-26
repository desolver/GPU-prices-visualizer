using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using OpenQA.Selenium;
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
            using var webDriver = new ChromeDriver();
            await Task.Run(() => webDriver.Navigate().GoToUrl(url));
            var document = webDriver.PageSource;
            webDriver.Quit();
            return document;
        }
    }
}