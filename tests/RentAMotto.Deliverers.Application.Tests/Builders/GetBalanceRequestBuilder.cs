using RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

namespace RentAMotto.Deliverers.Application.Tests.Builders;

public class GetBalanceRequestBuilder
{
    private int _id = 1;
    private DateTime? _returnDate = null;

    public GetBalanceRequestBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public GetBalanceRequestBuilder WithReturnDate(DateTime? returnDate)
    {
        _returnDate = returnDate;
        return this;
    }

    public GetBalanceRequest Build()
    {
        return new GetBalanceRequest
        {
            Id = _id,
            ReturnDate = _returnDate
        };
    }
}
