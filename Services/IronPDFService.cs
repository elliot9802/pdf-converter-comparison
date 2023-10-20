using System;
using System.IO;
using Microsoft.Extensions.Logging;
using IronPdf;
using System.Net.Http;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the ExpertPdf library, specifically using its PdfConverter class.
    /// The PdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class IronPdfService : UtilityService
    {
        private readonly IFileService _fileService;
        private readonly ChromePdfRenderer _pdfRenderer;

        public IronPdfService(IFileService fileService)
        {
            _fileService = fileService;
            _pdfRenderer = CreatePdfConverter();
        }
        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            var pdfDocument = _pdfRenderer.RenderHtmlAsPdf(htmlContent);
            byte[] pdfBytes = pdfDocument.BinaryData;
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            var pdfDocument = _pdfRenderer.RenderUrlAsPdf(urlContent);
            byte[] pdfBytes = pdfDocument.BinaryData;
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        /// <summary>
        /// Creates an instance of PdfConverter with predefined settings.
        /// </summary>
        /// <returns>Configured instance of PdfConverter.</returns>
        private ChromePdfRenderer CreatePdfConverter()
        {
            ChromePdfRenderer pdfConverter = new ChromePdfRenderer{};

            pdfConverter.RenderingOptions.EnableJavaScript = true;

            return pdfConverter;
        }
    }
}