using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Domain.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<(int total, IEnumerable<VehicleSummary>)> SearchAsync(VehicleFilter filter, CancellationToken cancellationToken = default);
    Task<int> CountAsync(VehicleType type, string numberPlate, CancellationToken cancellationToken = default);
}
