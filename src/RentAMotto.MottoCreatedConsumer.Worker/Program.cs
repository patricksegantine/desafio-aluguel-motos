using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.ConfigureServices();

var app = builder.Build();

app.UseSerilogRequestLogging();

await app.RunAsync();