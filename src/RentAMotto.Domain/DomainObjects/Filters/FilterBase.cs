namespace RentAMotto.Domain.DomainObjects.Filters;

public abstract class FilterBase
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
