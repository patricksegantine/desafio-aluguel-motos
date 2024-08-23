using RentAMotto.Domain.Entities;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

public record GetBalanceResult
{
    public int Id { get; set; }
    public decimal RentalAmount { get; set; }
    public decimal FineAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public static explicit operator GetBalanceResult(RentalContract rental)
    {
        return new GetBalanceResult
        {
            Id = rental.Id,
            RentalAmount = rental.RentalAmount,
            FineAmount = rental.FineAmount,
            TotalAmount = rental.TotalAmount,
        };
    }
}
