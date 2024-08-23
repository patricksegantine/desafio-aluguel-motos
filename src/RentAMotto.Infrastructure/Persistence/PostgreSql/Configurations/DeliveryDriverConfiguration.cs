using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Configurations;

public class DeliveryDriverConfiguration : IEntityTypeConfiguration<DeliveryDriver>
{
    public void Configure(EntityTypeBuilder<DeliveryDriver> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(60);
        
        builder.Property(e => e.Cnpj)
            .IsRequired()
            .HasMaxLength(14);
        
        builder.Property(e => e.DrivingLicenceNumber)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(e => e.DrivingLicenceType)
            .IsRequired();
        
        builder.Property(e => e.Birthday)
            .IsRequired();
        
        builder.Property(e => e.DriverLicensePicture)
            .HasMaxLength(1000);

        builder.Property(e => e.Deleted)
            .IsRequired();

        builder.HasMany(e => e.RentalContracts)
               .WithOne(r => r.DeliveryDriver)
               .HasForeignKey(r => r.DeliveryDriverId);

        builder.HasIndex(e => e.Cnpj)
            .IsUnique();

        builder.HasIndex(e => e.DrivingLicenceNumber)
            .IsUnique();
    }
}
