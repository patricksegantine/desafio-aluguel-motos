using RentAMotto.Domain.Entities;

namespace RentAMotto.Deliverers.Application.Common.DTOs;

public class RentalPlanDto
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public decimal CostPerDay { get; set; }
    public decimal PercentageOfFineForReturnBeforeExpectedEndDatePerDay { get; set; }
    public decimal AmountOfFineForReturnAfterExpectedEndDatePerDay { get; set; }


    public static explicit operator RentalPlanDto(RentalPlan rentalPlan)
    {
        return new RentalPlanDto
        {
            Id = rentalPlan.Id,
            Description = rentalPlan.Description,
            CostPerDay = rentalPlan.CostPerDay,
            PercentageOfFineForReturnBeforeExpectedEndDatePerDay = rentalPlan.PercentageOfFineForReturnBeforeExpectedEndDatePerDay,
            AmountOfFineForReturnAfterExpectedEndDatePerDay = rentalPlan.AmountOfFineForReturnAfterExpectedEndDatePerDay,
        };
    }
}
