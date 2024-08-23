using RentAMotto.Domain.Tests.Builders;

namespace RentAMotto.Domain.Tests.Unit;

using Xunit;

public class RentalContractTests
{
    [Fact]
    public void CalculateAmountAndFineBalance_ReturnDateBeforeExpectedEndDate_ShouldCalculateCorrectly()
    {
        // Arrange
        var rentalPlan = new RentalPlanBuilder()
            .WithCostPerDay(100)
            .WithPercentageOfFineForReturnBeforeExpectedEndDatePerDay(10)
            .WithAmountOfFineForReturnAfterExpectedEndDatePerDay(50)
            .Build();

        var rentalContract = new RentalContractBuilder()
            .WithRentalPlan(rentalPlan)
            .WithStartDate(DateTime.Now.AddDays(1))
            .WithExpectedEndDate(DateTime.Now.AddDays(11))
            .Build();

        var returnDate = DateTime.Now.AddDays(6);

        // Act
        var (amount, fine) = rentalContract.CalculateAmountAndFineBalance(returnDate);

        // Assert
        Assert.Equal(500, amount); // 5 days * 100 cost per day
        Assert.Equal(50, fine); // 5 days * 10% fine per day
    }

    [Fact]
    public void CalculateAmountAndFineBalance_ReturnDateOnExpectedEndDate_ShouldCalculateCorrectly()
    {
        // Arrange
        var rentalPlan = new RentalPlanBuilder()
            .WithCostPerDay(100)
            .WithPercentageOfFineForReturnBeforeExpectedEndDatePerDay(10)
            .WithAmountOfFineForReturnAfterExpectedEndDatePerDay(50)
            .Build();

        var rentalContract = new RentalContractBuilder()
            .WithRentalPlan(rentalPlan)
            .WithExpectedEndDate(DateTime.Now.AddDays(11))
            .Build();

        var returnDate = DateTime.Now.AddDays(11);

        // Act
        var (amount, fine) = rentalContract.CalculateAmountAndFineBalance(returnDate);

        // Assert
        Assert.Equal(1000, amount); // 10 days * 100 cost per day
        Assert.Equal(0, fine); // No fine
    }

    [Fact]
    public void CalculateAmountAndFineBalance_ReturnDateAfterExpectedEndDate_ShouldCalculateCorrectly()
    {
        // Arrange
        var rentalPlan = new RentalPlanBuilder()
            .WithCostPerDay(100)
            .WithPercentageOfFineForReturnBeforeExpectedEndDatePerDay(10)
            .WithAmountOfFineForReturnAfterExpectedEndDatePerDay(50)
            .Build();

        var rentalContract = new RentalContractBuilder()
            .WithRentalPlan(rentalPlan)
            .WithExpectedEndDate(DateTime.Now.AddDays(11))
            .Build();

        var returnDate = DateTime.Now.AddDays(16);

        // Act
        var (amount, fine) = rentalContract.CalculateAmountAndFineBalance(returnDate);

        // Assert
        Assert.Equal(1000, amount); // 10 days * 100 cost per day
        Assert.Equal(250, fine); // 5 days * 50 fine per day
    }

    [Fact]
    public void CalculateAmountAndFineBalance_ReturnDateBeforeStartDate_ShouldCalculateCorrectly()
    {
        // Arrange
        var rentalPlan = new RentalPlanBuilder()
            .WithCostPerDay(100)
            .WithPercentageOfFineForReturnBeforeExpectedEndDatePerDay(10)
            .WithAmountOfFineForReturnAfterExpectedEndDatePerDay(50)
            .Build();

        var rentalContract = new RentalContractBuilder()
            .WithRentalPlan(rentalPlan)
            .WithStartDate(DateTime.Now.AddDays(1))
            .WithExpectedEndDate(DateTime.Now.AddDays(11))
            .Build();

        var returnDate = DateTime.Now;

        // Act
        var (amount, fine) = rentalContract.CalculateAmountAndFineBalance(returnDate);

        // Assert
        Assert.Equal(0, amount); // No days used
        Assert.Equal(100, fine); // 10 days * 10% fine per day
    }
}
