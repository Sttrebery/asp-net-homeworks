var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

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
            ConnectionTimeout = section.GetValue<int>("ConnectionTimeout"),
            IsAuthorizationRequired = section.GetValue<bool>("IsAuthorizationRequired"),
        });
});

app.Run();