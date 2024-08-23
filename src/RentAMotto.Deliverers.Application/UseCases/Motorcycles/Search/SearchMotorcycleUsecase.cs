using ErrorOr;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Queries;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.Motorcycles.Search;

public sealed class SearchMotorcycleUsecase(IVehicleRepository vehicleRepository) : ISearchMotorcycleUsecase
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public async Task<ErrorOr<PagedResult<VehicleSummary>>> Handle(SearchMotorcycleRequest request, CancellationToken cancellationToken = default)
    {
        var filter = new VehicleFilter
        {
            Type = Domain.DomainObjects.Enums.VehicleType.Motorcycle,
            Status = Domain.DomainObjects.Enums.StatusType.Active,
            Make = request.Make,
            Model = request.Model,
            YearOfManufacture = request.YearOfManufacture,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        var (totalItens, items) = await _vehicleRepository.SearchAsync(filter, cancellationToken);

        return new PagedResult<VehicleSummary>(totalItens, items);
    }
}
