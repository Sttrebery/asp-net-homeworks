namespace homework_3.Middlewares;

public static class KeyCheckMiddlewareExtension
{
    public static IApplicationBuilder UseKeyCheck(this IApplicationBuilder app)
    {
        return app.UseMiddleware<KeyCheckMiddleware>();
    }
}