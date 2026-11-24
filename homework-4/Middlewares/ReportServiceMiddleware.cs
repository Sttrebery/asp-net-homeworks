using homework_4.Reports;

namespace homework_4.Middlewares;

public class ReportServiceMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var type = context.Request.Query["type"].ToString().ToLowerInvariant();

        if (!string.IsNullOrWhiteSpace(type))
        {
            var service = serviceProvider.GetKeyedService<IReportService>(type);
            if (service is not null)
            {
                context.Items["ReportServiceType"] = type;
            }
        }

        await next(context);
    }
}

public static class ReportServiceMiddlewareExtension
{
    public static IApplicationBuilder UseReports(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ReportServiceMiddleware>();
    }
}