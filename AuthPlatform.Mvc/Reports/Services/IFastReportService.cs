namespace  AuthPlatform.Mvc.Reports.Services
{

    public interface IFastReportService
    {
        byte[] GeneratePdf<T>(
            string reportPath,
            string dataSourceName,
            List<T> data);
    }
}
