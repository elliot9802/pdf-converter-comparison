using System;
using System.IO;
using IronPdf.Imaging;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace Services
{
    /// <summary>
    /// Service class responsible for converting HTML content and URLs to PDF format.
    /// This implementation leverages the Syncfusion library, using its HtmlToPdfConverter class.
    /// The HtmlToPdfConverter is initialized with predefined settings suitable for the PDF conversion tasks.
    /// </summary>
    public class SyncfusionService : UtilityService
    {
        private readonly IFileService _fileService;
        private readonly HtmlToPdfConverter _htmlToPdfConverter;
        private readonly WebKitConverterSettings _converterSettings;

        public SyncfusionService(IFileService fileService)
        {
            _fileService = fileService;
            _converterSettings = new WebKitConverterSettings();
            _htmlToPdfConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);
        }

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            PdfDocument pdfDocument = _htmlToPdfConverter.Convert(htmlContent);
            pdfDocument.Save(outputPath);
            pdfDocument.Close(true);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            PdfDocument pdfDocument = _htmlToPdfConverter.Convert(urlContent);
            pdfDocument.Save(outputPath);
            pdfDocument.Close(true);
        }
    }
}
