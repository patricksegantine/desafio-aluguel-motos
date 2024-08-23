using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Domain.Repositories;

public interface IRentalPlanRepository : IRepository<RentalPlan>
{
    Task<(int total, IEnumerable<RentalPlan>)> Search(RentalPlanFilter filter, CancellationToken cancellationToken = default);
}
