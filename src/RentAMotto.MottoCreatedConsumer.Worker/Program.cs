using MassTransit;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Starting up the service");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureHost()
        .Build();

    var busControl = host.Services.GetRequiredService<IBusControl>();

    await busControl.StartAsync();

    Log.Information("Bus started. Press any key to exit...");

    await Task.Run(() => Console.ReadLine());

    await busControl.StopAsync();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a problem starting the service");
}
finally
{
    await Log.CloseAndFlushAsync();
}
