using Carter;
using Serilog;
using ToDoAPI;

Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.RegisterServices();
builder.RegisterCustomServices();
builder.RegisterAuthServices();

var app = builder.Build();

app.RegisterMiddlewares();

app.MapCarter();

app.Run();
