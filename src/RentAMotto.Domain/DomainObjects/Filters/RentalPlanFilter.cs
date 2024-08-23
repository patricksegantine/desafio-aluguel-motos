using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.DomainObjects.Filters;

public class RentalPlanFilter : FilterBase
{
    public VehicleType Type { get; set; }
    public string? NumberPlate { get; set; }
}
