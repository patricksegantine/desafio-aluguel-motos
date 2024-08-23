using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Domain.Tests.Builders;

public class DeliveryDriverBuilder
{
    private string _name = "John Doe";
    private string _cnpj = "12345678901234";
    private DateTime _birthday = new DateTime(1980, 1, 1);
    private string _drivingLicenceNumber = "DL123456";
    private DrivingLicenceType _drivingLicenceType = DrivingLicenceType.B;
    private string? _driverLicensePicture = null;
    private bool _deleted = false;

    public DeliveryDriverBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public DeliveryDriverBuilder WithCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }

    public DeliveryDriverBuilder WithBirthday(DateTime birthday)
    {
        _birthday = birthday;
        return this;
    }

    public DeliveryDriverBuilder WithDrivingLicenceNumber(string drivingLicenceNumber)
    {
        _drivingLicenceNumber = drivingLicenceNumber;
        return this;
    }

    public DeliveryDriverBuilder WithDrivingLicenceType(DrivingLicenceType drivingLicenceType)
    {
        _drivingLicenceType = drivingLicenceType;
        return this;
    }

    public DeliveryDriverBuilder WithDriverLicensePicture(string? driverLicensePicture)
    {
        _driverLicensePicture = driverLicensePicture;
        return this;
    }

    public DeliveryDriverBuilder WithDeleted(bool deleted)
    {
        _deleted = deleted;
        return this;
    }

    public DeliveryDriver Build()
    {
        var driver = DeliveryDriver.Create(_name, _cnpj, _birthday, _drivingLicenceNumber, _drivingLicenceType);
        if (_driverLicensePicture != null)
        {
            driver.UpdateDriverLicenseImage(_driverLicensePicture);
        }
        if (_deleted)
        {
            driver.Delete();
        }
        return driver;
    }
}
