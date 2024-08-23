using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.DomainObjects.Filters;

public class VehicleFilter : FilterBase
{
    public VehicleType Type { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? YearOfManufacture { get; set; }
    public string? NumberPlate { get; set; }
    public StatusType? Status { get; set; }
}
