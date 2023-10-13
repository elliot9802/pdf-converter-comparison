using System;
using System.IO;
using NReco.PdfGenerator;

namespace Services
{
    public class NRecoConvertService : IPdfConvertService
    {
        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            var htmlToPdf = CreateHtmlToPdfConverter();

            try
            {
                var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
                File.WriteAllBytes(outputPath, pdfBytes);
            }
            catch (Exception)
            {
                // Throw the exception so it can be caught and logged/handled at the caller.
                throw;
            }
        }

        public void ConvertUrlToPdf(string urlContent, string outputPath)
        {
            var htmlToPdf = CreateHtmlToPdfConverter();

            try
            {
                htmlToPdf.GeneratePdfFromFile(urlContent, null, outputPath);
            }
            catch (Exception)
            {
                // Throw the exception so it can be caught and logged/handled at the caller.
                throw;
            }
        }

        private HtmlToPdfConverter CreateHtmlToPdfConverter()
        {
            return new HtmlToPdfConverter();
            // Add specific configurations to set for every instance of HtmlToPdfConverter
            // For instance:
            // converter.PageFooterHtml = null;
            // return converter;
        }
    }
}
