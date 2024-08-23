namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Update;

public record UpdateMotorcycleRequest
{
    public int Id { get; set; }
    public string NumberPlate { get; set; } = default!;
}