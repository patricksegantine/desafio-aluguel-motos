using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Update;

public sealed class UpdateMotorcycleUsecase(IVehicleRepository vehicleRepository) : IUpdateMotorcycleUsecase
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public async Task<ErrorOr<Updated>> Handle(UpdateMotorcycleRequest request, CancellationToken cancellationToken = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (vehicle is null)
            return ErrorCatalog.VehicleNotFound;

        var (canUpdate, error) = await CanBeUpdated(request.NumberPlate, cancellationToken);
        if (!canUpdate)
            return error!.Value;

        vehicle.Update(numberPlate: request.NumberPlate);
        await _vehicleRepository.UpdateAsync(vehicle, cancellationToken);

        return Result.Updated;
    }

    public async Task<(bool, Error?)> CanBeUpdated(string? numberPlate, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(numberPlate))
            return (false, ErrorCatalog.VehicleNumberPlateMustNotBeEmpty);

        var numberPlateRegisterd = await _vehicleRepository.CountAsync(VehicleType.Motorcycle, numberPlate, cancellationToken);
        if (numberPlateRegisterd > 0)
            return (false, ErrorCatalog.VehicleNumberPlateAlreadyRegisterd);

        return (true, null);
    }
}
