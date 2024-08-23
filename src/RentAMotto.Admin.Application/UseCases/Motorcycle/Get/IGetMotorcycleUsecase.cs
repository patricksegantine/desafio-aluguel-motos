using ErrorOr;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Get;

public interface IGetMotorcycleUsecase
{
    Task<ErrorOr<GetMotorcycleResult>> Handle(int id, CancellationToken cancellationToken = default);
}
