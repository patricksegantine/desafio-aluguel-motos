namespace RentAMotto.Domain.Queries;

public record DeliveryDriverSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime Birthday { get; set; }
    public string DrivingLicenceNumber { get; set; } = default!;
}
