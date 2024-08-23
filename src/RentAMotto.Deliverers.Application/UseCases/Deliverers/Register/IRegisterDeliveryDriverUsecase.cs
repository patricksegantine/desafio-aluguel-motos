using ErrorOr;

namespace RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;

public interface IRegisterDeliveryDriverUsecase
{
    Task<ErrorOr<RegisterDeliveryDriverResult>> Handle(RegisterDeliveryDriverRequest request, CancellationToken cancellationToken = default);
}
