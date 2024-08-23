using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentAMotto.Domain.Entities;
using RentAMotto.Infrastructure.Persistence.PostgreSql.Configurations;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql;

public class MottoContext : DbContext
{
    public MottoContext() { }

    public MottoContext(DbContextOptions<MottoContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
    public DbSet<RentalPlan> RentalPlans { get; set; }
    public DbSet<RentalContract> RentalContracts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeliveryDriverConfiguration());
        modelBuilder.ApplyConfiguration(new RentalContractConfiguration());
        modelBuilder.ApplyConfiguration(new RentalPlanConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());

        // Configurar todas as propriedades DateTime para serem UTC
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
    }
}
