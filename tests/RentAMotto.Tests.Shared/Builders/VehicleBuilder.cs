using RentAMotto.Domain.Entities;

namespace RentAMotto.Tests.Shared.Builders;

public class VehicleBuilder
{
    private string _make = "Honda";
    private string _model = "CB500";
    private int _yearOfManufacture = 2020;
    private string _numberPlate = "ABC-1234";

    public VehicleBuilder WithMake(string make)
    {
        _make = make;
        return this;
    }

    public VehicleBuilder WithModel(string model)
    {
        _model = model;
        return this;
    }

    public VehicleBuilder WithYearOfManufacture(int yearOfManufacture)
    {
        _yearOfManufacture = yearOfManufacture;
        return this;
    }

    public VehicleBuilder WithNumberPlate(string numberPlate)
    {
        _numberPlate = numberPlate;
        return this;
    }

    public Vehicle Build()
    {
        return Vehicle.CreateMotorcycle(_make, _model, _yearOfManufacture, _numberPlate);
    }
}
