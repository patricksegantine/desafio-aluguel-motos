using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Domain.Repositories;

public interface IRentalPlanRepository : IRepository<RentalPlan>
{
    Task<(int total, IEnumerable<RentalPlanSummary>)> SearchAsync(RentalPlanFilter filter, CancellationToken cancellationToken = default);
}
