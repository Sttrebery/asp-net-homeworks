using homework_4.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Задание 1
app.UseCustomBranches();

app.Run();