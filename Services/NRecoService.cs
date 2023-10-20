using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NReco.PdfGenerator;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the ExpertPdf library, specifically using its PdfConverter class.
    /// The PdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class NRecoService : IUtilityService
    {
        private readonly IFileService _fileService;
        private readonly HtmlToPdfConverter _pdfConverter;

        public NRecoService(IFileService fileService)
        {
            _fileService = fileService;
            _pdfConverter = CreatePdfConverter();
        }
        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            byte[] pdfBytes = _pdfConverter.GeneratePdf(htmlContent);
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            _pdfConverter.GeneratePdfFromFile(urlContent, null, outputPath);
            // The PDF is already written to outputPath, no need to write again
        }

        /// <summary>
        /// Creates an instance of PdfConverter with predefined settings.
        /// </summary>
        /// <returns>Configured instance of PdfConverter.</returns>
        private HtmlToPdfConverter CreatePdfConverter()
        {
            HtmlToPdfConverter pdfConverter = new HtmlToPdfConverter
            {
                Orientation = PageOrientation.Portrait,
                Size = PageSize.A4
            };

            return pdfConverter;
        }
    }
}
