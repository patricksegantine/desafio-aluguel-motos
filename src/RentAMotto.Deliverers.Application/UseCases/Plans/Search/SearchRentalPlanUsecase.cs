using ErrorOr;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Queries;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.RentalPlans.Search;

public sealed class SearchRentalPlanUsecase(IRentalPlanRepository rentalPlanRepository) : ISearchRentalPlanUsecase
{
    private readonly IRentalPlanRepository _rentalPlanRepository = rentalPlanRepository;

    public async Task<ErrorOr<PagedResult<RentalPlanSummary>>> Handle(SearchRentalPlanRequest request, CancellationToken cancellationToken = default)
    {
        var filter = new RentalPlanFilter
        {
            Description = request.Description,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        var (totalItens, items) = await _rentalPlanRepository.SearchAsync(filter, cancellationToken);

        return new PagedResult<RentalPlanSummary>(totalItens, items);
    }
}
