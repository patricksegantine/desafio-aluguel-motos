using ErrorOr;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Deliverers.Application.UseCases.RentalPlans.Search;

public interface ISearchRentalPlanUsecase
{
    Task<ErrorOr<PagedResult<RentalPlanSummary>>> Handle(SearchRentalPlanRequest request, CancellationToken cancellationToken = default);
}
