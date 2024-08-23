using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RentAMotto.Domain.Events;
using RentAMotto.Domain.Interfaces;

namespace RentAMotto.Infrastructure.Persistence.MongoDB;

public class EventStore : IEventStore
{
    private readonly IMongoCollection<VehicleCreatedEvent> _collection;

    public EventStore(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        _collection = database.GetCollection<VehicleCreatedEvent>(nameof(VehicleCreatedEvent));
    }

    public async Task SaveEventAsync(VehicleCreatedEvent vehicleCreatedEvent)
    {
        await _collection.InsertOneAsync(vehicleCreatedEvent);
    }
}
