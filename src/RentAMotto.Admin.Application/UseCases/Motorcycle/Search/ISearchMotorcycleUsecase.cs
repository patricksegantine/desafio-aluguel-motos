using ErrorOr;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Search;

public interface ISearchMotorcycleUsecase
{
    Task<ErrorOr<PagedResult<VehicleSummary>>> Handle(SearchMotorcycleRequest request, CancellationToken cancellationToken = default);
}
