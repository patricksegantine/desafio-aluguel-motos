using Microsoft.DependencyInjection.Extensions;
using RentAMotto.Common.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

await app.ApplyMigrationsAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentAMotto.Admin.Api V1"));
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
