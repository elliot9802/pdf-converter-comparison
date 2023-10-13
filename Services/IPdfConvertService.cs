using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPdfConvertService
    {
        void ConvertHtmlToPdf(string htmlContent, string outputPath);
        void ConvertUrlToPdf(string urlContent, string outputPath);
    }
}
