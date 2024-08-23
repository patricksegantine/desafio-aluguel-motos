using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Entities;

public class RentalPlan : Entity
{
    public string Description { get; private set; } = default!;
    public decimal CostPerDay { get; private set; }
    public decimal PercentageOfFineForReturnBeforeExpectedEndDatePerDay { get; private set; }
    public decimal AmountOfFineForReturnAfterExpectedEndDatePerDay { get; private set; }
    public StatusType Status { get; set; }
    public bool Deleted { get; set; }

    public static RentalPlan Create(
        string description,
        decimal costPerDay,
        decimal percentageOfFineForReturnBeforeExpectedEndDatePerDay,
        decimal amountOfFineForReturnAfterExpectedEndDatePerDay,
        StatusType status)
    {
        return new RentalPlan
        {
            Description = description,
            CostPerDay = costPerDay,
            PercentageOfFineForReturnBeforeExpectedEndDatePerDay = percentageOfFineForReturnBeforeExpectedEndDatePerDay,
            AmountOfFineForReturnAfterExpectedEndDatePerDay = amountOfFineForReturnAfterExpectedEndDatePerDay,
            Status = status,
            CreatedDate = DateTime.Now,
            UpdatedDate = null,
            Deleted = false,
        };
    }
}
