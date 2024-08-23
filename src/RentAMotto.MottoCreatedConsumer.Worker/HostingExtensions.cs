using MassTransit;
using MongoDB.Driver;
using RentAMotto.Domain.Interfaces;
using RentAMotto.Infrastructure.Persistence.MongoDB;
using RentAMotto.MottoCreatedConsumer.Worker.Consumers;

namespace Microsoft.Extensions.DependencyInjection;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddScoped<IEventStore, EventStore>();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<VehicleCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ");
                cfg.Host(rabbitMqSettings["Host"], h =>
                {
                    h.Username(rabbitMqSettings["Username"]!);
                    h.Password(rabbitMqSettings["Password"]!);
                });

                cfg.ReceiveEndpoint(rabbitMqSettings["QueueName"]!, e =>
                {
                    e.ConfigureConsumer<VehicleCreatedConsumer>(context);
                });
            });
        });

        builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            new MongoClient(builder.Configuration["MongoDB:ConnectionString"]!));

        builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(builder.Configuration["MongoDB:DatabaseName"]!));

        return builder;
    }
}
