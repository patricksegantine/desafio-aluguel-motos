namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

public record GetBalanceRequest
{
    public int Id { get; set; }
    public DateTime? ReturnDate { get; set; }
}