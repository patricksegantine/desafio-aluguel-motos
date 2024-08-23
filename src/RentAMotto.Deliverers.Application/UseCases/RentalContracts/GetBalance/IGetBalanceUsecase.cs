using ErrorOr;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

public interface IGetBalanceUsecase
{
    Task<ErrorOr<GetBalanceResult>> Handle(GetBalanceRequest request, CancellationToken cancellationToken = default);
}
