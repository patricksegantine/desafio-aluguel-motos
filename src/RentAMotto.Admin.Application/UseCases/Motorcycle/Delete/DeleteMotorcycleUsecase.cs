using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Delete;

public sealed class DeleteMotorcycleUsecase(IVehicleRepository vehicleRepository) : IDeleteMotorcycleUsecase
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public async Task<ErrorOr<Deleted>> Handle(int id, CancellationToken cancellationToken = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id, cancellationToken);
        if (vehicle is null)
            return ErrorCatalog.VehicleNotFound;

        if (vehicle.RentalContracts?.Count > 0)
            return ErrorCatalog.VehicleCannotBeDeleted;

        await _vehicleRepository.DeleteAsync(vehicle, cancellationToken);

        return Result.Deleted;
    }
}
