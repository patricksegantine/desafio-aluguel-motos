using RentAMotto.Domain.Entities;

namespace RentAMotto.Tests.Shared.Builders;

public class RentalContractBuilder
{
    private Vehicle _vehicle = new VehicleBuilder().Build();
    private DeliveryDriver _deliveryDriver = new DeliveryDriverBuilder().Build();
    private RentalPlan _rentalPlan = new RentalPlanBuilder().Build();
    private DateTime _startDate = DateTime.Now.AddDays(1);
    private DateTime _expectedEndDate = DateTime.Now.AddDays(10);
    private Domain.DomainObjects.Enums.RentalStatusType _status = Domain.DomainObjects.Enums.RentalStatusType.Open;

    public RentalContractBuilder WithVehicle(Vehicle vehicle)
    {
        _vehicle = vehicle;
        return this;
    }

    public RentalContractBuilder WithDeliveryDriver(DeliveryDriver deliveryDriver)
    {
        _deliveryDriver = deliveryDriver;
        return this;
    }

    public RentalContractBuilder WithRentalPlan(RentalPlan rentalPlan)
    {
        _rentalPlan = rentalPlan;
        return this;
    }

    public RentalContractBuilder WithStartDate(DateTime startDate)
    {
        _startDate = startDate;
        return this;
    }

    public RentalContractBuilder WithExpectedEndDate(DateTime expectedEndDate)
    {
        _expectedEndDate = expectedEndDate;
        return this;
    }

    public RentalContractBuilder WithStatus(Domain.DomainObjects.Enums.RentalStatusType status)
    {
        _status = status;
        return this;
    }

    public RentalContract Build()
    {
        return RentalContract.Create(_vehicle, _deliveryDriver, _rentalPlan, _expectedEndDate);
    }
}
