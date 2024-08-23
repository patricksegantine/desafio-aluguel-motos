using Microsoft.EntityFrameworkCore;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;

public class RentalPlanRepository(MottoContext context) : IRentalPlanRepository
{
    private readonly MottoContext _context = context;

    public async Task<RentalPlan> AddAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        await _context.RentalPlans.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }


    public async Task<(int total, IEnumerable<RentalPlanSummary>)> SearchAsync(RentalPlanFilter filter, CancellationToken cancellationToken = default)
    {
        var pageSize = filter.PageSize ?? 10;
        var toSkip = ((filter.PageNumber ?? 1) - 1) * pageSize;

        var query = _context.RentalPlans.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(p => p.Description.Contains(filter.Description));

        query = query.Where(p => !p.Deleted)
                    .OrderBy(p => p.Description);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .AsSplitQuery()
            .Skip(toSkip)
            .Take(pageSize)
            .Select(p => new RentalPlanSummary
            {
                Id = p.Id,
                Description = p.Description,
                CostPerDay = p.CostPerDay,
                Status = p.Status
            }).ToListAsync(cancellationToken);

        return (totalCount, result);
    }

    public async Task<RentalPlan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.RentalPlans
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        _context.RentalPlans.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RentalPlan entity, CancellationToken cancellationToken = default)
    {
        entity.Delete();
        await UpdateAsync(entity, cancellationToken);
    }

}
