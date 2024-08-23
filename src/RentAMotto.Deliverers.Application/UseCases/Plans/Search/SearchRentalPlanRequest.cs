namespace RentAMotto.Deliverers.Application.UseCases.RentalPlans.Search;

public record SearchRentalPlanRequest
{
    public string? Description { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
