using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;
using System.Linq.Expressions;

namespace RentAMotto.Domain.Repositories;

public interface IDeliveryDriverRepository : IRepository<DeliveryDriver>
{
    Task<(int total, IEnumerable<DeliveryDriverSummary>)> SearchAsync(DeliveryDriverFilter filter, CancellationToken cancellationToken = default);
    Task<int> CountAsyc(Expression<Func<DeliveryDriver, bool>> expression, CancellationToken cancellationToken = default);
}