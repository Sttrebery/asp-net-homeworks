using homework_5.Configurations;
using homework_5.Routes;
using homework_5.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddYamlFile("appsettings.yaml", optional: true);
// builder.Configuration.AddYamlFile("appsettings.yaml");

builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("Application"));
builder.Services.AddScoped<ApplicationInfoService>();   //Внутри передается IOptionsSnapshot

var app = builder.Build();

app.MapConfiguration();

app.Run();