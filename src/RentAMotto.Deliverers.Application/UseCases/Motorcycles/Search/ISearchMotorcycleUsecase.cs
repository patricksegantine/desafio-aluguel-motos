using ErrorOr;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Deliverers.Application.UseCases.Motorcycles.Search;

public interface ISearchMotorcycleUsecase
{
    Task<ErrorOr<PagedResult<VehicleSummary>>> Handle(SearchMotorcycleRequest request, CancellationToken cancellationToken = default);
}
