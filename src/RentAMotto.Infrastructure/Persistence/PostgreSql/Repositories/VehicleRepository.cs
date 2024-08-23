using Microsoft.EntityFrameworkCore;
using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.DomainObjects.Filters;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Queries;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;

public class VehicleRepository(MottoContext context) : IVehicleRepository
{
    private readonly MottoContext _context = context;

    public async Task<Vehicle> AddAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        await _context.Vehicles.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<(int total, IEnumerable<VehicleSummary>)> SearchAsync(VehicleFilter filter, CancellationToken cancellationToken = default)
    {
        var pageSize = filter.PageSize ?? 10;
        var toSkip = ((filter.PageNumber ?? 1) - 1) * pageSize;

        var query = _context.Vehicles
            .AsNoTracking()
            .Where(v => v.Type == filter.Type);

        if (!string.IsNullOrWhiteSpace(filter.NumberPlate))
            query = query.Where(v => v.NumberPlate == filter.NumberPlate);

        query = query
            .OrderBy(v => v.Make)
            .ThenBy(v => v.Model);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .AsSplitQuery()
            .Skip(toSkip)
            .Take(pageSize)
            .Select(v => new VehicleSummary
            {
                Id = v.Id,
                Make = v.Make,
                Model = v.Model,
                NumberPlate = v.NumberPlate,
                YearOfManufacture = v.YearOfManufacture,
                Status = v.Status,
            }).ToListAsync(cancellationToken);

        return (totalCount, result);
    }

    public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .Include(dd => dd.RentalContracts)
            .AsSplitQuery()
            .FirstOrDefaultAsync(dd => dd.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        _context.Vehicles.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task DeleteAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        entity.Delete();
        await UpdateAsync(entity, cancellationToken);
    }

    public async Task<int> CountAsync(VehicleType type, string numberPlate, CancellationToken cancellationToken = default)
    {
        var query = _context.Vehicles
            .AsNoTracking()
            .Where(v => v.Type == type)
            .Where(v => v.NumberPlate == numberPlate);

        return await query.CountAsync(cancellationToken);
    }
}
