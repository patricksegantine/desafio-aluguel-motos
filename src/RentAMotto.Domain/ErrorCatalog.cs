using ErrorOr;
using FluentValidation.Results;

namespace RentAMotto.Domain;

public static class ErrorCatalog
{
    public static Error VehicleMakeMustNotBeEmpty => Error.Validation("ERR-CODE-01", "Fabricante deve ser preenchido");
    public static Error VehicleModelMustNotBeEmpty => Error.Validation("ERR-CODE-02", "Modelo deve ser preenchido");
    public static Error VehicleYearOfManufactureMustNotBeOldThan(int years) => Error.Validation("ERR-CODE-03", $"Não é possível cadastrar veículos com mais de {years} de uso");
    public static Error VehicleNumberPlateMustNotBeEmpty => Error.Validation("ERR-CODE-04", "A placa deve ser preenchida");
    public static Error VehicleNumberPlateAlreadyRegisterd => Error.Validation("ERR-CODE-05", "A placa deve ser preenchida");
    public static Error VehicleNotFound => Error.Validation("ERR-CODE-06", "Veículo não encontrado");
    public static Error VehicleUnavailable => Error.Validation("ERR-CODE-07", "Veículo indisponível para locação no momento");
    public static Error VehicleCannotBeDeleted => Error.Validation("ERR-CODE-08", "Veículo não pode ser deletado, pois possui locações associadas");

    public static Error DeliveryDriverNotFound => Error.Validation("ERR-CODE-20", "Entregador não encontrado");
    public static Error InvalidDrivingLicenceType => Error.Validation("ERR-CODE-21", "Tipo de habilitação não permitido para motocicletas");
    public static Error InvalidDrivingLicenceFormat => Error.Validation("ERR-CODE-22", "Tipo de arquivo não permitido");
    public static Error CnpjAlreadyRegisterd => Error.Validation("ERR-CODE-23", "CNPJ já cadastrado");
    public static Error DrivingLicenceAlreadyRegisterd => Error.Validation("ERR-CODE-24", "Habilitação já cadastrada");
    public static Error ErrorWhileSavingDrivingLicence => Error.Validation("ERR-CODE-25", "Erro ao salvar a imagem da habilitação. Tente novamente");

    public static Error RentalContractNotFound => Error.Validation("ERR-CODE-30", "Contrato de locação não encontrado");
    public static Error RentalPlanNotFound => Error.Validation("ERR-CODE-31", "Plano não encontrado");
    public static Error RentalPlanUnavailable => Error.Validation("ERR-CODE-32", "Plano não disponível no momento");

}

public static class ErrorOrExtensions
{
    public static List<Error> ToErrorList(this List<ValidationFailure> failures)
    {
        return failures.ConvertAll((ValidationFailure failure) => Error.Custom((int)ErrorType.Validation, failure.ErrorCode, failure.ErrorMessage)) ?? Enumerable.Empty<Error>().ToList();
    }
}
