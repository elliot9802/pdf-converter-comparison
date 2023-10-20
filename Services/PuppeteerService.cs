using PuppeteerSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the PuppeteerSharp library.
    /// </summary>
    public class PuppeteerService : IUtilityService
    {

        public PuppeteerService()
        {
            // Ensure PuppeteerSharp's browser binaries are downloaded
            new BrowserFetcher().DownloadAsync().Wait();
        }

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            ConvertHtmlToPdfAsync(htmlContent, outputPath).Wait();
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            ConvertUrlToPdfAsync(urlContent, outputPath).Wait();
        }

        private async Task ConvertHtmlToPdfAsync(string htmlContent, string outputPath)
        {
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetContentAsync(htmlContent);
                await page.PdfAsync(outputPath);
            }
        }

        private async Task ConvertUrlToPdfAsync(string urlContent, string outputPath)
        {
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                await page.GoToAsync(urlContent);
                await page.PdfAsync(outputPath);
            }
        }
    }
}
