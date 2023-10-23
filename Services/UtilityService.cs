using PuppeteerSharp;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Defines functionalities for PDF conversion.
    /// </summary>
    public interface IUtilityService
    {
        void ConvertHtmlToPdf(string htmlContent, string outputPath);
        void ConvertUrlToPdf(string urlContent, string outputPath);

        //Task ConvertHtmlToPdfAsync(string htmlContent, string outputPath);

        //Task ConvertUrlToPdfAsync(string urlContent, string outputPath);
    }
}