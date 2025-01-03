using CleverCore.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/clevercore-{Date}.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Configure services
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the middleware pipeline
app.ConfigurePipeline(app.Environment);

// Seed the database
await app.SeedDatabase();

// Run the app
app.Run();
