namespace RentAMotto.Domain.DomainObjects.Filters;

public class DeliveryDriverFilter : FilterBase
{
    public string? Name { get; set; }
    public string? Cnpj { get; set; }
    public DateTime? Birthday { get; set; }
    public string? DrivingLicenceNumber { get; set; }
}
