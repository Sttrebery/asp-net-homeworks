namespace homework_4.Middlewares;

public static class CustomBranchesMiddleware
{
    public static IApplicationBuilder UseCustomBranches(this IApplicationBuilder builder)
    {
        builder.UseWhen(
            context => context.Request.Path.StartsWithSegments("/api"),
            apiBranch =>
            {
                apiBranch.Use(async (context, next) =>
                {
                    context.Response.Headers.Append("X-Branch", "UseWhen");
                    await next(context);
                });
            }
        );

        builder.MapWhen(
            context => context.Request.Query["debug"] == "true",
            apiBranch =>
            {
                apiBranch.Run(async (context) =>
                {
                    context.Response.Headers.AppendCommaSeparatedValues("X-Branch", "MapWhen");
                    await context.Response.WriteAsync("It's a Debug Page");
                });
            }
        );
        
        //основная ветвь
        builder.Run(async (context) =>
        {
            await context.Response.WriteAsync("Main Page");
        });
        
        return builder;
    }
}