using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Configurations;

public class RentalContractConfiguration : IEntityTypeConfiguration<RentalContract>
{
    public void Configure(EntityTypeBuilder<RentalContract> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DeliveryDriverId)
            .IsRequired();

        builder.Property(e => e.RentalPlanId)
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.ExpectedEndDate)
            .IsRequired();

        builder.Property(e => e.RentalAmount)
            .IsRequired()
            .HasColumnType("decimal(7,2)");

        builder.Property(e => e.FineAmount)
            .IsRequired().HasColumnType("decimal(7,2)");

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(e => e.Vehicle)
               .WithMany(d => d.RentalContracts)
               .HasForeignKey(e => e.VehicleId);

        builder.HasOne(e => e.DeliveryDriver)
               .WithMany(d => d.RentalContracts)
               .HasForeignKey(e => e.DeliveryDriverId);

        builder.HasOne(e => e.RentalPlan)
               .WithMany()
               .HasForeignKey(e => e.RentalPlanId);
    }
}
