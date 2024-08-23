using ErrorOr;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Delete;

public interface IDeleteMotorcycleUsecase
{
    Task<ErrorOr<Deleted>> Handle(int id, CancellationToken cancellationToken = default);
}
