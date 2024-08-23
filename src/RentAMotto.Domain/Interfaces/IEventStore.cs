using RentAMotto.Domain.Events;

namespace RentAMotto.Domain.Interfaces;

public interface IEventStore
{
    Task SaveEventAsync(VehicleCreatedEvent vehicleCreatedEvent);
}
