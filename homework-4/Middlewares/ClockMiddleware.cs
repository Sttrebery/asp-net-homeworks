using homework_4.Services;

namespace homework_4.Middlewares;

public class ClockMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IClockService clockService)
    {
        await next(context);
        await context.Response.WriteAsync(clockService.GetServerTime());
    }
}


public static class ClockMiddlewareExtension
{
    public static IApplicationBuilder UseClock(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ClockMiddleware>();
    }
}