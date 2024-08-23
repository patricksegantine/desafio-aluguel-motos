using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Get;

public record GetMotorcycleResult
{
    public int Id { get; set; }
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int YearOfManufacture { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public StatusType Status { get; set; }


    public static explicit operator GetMotorcycleResult(Vehicle vehicle)
    {
        return new GetMotorcycleResult
        {
            Id = vehicle.Id,
            Make = vehicle.Make,
            Model = vehicle.Model,
            YearOfManufacture = vehicle.YearOfManufacture,
            NumberPlate = vehicle.NumberPlate,
            Status = vehicle.Status,
        };
    }
}
