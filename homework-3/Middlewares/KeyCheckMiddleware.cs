namespace homework_3.Middlewares;

public class KeyCheckMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var keyValue = context.Request.Query["key"];

        if (string.IsNullOrEmpty(keyValue))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("400: Key is missing");
            return;
        }
        
        await next(context);
    }
}