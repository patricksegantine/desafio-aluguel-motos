using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;

public class RentalPlanRepository : IRentalPlanRepository
{
    public Task<RentalPlan> AddAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RentalPlan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<(int total, IEnumerable<RentalPlan>)> Search(RentalPlanFilter filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
