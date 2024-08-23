namespace RentAMotto.Deliverers.Application.UseCases.Motorcycles.Search;

public record SearchMotorcycleRequest
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? YearOfManufacture { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}