using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Deliverers.Application.Common.DTOs;

public class DeliveryDriverDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime Birthday { get; set; }
    public string DrivingLicenceNumber { get; private set; } = default!;
    public DrivingLicenceType DrivingLicenceType { get; set; }
    public string? DriverLicensePicture { get; set; }


    public static explicit operator DeliveryDriverDto(DeliveryDriver deliveryDriver)
    {
        return new DeliveryDriverDto
        {
            Id = deliveryDriver.Id,
            Name = deliveryDriver.Name,
            Cnpj = deliveryDriver.Cnpj,
            Birthday = deliveryDriver.Birthday,
            DrivingLicenceNumber = deliveryDriver.DrivingLicenceNumber,
            DrivingLicenceType = deliveryDriver.DrivingLicenceType,
        };
    }
}
