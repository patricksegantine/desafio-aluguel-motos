namespace RentAMotto.Domain.Entities;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? UpdatedDate { get; protected set; }
}
