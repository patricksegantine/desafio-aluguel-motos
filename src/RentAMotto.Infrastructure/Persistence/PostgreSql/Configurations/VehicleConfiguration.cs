using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(e => e.Make)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.Model)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(e => e.YearOfManufacture)
            .IsRequired();
        
        builder.Property(e => e.NumberPlate)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(e => e.Deleted)
            .IsRequired();

        builder.HasMany(e => e.RentalContracts)
               .WithOne(r => r.Vehicle)
               .HasForeignKey(r => r.VehicleId);

        builder.HasIndex(e => e.NumberPlate)
            .IsUnique();
    }
}