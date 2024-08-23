using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Queries;

public class VehicleSummary
{
    public int Id { get; set; }
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int YearOfManufacture { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public StatusType Status { get; set; }
}
