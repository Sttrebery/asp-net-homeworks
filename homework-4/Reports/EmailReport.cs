namespace homework_4.Reports;

public class EmailReport : IReportService
{
    public string Name { get; } =  "EmailReport";
    public string CreateReport()
    {
        return "Report created successfully and sent to your email.";
    }
}