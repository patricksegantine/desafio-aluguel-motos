using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.DomainObjects.Filters;

public class VehicleFilter : FilterBase
{
    public VehicleType Type { get; set; }
    public string? NumberPlate { get; set; }
}
