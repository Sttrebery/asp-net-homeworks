using homework_3.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//Задание 3 (в .http есть тесты)
app.UseKeyCheck();

#region Задание 2
app.Use(async (context, next) =>
{
    Console.WriteLine("First middleware has been started");
    Console.WriteLine($"Request processing started at: {DateTime.Now}:");
    await next(context);
    Console.WriteLine("First middleware is finished");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Second middleware has been started");
    context.Response.Headers.Append("X-Processed-By", "second-middleware");
    await next(context);
    Console.WriteLine("Second middleware is finished");
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("You're reached the endpoint!\n");
});
#endregion

// Задание 1
app.Run(async (context) =>
{
    string response = $"URL path: {context.Request.Path}\nHTTP-method: {context.Request.Method}\nServer time: {DateTime.Now}";
    await context.Response.WriteAsync(response);
});

// Этот код уже не выполняется (Проверено)
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.Run();