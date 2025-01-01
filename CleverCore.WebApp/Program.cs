using CleverCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the middleware pipeline
app.ConfigurePipeline(app.Environment);

// Seed the database
await app.SeedDatabase();

// Run the app
app.Run();
