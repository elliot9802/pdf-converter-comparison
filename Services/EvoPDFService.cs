using EvoPdf;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the ExpertPdf library, specifically using its PdfConverter class.
    /// The PdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class EvoPdfService : IUtilityService
    {
        private readonly IFileService _fileService;
        private readonly PdfConverter _pdfConverter;

        public EvoPdfService(IFileService fileService)
        {
            _fileService = fileService;
            _pdfConverter = CreatePdfConverter();
        }
        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            byte[] pdfBytes = _pdfConverter.GetPdfBytesFromHtmlString(htmlContent);
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            byte[] pdfBytes = _pdfConverter.GetPdfBytesFromUrl(urlContent);
            _fileService.WriteAllBytes(outputPath, pdfBytes);
        }

        /// <summary>
        /// Creates an instance of PdfConverter with predefined settings.
        /// </summary>
        /// <returns>Configured instance of PdfConverter.</returns>
        private PdfConverter CreatePdfConverter()
        {
            PdfConverter pdfConverter = new PdfConverter
            {
                PdfDocumentOptions =
                {
                    PdfPageSize = PdfPageSize.A4,
                    PdfCompressionLevel = PdfCompressionLevel.Normal,
                    ShowHeader = false,
                    ShowFooter = false
                }
            };

            return pdfConverter;
        }
    }
}