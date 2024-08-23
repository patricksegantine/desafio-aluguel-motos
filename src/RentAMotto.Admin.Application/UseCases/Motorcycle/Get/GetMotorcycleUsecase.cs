using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Get;

public sealed class GetMotorcycleUsecase(IVehicleRepository vehicleRepository) : IGetMotorcycleUsecase
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public async Task<ErrorOr<GetMotorcycleResult>> Handle(int id, CancellationToken cancellationToken = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id, cancellationToken);
        if (vehicle is null)
            return ErrorCatalog.VehicleNotFound;

        var result = (GetMotorcycleResult)vehicle;
        return result;
    }
}
