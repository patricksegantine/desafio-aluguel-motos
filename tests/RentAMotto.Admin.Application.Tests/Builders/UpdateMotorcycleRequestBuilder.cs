using RentAMotto.Admin.Application.UseCases.Motorcycle.Update;

namespace RentAMotto.Admin.Application.Tests.Builders;

public class UpdateMotorcycleRequestBuilder
{
    private int _id = 1;
    private string _numberPlate = "ABC1234";

    public UpdateMotorcycleRequestBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UpdateMotorcycleRequestBuilder WithNumberPlate(string numberPlate)
    {
        _numberPlate = numberPlate;
        return this;
    }

    public UpdateMotorcycleRequest Build()
    {
        return new UpdateMotorcycleRequest
        {
            Id = _id,
            NumberPlate = _numberPlate
        };
    }
}
