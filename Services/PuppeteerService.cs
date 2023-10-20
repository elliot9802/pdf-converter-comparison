using PuppeteerSharp;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the PuppeteerSharp library.
    /// </summary>
    public class PuppeteerService
    {

        public PuppeteerService( )
        {
            // Ensure PuppeteerSharp's browser binaries are downloaded
            new BrowserFetcher().DownloadAsync().Wait();
        }

        public async Task ConvertHtmlToPdfAsync(string htmlContent, string outputPath)
        {
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetContentAsync(htmlContent);
                await page.PdfAsync(outputPath);
            }
        }

        public async Task ConvertUrlToPdfAsync(string urlContent, string outputPath)
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
