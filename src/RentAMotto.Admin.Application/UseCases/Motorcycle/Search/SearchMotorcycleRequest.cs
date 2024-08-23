namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Search;

public record SearchMotorcycleRequest
{
    public string? NumberPlate { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}