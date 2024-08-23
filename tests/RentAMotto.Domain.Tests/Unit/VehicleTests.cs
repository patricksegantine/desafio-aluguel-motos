using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Tests.Shared.Builders;

namespace RentAMotto.Domain.Tests.Unit;

public class VehicleTests
{
    [Fact]
    public void CreateMotorcycle_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var vehicle = new VehicleBuilder().Build();

        // Assert
        Assert.Equal(VehicleType.Motorcycle, vehicle.Type);
        Assert.Equal("Honda", vehicle.Make);
        Assert.Equal("CB500", vehicle.Model);
        Assert.Equal(2020, vehicle.YearOfManufacture);
        Assert.Equal("ABC-1234", vehicle.NumberPlate);
        Assert.Equal(StatusType.Active, vehicle.Status);
        Assert.False(vehicle.Deleted);
    }
}
