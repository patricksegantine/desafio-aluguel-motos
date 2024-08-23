using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Tests.Shared.Builders;

public class RentalPlanBuilder
{
    private string _description = "Basic Plan";
    private decimal _costPerDay = 50;
    private decimal _percentageOfFineForReturnBeforeExpectedEndDatePerDay = 10;
    private decimal _amountOfFineForReturnAfterExpectedEndDatePerDay = 20;
    private StatusType _status = StatusType.Active;
    private bool _deleted = false;

    public RentalPlanBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public RentalPlanBuilder WithCostPerDay(decimal costPerDay)
    {
        _costPerDay = costPerDay;
        return this;
    }

    public RentalPlanBuilder WithPercentageOfFineForReturnBeforeExpectedEndDatePerDay(decimal percentage)
    {
        _percentageOfFineForReturnBeforeExpectedEndDatePerDay = percentage;
        return this;
    }

    public RentalPlanBuilder WithAmountOfFineForReturnAfterExpectedEndDatePerDay(decimal amount)
    {
        _amountOfFineForReturnAfterExpectedEndDatePerDay = amount;
        return this;
    }

    public RentalPlanBuilder WithStatus(StatusType status)
    {
        _status = status;
        return this;
    }

    public RentalPlanBuilder WithDeleted(bool deleted)
    {
        _deleted = deleted;
        return this;
    }

    public RentalPlan Build()
    {
        var plan = RentalPlan.Create(_description, _costPerDay, _percentageOfFineForReturnBeforeExpectedEndDatePerDay, _amountOfFineForReturnAfterExpectedEndDatePerDay, _status);
        if (_deleted)
        {
            plan.Delete();
        }
        return plan;
    }
}
