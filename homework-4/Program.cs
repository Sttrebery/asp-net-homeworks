using homework_4.Middlewares;
using homework_4.Repositories;
using homework_4.Routes;
using homework_4.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClockService, ClockService>();
builder.Services.AddScoped<IUserRepository, FileUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IValidatorService, UserValidatorService>();

var app = builder.Build();

//Задание 2
// app.UseClock(); //можно заккоментить
app.UseMiddleware<LifetimeMiddleware>();
app.MapUsers();
app.MapServiceIdentifiers();


// Задание 1
// app.UseCustomBranches(); //можно заккоментить

app.Run();