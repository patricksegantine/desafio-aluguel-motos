using MassTransit;
using RentAMotto.Domain.Events;
using RentAMotto.Domain.Interfaces;

namespace RentAMotto.MottoCreatedConsumer.Worker.Consumers;

public class VehicleCreatedConsumer(
    IEventStore eventStore,
    ILogger<VehicleCreatedConsumer> logger) : IConsumer<VehicleCreatedEvent>
{
    private readonly IEventStore _eventStore = eventStore;
    private readonly ILogger<VehicleCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<VehicleCreatedEvent> context)
    {
        await _eventStore.SaveEventAsync(context.Message);

        _logger.LogInformation(
            "Received and stored message (make: '{Make}' | model: '{Model}' | numberPlate: '{NumberPlate}' yearOfManufacture: '{YearOfManufacture}')",
            context.Message.Make,
            context.Message.Model,
            context.Message.NumberPlate,
            context.Message.YearOfManufacture
        );
    }
}
