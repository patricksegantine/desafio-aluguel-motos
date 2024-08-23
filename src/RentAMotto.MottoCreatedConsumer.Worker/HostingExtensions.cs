using MassTransit;
using MongoDB.Driver;
using RentAMotto.MottoCreatedConsumer.Worker.Consumers;

namespace Microsoft.Extensions.DependencyInjection;

public static class HostingExtensions
{
    public static IHostBuilder ConfigureHost(this IHostBuilder builder)
    {
        return builder.ConfigureServices((context, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<VehicleCreatedConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(context.Configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint(context.Configuration["RabbitMQ:QueueName"]!, e =>
                    {
                        e.ConfigureConsumer<VehicleCreatedConsumer>(ctx);
                    });
                });
            });

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
                new MongoClient(context.Configuration["MongoDB:ConnectionString"]!));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(context.Configuration["MongoDB:DatabaseName"]!));
        });
    }
}
