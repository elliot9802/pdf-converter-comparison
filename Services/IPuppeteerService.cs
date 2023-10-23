using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPuppeteerService
    {
        Task ConvertHtmlToPdfAsync(string htmlContent, string outputPath);
        Task ConvertUrlToPdfAsync(string urlContent, string outputPath);
    }
}
