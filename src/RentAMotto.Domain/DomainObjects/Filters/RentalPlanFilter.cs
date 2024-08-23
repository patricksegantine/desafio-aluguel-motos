using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.DomainObjects.Filters;

public class RentalPlanFilter : FilterBase
{
    public string? Description { get; set; }
}
