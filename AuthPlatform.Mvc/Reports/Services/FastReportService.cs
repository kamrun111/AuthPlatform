

using FastReport;
using FastReport.Export.PdfSimple;


namespace AuthPlatform.Mvc.Reports.Services
{


    public class FastReportService : IFastReportService
    {
        public byte[] GeneratePdf<T>(
            string reportPath,
            string dataSourceName,
            List<T> data)
        {
            using var report = new Report();

            report.Load(reportPath);

            report.RegisterData(data, dataSourceName);

            report.GetDataSource(dataSourceName).Enabled = true;

            report.Prepare();

            using var pdfExport = new PDFSimpleExport();

            using var stream = new MemoryStream();

            report.Export(pdfExport, stream);

            return stream.ToArray();
        }
    }
}
