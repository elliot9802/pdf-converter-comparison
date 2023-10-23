using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace Services
{
    public class SyncfusionService : IUtilityService
    {
        private readonly IFileService _fileService;
        private readonly HtmlToPdfConverter _htmlToPdfConverter;

        public SyncfusionService(IFileService fileService)
        {
            _fileService = fileService;
            _htmlToPdfConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);
        }

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            PdfDocument pdfDocument = _htmlToPdfConverter.Convert(htmlContent);
            using (FileStream stream = new FileStream(outputPath, FileMode.Create))
            {
                pdfDocument.Save(stream);
            }
            pdfDocument.Close(true);
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            PdfDocument pdfDocument = _htmlToPdfConverter.Convert(urlContent);
            using (FileStream stream = new FileStream(outputPath, FileMode.Create))
            {
                pdfDocument.Save(stream);
            }
            pdfDocument.Close(true);
        }
    }
}

