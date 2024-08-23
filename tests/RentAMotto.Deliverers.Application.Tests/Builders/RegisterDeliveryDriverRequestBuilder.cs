using RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;
using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Deliverers.Application.Tests.Builders;

public class RegisterDeliveryDriverRequestBuilder
{
    private string _name = "John Doe";
    private string _cnpj = "12345678901234";
    private DateTime _birthday = DateTime.Now.AddYears(-30);
    private string _drivingLicenceNumber = "ABC123";
    private DrivingLicenceType _drivingLicenceType = DrivingLicenceType.B;

    public RegisterDeliveryDriverRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public RegisterDeliveryDriverRequestBuilder WithCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }

    public RegisterDeliveryDriverRequestBuilder WithBirthday(DateTime birthday)
    {
        _birthday = birthday;
        return this;
    }

    public RegisterDeliveryDriverRequestBuilder WithDrivingLicenceNumber(string drivingLicenceNumber)
    {
        _drivingLicenceNumber = drivingLicenceNumber;
        return this;
    }

    public RegisterDeliveryDriverRequestBuilder WithDrivingLicenceType(DrivingLicenceType drivingLicenceType)
    {
        _drivingLicenceType = drivingLicenceType;
        return this;
    }

    public RegisterDeliveryDriverRequest Build()
    {
        return new RegisterDeliveryDriverRequest
        {
            Name = _name,
            Cnpj = _cnpj,
            Birthday = _birthday,
            DrivingLicenceNumber = _drivingLicenceNumber,
            DrivingLicenceType = _drivingLicenceType
        };
    }
}
