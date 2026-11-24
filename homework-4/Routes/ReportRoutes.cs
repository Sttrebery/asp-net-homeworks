using homework_4.Reports;

namespace homework_4.Routes;

public static class ReportRoutes
{
    public static void MapReports(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map("/reports/request", async (context) =>
        {
            var serviceType = context.Items["ReportServiceType"]?.ToString();
            if (string.IsNullOrWhiteSpace(serviceType))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var service = context.RequestServices.GetKeyedService<IReportService>(serviceType);
            if (service is null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Service '{serviceType}' not found");
                return;
            }
            
            await context.Response.WriteAsJsonAsync(new {Name=service.Name, Report=service.CreateReport()});
        });

        endpoints.Map("/reports/provider", async (HttpContext context, IServiceProvider serviceProvider) =>
        {
            var serviceType = context.Items["ReportServiceType"]?.ToString();
            if (string.IsNullOrWhiteSpace(serviceType))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var service = serviceProvider.GetKeyedService<IReportService>(serviceType);
            
            if (service is null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Service '{serviceType}' not found");
                return;
            }
            
            await context.Response.WriteAsJsonAsync(new {Name=service.Name, Report=service.CreateReport()});
        });
    }
}