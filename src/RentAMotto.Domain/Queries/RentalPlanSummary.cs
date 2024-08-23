using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Queries;

public record RentalPlanSummary
{
    public int Id { get; set; }
    public string Description { get;  set; } = default!;
    public decimal CostPerDay { get;  set; }
    public StatusType Status { get;  set; }
}
