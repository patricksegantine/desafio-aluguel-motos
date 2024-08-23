using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Configurations;

public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlan>
{
    public void Configure(EntityTypeBuilder<RentalPlan> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.CostPerDay)
            .IsRequired()
            .HasColumnType("decimal(7,2)");
        
        builder.Property(e => e.PercentageOfFineForReturnBeforeExpectedEndDatePerDay)
            .IsRequired()
            .HasColumnType("decimal(7,2)");
        
        builder.Property(e => e.AmountOfFineForReturnAfterExpectedEndDatePerDay)
            .IsRequired()
            .HasColumnType("decimal(7,2)");
        
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(e => e.Deleted)
            .IsRequired();
    }
}
