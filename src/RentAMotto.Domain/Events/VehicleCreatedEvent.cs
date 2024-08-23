using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Events;

public class VehicleCreatedEvent
{
    public int Id { get; set; }
    public VehicleType Type { get; set; }
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int YearOfManufacture { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}
