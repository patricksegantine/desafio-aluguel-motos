using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Entities;

public class DeliveryDriver : Entity
{
    public string Name { get; private set; } = default!;
    public string Cnpj { get; private set; } = default!;
    public DateTime Birthday { get; set; }
    public string DrivingLicenceNumber { get; private set; } = default!;
    public DrivingLicenceType DrivingLicenceType { get; private set; }
    public string? DriverLicensePicture { get; private set; }
    public bool Deleted { get; private set; }

    public List<RentalContract>? RentalContracts { get; set; }

    public static DeliveryDriver Create(string name, string cnpj, DateTime birthday, string drivingLicenceNumber, DrivingLicenceType drivingLicenceType)
    {
        return new DeliveryDriver
        {
            Name = name,
            Cnpj = cnpj,
            Birthday = birthday,
            DrivingLicenceNumber = drivingLicenceNumber,
            DrivingLicenceType = drivingLicenceType,
            CreatedDate = DateTime.Now,
            UpdatedDate = null,
        };
    }

    public void Delete()
    {
        Deleted = true;
        UpdatedDate = DateTime.Now;
    }

    public void UpdateDriverLicenseImage(string url) => DriverLicensePicture = url;
}
