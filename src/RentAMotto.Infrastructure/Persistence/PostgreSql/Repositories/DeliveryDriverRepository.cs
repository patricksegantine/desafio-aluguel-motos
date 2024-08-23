using Microsoft.EntityFrameworkCore;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;
using RentAMotto.Domain.Repositories;
using System.Linq.Expressions;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;

public class DeliveryDriverRepository(MottoContext context) : IDeliveryDriverRepository
{
    private readonly MottoContext _context = context;

    public async Task<DeliveryDriver> AddAsync(DeliveryDriver entity, CancellationToken cancellationToken = default)
    {
        await _context.DeliveryDrivers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<(int total, IEnumerable<DeliveryDriverSummary>)> SearchAsync(DeliveryDriverFilter filter, CancellationToken cancellationToken = default)
    {
        var pageSize = filter.PageSize ?? 10;
        var toSkip = ((filter.PageNumber ?? 1) - 1) * pageSize;

        var query = _context.DeliveryDrivers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(dd => dd.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Cnpj))
            query = query.Where(dd => dd.Cnpj == filter.Cnpj);

        if (!filter.Birthday.HasValue)
            query = query.Where(dd => dd.Birthday == filter.Birthday);

        if (!string.IsNullOrEmpty(filter.DrivingLicenceNumber))
            query = query.Where(dd => dd.DrivingLicenceNumber == filter.DrivingLicenceNumber);

        query = query.OrderBy(dd => dd.Name);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .AsSplitQuery()
            .Skip(toSkip)
            .Take(pageSize)
            .Select(dd => new DeliveryDriverSummary
            {
                Id = dd.Id,
                Name = dd.Name,
                Cnpj = dd.Cnpj,
                Birthday = dd.Birthday,
                DrivingLicenceNumber = dd.DrivingLicenceNumber,
            }).ToListAsync(cancellationToken);

        return (totalCount, result);
    }

    public async Task<DeliveryDriver?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.DeliveryDrivers
            .Include(dd => dd.RentalContracts)
            .AsSplitQuery()
            .FirstOrDefaultAsync(dd => dd.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(DeliveryDriver entity, CancellationToken cancellationToken = default)
    {
        _context.DeliveryDrivers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeliveryDriver entity, CancellationToken cancellationToken = default)
    {
        entity.Delete();
        await UpdateAsync(entity, cancellationToken);
    }

    public async Task<int> CountAsyc(Expression<Func<DeliveryDriver, bool>> expression, CancellationToken cancellationToken = default)
    {
        var query = _context.DeliveryDrivers
            .AsNoTracking()
            .Where(expression);

        return await query.CountAsync(cancellationToken);
    }
}
