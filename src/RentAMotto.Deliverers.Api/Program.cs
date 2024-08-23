using RentAMotto.Common.Api.Middlewares;
using RentAMotto.Deliverers.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentAMotto.Deliverers.Api V1"));
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
