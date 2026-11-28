using homework_5.Configurations;
using homework_5.Services;
using Microsoft.Extensions.Options;

namespace homework_5.Routes;

public static class MapConfigurationMiddleware
{
    public static void MapConfiguration(this IEndpointRouteBuilder app)
    {
        
        // Задание 1
        app.MapGet("/api/settings", async (HttpContext context, IConfiguration config) =>
        {
            var section = config.GetSection("Application");
    
            await context.Response.WriteAsJsonAsync(
                new
                {
                    //по ключу
                    Name=config["Application:Name"], 
                    Version=config["Application:Version"],
                    LastUpdate=config["Application:LastUpdate"],
            
                    //с помощью секции
                    MaxItems = section.GetValue<int>("MaxItems"),
                    ConnectionTimeout = section.GetValue<int>("ConnectionTimeout"),
                    IsAuthorizationRequired = section.GetValue<bool>("IsAuthorizationRequired"),
                });
        });

        //Задание 2
        //считывает конфигурацию 1 раз и кэширует значение (можно закомментировать)
        app.MapGet("/api/application-info",(HttpContext context, IOptions<ApplicationOptions> appOptions, ApplicationInfoService appInfoService) =>
        {
            var appOptionsValue = appOptions.Value;
            if (string.IsNullOrEmpty(appOptionsValue.Name.Trim()) || string.IsNullOrEmpty(appOptionsValue.Version.Trim()) ||
                appOptionsValue.MaxItems <= 0)
            {
                return Results.InternalServerError("Incorrect app configuration");
            }
    
            return Results.Ok(appInfoService.GetApplicationInfo());
        });

        // // сохраняет конфигурацию при создании каждого запроса (scope-время жизни)
        // app.MapGet("/api/application-info",(HttpContext context, IOptionsSnapshot<ApplicationOptions> appOptions, ApplicationInfoService appInfoService) =>
        // {
        //     var appOptionsValue = appOptions.Value;
        //     if (string.IsNullOrEmpty(appOptionsValue.Name.Trim()) || string.IsNullOrEmpty(appOptionsValue.Version.Trim()) ||
        //         appOptionsValue.MaxItems <= 0)
        //     {
        //         return Results.InternalServerError("Incorrect app configuration");
        //     }
        //     
        //     return Results.Ok(appInfoService.GetApplicationInfo());
        // });

        // // IOptionsMonitor отслеживает изменения фоном, но там надо через OnChange обрабатывать
        
        
        // Задание 3 
        //доступ через IConfiguration
        app.MapGet("/api/yaml-settings", async (HttpContext context, IConfiguration config, IHostEnvironment env) =>
        {
            //обработка ошибков
            try
            {
                var fullPath = Path.Combine(env.ContentRootPath, "appsettings.yaml");

                if (!File.Exists(fullPath))
                {
                    await Results.NotFound("Файл конфигурации 'appsettings.yaml' не найден.").ExecuteAsync(context);
                    
                    return;
                }
                
                var section = config.GetSection("Application");
                var features = section.GetSection("Features");

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        // секция Applicatiom
                        Name = config["Application:Name"],
                        Version = config["Application:Version"],
                        MaxItems = section.GetValue<int?>("MaxItems"),

                        // секция Features
                        IsHttpsEnabled = config.GetValue<bool?>("Application:Features:IsHttpsEnabled"),
                        IsRazorPagesEnabled = section.GetValue<bool?>("Features:IsRazorPagesEnabled"),
                        IsMappingEnabled = features.GetValue<bool?>("IsMappingEnabled"),
                    });
            }
            catch (Exception e)
            {
                await Results.InternalServerError(e.Message).ExecuteAsync(context);
            }
            
        });

        // доступ через IOptions
        app.MapGet("/api/yaml-application-info",(HttpContext context, IOptionsSnapshot<ApplicationOptions> appOptions, ApplicationInfoService appInfoService) =>
        {
            var appOptionsValue = appOptions.Value;
            if (string.IsNullOrEmpty(appOptionsValue.Name.Trim()) || string.IsNullOrEmpty(appOptionsValue.Version.Trim()) ||
                appOptionsValue.MaxItems <= 0)
            {
                return Results.InternalServerError("Incorrect app configuration");
            }
            
            return Results.Ok(appInfoService.GetApplicationInfo());
        });
    }
}