using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Winnovative;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the Winnovative library, using its HtmlToPdfConverter class.
    /// The HtmlToPdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class WinnovativeService : IUtilityService
    {
        private readonly IFileService _fileService;
        private readonly HtmlToPdfConverter _htmlToPdfConverter;

        public WinnovativeService(IFileService fileService)
        {
            _fileService = fileService;
            _htmlToPdfConverter = new HtmlToPdfConverter();
        }

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            byte[] pdfBytes = _htmlToPdfConverter.ConvertHtml(htmlContent, null);
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            byte[] pdfBytes = _htmlToPdfConverter.ConvertUrl(urlContent);
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }
    }
}
