namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;

public record CreateRentalContractRequest
{
    public int VehicleId { get; set; }
    public int DeliveryDriverId { get; set; }
    public int RentalPlanId { get; set; }
    public DateTime ExpectedEndDate { get; set; }
}
