namespace homework_4.Reports;

public interface IReportService
{
    string Name { get; }
    string CreateReport();
}