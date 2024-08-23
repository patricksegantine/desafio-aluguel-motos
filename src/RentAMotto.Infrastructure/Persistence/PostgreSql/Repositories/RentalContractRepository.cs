using Microsoft.EntityFrameworkCore;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;

public class RentalContractRepository(MottoContext context) : IRentalContractRepository
{
    private readonly MottoContext _context = context;

    public async Task<RentalContract> AddAsync(RentalContract entity, CancellationToken cancellationToken = default)
    {
        await _context.RentalContracts.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<RentalContract?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.RentalContracts
            .Include(r => r.Vehicle)
            .Include(r => r.DeliveryDriver)
            .Include(r => r.RentalPlan)
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public Task UpdateAsync(RentalContract entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(RentalContract entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}