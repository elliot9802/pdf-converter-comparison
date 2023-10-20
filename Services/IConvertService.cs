using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Defines functionalities related to PDF conversion operations.
    /// </summary>
    public interface IConvertService
    {
        bool IsValidUrl(string url);

        byte[] ConvertHtmlContentToPdfBytes(string htmlContent);
        byte[] ConvertUrlToPdfBytes(string url);
    }
}