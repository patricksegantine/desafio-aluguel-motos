using RentAMotto.Domain.DomainObjects.Enums;

namespace RentAMotto.Domain.Entities;

public class Vehicle : Entity
{
    public VehicleType Type { get; private set; }
    public string Make { get; private set; } = default!;
    public string Model { get; private set; } = default!;
    public int YearOfManufacture { get; private set; } = default!;
    public string NumberPlate { get; private set; } = default!;
    public StatusType Status { get; private set; }
    public bool Deleted { get; private set; }

    public List<RentalContract>? RentalContracts { get; set; }


    public static Vehicle CreateMotorcycle(string make, string model, int yearOfManufacture, string numberPlate)
    {
        return new Vehicle
        {
            Type = VehicleType.Motorcycle,
            Make = make,
            Model = model,
            YearOfManufacture = yearOfManufacture,
            NumberPlate = numberPlate,
            Status = StatusType.Active,
            Deleted = false,
            CreatedDate = DateTime.Now,
            UpdatedDate = null,
        };
    }

    public void Update(
        string? make = null,
        string? model = null,
        int? yearOfManufacture = null,
        string? numberPlate = null)
    {
        if (!string.IsNullOrWhiteSpace(make))
            Make = make;

        if (!string.IsNullOrWhiteSpace(model))
            Model = model;

        YearOfManufacture = yearOfManufacture ?? YearOfManufacture;

        if (!string.IsNullOrWhiteSpace(numberPlate))
            NumberPlate = numberPlate;

        UpdatedDate = DateTime.Now;
    }

    public void Delete()
    {
        Deleted = true;
        UpdatedDate = DateTime.Now;
    }
}
