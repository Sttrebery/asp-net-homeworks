namespace homework_4.Reports;

public class ConsoleReport : IReportService
{
    public string Name { get; } = "ConsoleReport";
    public string CreateReport()
    {
        return $"Report created by {Name} at {DateTime.Now}.";
    }
}