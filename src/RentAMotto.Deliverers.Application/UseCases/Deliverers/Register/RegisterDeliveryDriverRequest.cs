using FluentValidation;
using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;

public record RegisterDeliveryDriverRequest
{
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime Birthday { get; set; }
    public string DrivingLicenceNumber { get; set; } = default!;
    public DrivingLicenceType DrivingLicenceType { get; set; }
}

public class Validator : AbstractValidator<RegisterDeliveryDriverRequest>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .Length(5, 60);

        RuleFor(x => x.Cnpj)
            .Length(14);

        RuleFor(x => x.Birthday)
            .GreaterThan(DateTime.Now.AddYears(-100));

        RuleFor(x => x.DrivingLicenceNumber)
            .Length(6, 10);
    }
}
