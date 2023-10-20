using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using SelectPdf;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the ExpertPdf library, specifically using its PdfConverter class.
    /// The PdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class SelectService : IUtilityService
    {
        private readonly IFileService _fileService;
        private readonly HtmlToPdf _pdfConverter;

        public SelectService(IFileService fileService)
        {
            _fileService = fileService;
            _pdfConverter = CreatePdfConverter();
        }
        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            PdfDocument pdfDocument = _pdfConverter.ConvertHtmlString(htmlContent);
            byte[] pdfBytes = pdfDocument.Save();
            _fileService.WriteAllBytes(outputPath, pdfBytes);
            pdfDocument.Close();
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            PdfDocument pdfDocument = _pdfConverter.ConvertUrl(urlContent);
            byte[] pdfBytes = pdfDocument.Save();
            _fileService.WriteAllBytes(outputPath, pdfBytes);
            pdfDocument.Close();
        }

        /// <summary>
        /// Creates an instance of PdfConverter with predefined settings.
        /// </summary>
        /// <returns>Configured instance of PdfConverter.</returns>
        private HtmlToPdf CreatePdfConverter()
        {
            HtmlToPdf pdfConverter = new HtmlToPdf
            {
                Options =
                {
                    PdfPageSize = PdfPageSize.A4,
                    PdfPageOrientation = PdfPageOrientation.Portrait,
                    PdfCompressionLevel = PdfCompressionLevel.Normal,
                }
            };

            return pdfConverter;
        }
    }
}