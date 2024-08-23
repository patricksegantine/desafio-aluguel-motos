using RentAMotto.Deliverers.Application.Common.DTOs;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;

public record CreateRentalContractResult
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal RentalAmount { get; set; }
    public decimal FineAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public RentalPlanDto RentalPlan { get; set; }


    public static explicit operator CreateRentalContractResult(RentalContract rental)
    {
        return new CreateRentalContractResult
        {
            Id = rental.Id,
            StartDate = rental.StartDate,
            ExpectedEndDate = rental.ExpectedEndDate,
            EndDate = rental.EndDate,
            RentalAmount = rental.RentalAmount,
            FineAmount = rental.FineAmount,
            TotalAmount = rental.TotalAmount,
            RentalPlan = (RentalPlanDto)rental.RentalPlan,
        };
    }
}
