using Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AppConsole
{
    class Program
    {
        private const string BaseOutputPath = "HTML-to-PDF";
        private const string FileExtension = ".pdf";

        private static IUtilityService _pdfService;

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            string urlContent = @"https://messagequeue.actorsmartbook.se/Templates/ticket.aspx?orderid=3545624&uid=411ffdec-dcbc-491f-a629-8939d26dd031";

            while (true)
            {
                Console.WriteLine("Select PDF Conversion Service then press enter and wait: ");
                Console.WriteLine("1: EvoPdfService");
                Console.WriteLine("2: ExpertPdfConvertService");
                Console.WriteLine("3: IronPdfService");
                Console.WriteLine("4: NRecoService");
                Console.WriteLine("5: PuppeteerService");
                Console.WriteLine("6: SelectService");
                Console.WriteLine("7: SyncfusionService");
                Console.WriteLine("8: Exit");

                int choice;
                bool validChoice = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 8;
                string serviceIdentifier = string.Empty;

                if (!validChoice)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                if (choice == 9)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        _pdfService = new EvoPdfService(new FileService());
                        serviceIdentifier = "EvoPdf";
                        break;
                    case 2:
                        _pdfService = new ExpertPdfConvertService(new FileService());
                        serviceIdentifier = "ExpertPdf";
                        break;
                    case 3:
                        _pdfService = new IronPdfService(new FileService());
                        serviceIdentifier = "IronPdf";
                        break;
                    case 4:
                        _pdfService = new NRecoService(new FileService());
                        serviceIdentifier = "NReco";
                        break;
                    case 5:
                        _pdfService = new PuppeteerService();
                        serviceIdentifier = "Puppeteer";
                        await ConvertUrlToPdfAsync(urlContent, serviceIdentifier);
                        Console.WriteLine("PDF Conversion done.");
                        break;
                    case 6:
                        _pdfService = new SelectService(new FileService());
                        serviceIdentifier = "Select";
                        break;
                    case 7:
                        //Same as PuppeteerService, assuming SyncfusionService might need async handling
                        _pdfService = new SyncfusionService(new FileService());
                        serviceIdentifier = "Syncfusion";
                        break;
                    case 8:
                        //Same as PuppeteerService, assuming SyncfusionService might need async handling
                        _pdfService = new WinnovativeService(new FileService());
                        serviceIdentifier = "Winnovative";
                        break;
                }

                try
                {
                    // Use the appropriate method based on the service type
                    if (_pdfService is IPuppeteerService)
                    {
                        await ConvertUrlToPdfAsync(urlContent, serviceIdentifier);
                    }
                    else
                    {
                        ConvertUrlToPdf(urlContent, serviceIdentifier);
                    }
                    Console.WriteLine("PDF Conversion done.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during PDF conversion: {ex.Message}");
                }
            }
        }

        private static void ConvertUrlToPdf(string urlContent, string serviceIdentifier)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string outputPath = GenerateOutputFileName($"UrlPDF-{serviceIdentifier}");
            _pdfService.ConvertUrlToPdf(urlContent, outputPath);
            stopwatch.Stop();
            Console.WriteLine($"Time taken for {serviceIdentifier}: {stopwatch.ElapsedMilliseconds} milliseconds");
        }

        private static async Task ConvertUrlToPdfAsync(string urlContent, string serviceIdentifier)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string outputPath = GenerateOutputFileName($"UrlPDF-{serviceIdentifier}");

            if (_pdfService is IPuppeteerService puppeteerService)
            {
                await puppeteerService.ConvertUrlToPdfAsync(urlContent, outputPath);
            }

            stopwatch.Stop();
            Console.WriteLine($"Time taken for {serviceIdentifier}: {stopwatch.ElapsedMilliseconds} milliseconds");
        }

        private static string GenerateOutputFileName(string identifier)
        {
            return $"{BaseOutputPath}-{identifier}{FileExtension}";
        }
    }
}