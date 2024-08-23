using ErrorOr;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Create;

public interface ICreateMotorcycleUsecase
{
    Task<ErrorOr<CreateMotorcycleResult>> Handle(CreateMotorcycleRequest request, CancellationToken cancellationToken = default);
}
