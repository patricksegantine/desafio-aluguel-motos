using MassTransit;
using MongoDB.Driver;
using RentAMotto.Domain.Events;
using Serilog;

namespace RentAMotto.MottoCreatedConsumer.Worker.Consumers;

public class VehicleCreatedConsumer(IMongoDatabase database) : IConsumer<VehicleCreatedEvent>
{
    private readonly IMongoDatabase _database = database;

    public async Task Consume(ConsumeContext<VehicleCreatedEvent> context)
    {
        var collection = _database.GetCollection<VehicleCreatedEvent>("Mottos");
        await collection.InsertOneAsync(context.Message);

        Log.Information(
            "Received and stored message (make: '{Make}' | model: '{Model}' | numberPlate: '{NumberPlate}' yearOfManufacture: '{YearOfManufacture}')",
            context.Message.Make,
            context.Message.Model,
            context.Message.NumberPlate,
            context.Message.YearOfManufacture);
    }
}
