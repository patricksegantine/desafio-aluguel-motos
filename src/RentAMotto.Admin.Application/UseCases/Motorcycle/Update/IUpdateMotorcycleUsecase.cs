using ErrorOr;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Update;

public interface IUpdateMotorcycleUsecase
{
    Task<ErrorOr<Updated>> Handle(UpdateMotorcycleRequest request, CancellationToken cancellationToken = default);
}