using RentAMotto.Admin.Application.UseCases.Motorcycle.Create;

namespace RentAMotto.Admin.Application.Tests.Builders;

public class CreateMotorcycleRequestBuilder
{
    private string _make = "Honda";
    private string _model = "CB500";
    private int _yearOfManufacture = DateTime.Now.Year;
    private string _numberPlate = "ABC1234";

    public CreateMotorcycleRequestBuilder WithMake(string make)
    {
        _make = make;
        return this;
    }

    public CreateMotorcycleRequestBuilder WithModel(string model)
    {
        _model = model;
        return this;
    }

    public CreateMotorcycleRequestBuilder WithYearOfManufacture(int year)
    {
        _yearOfManufacture = year;
        return this;
    }

    public CreateMotorcycleRequestBuilder WithNumberPlate(string numberPlate)
    {
        _numberPlate = numberPlate;
        return this;
    }

    public CreateMotorcycleRequest Build()
    {
        return new CreateMotorcycleRequest
        {
            Make = _make,
            Model = _model,
            YearOfManufacture = _yearOfManufacture,
            NumberPlate = _numberPlate
        };
    }
}
