using FluentValidation;
using RentAMotto.Domain;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Create;

public record CreateMotorcycleRequest
{
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int YearOfManufacture { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
}

public class Validator : AbstractValidator<CreateMotorcycleRequest>
{
    // Não permite cadastrar motos com mais de 5 anos de uso
    private const int MAX_YEARS_OF_MANUFACTURE = 5;

    public Validator()
    {
        RuleFor(x => x.Make)
            .NotEmpty()
            .WithErrorCode(ErrorCatalog.VehicleMakeMustNotBeEmpty.Code)
            .WithMessage(ErrorCatalog.VehicleMakeMustNotBeEmpty.Description);

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithErrorCode(ErrorCatalog.VehicleModelMustNotBeEmpty.Code)
            .WithMessage(ErrorCatalog.VehicleModelMustNotBeEmpty.Description);

        RuleFor(x => x.YearOfManufacture)
            .GreaterThan(DateTime.Now.Year - MAX_YEARS_OF_MANUFACTURE)
            .WithErrorCode(ErrorCatalog.VehicleYearOfManufactureMustNotBeOldThan(MAX_YEARS_OF_MANUFACTURE).Code)
            .WithMessage(ErrorCatalog.VehicleYearOfManufactureMustNotBeOldThan(MAX_YEARS_OF_MANUFACTURE).Description);

        RuleFor(x => x.NumberPlate)
            .NotEmpty()
            .WithErrorCode(ErrorCatalog.VehicleNumberPlateMustNotBeEmpty.Code)
            .WithMessage(ErrorCatalog.VehicleNumberPlateMustNotBeEmpty.Description);
    }
}
