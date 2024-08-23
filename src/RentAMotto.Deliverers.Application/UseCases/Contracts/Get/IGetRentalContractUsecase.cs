using ErrorOr;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Get;

public interface IGetRentalContractUsecase
{
    Task<ErrorOr<GetRentalContractResult>> Handle(int rentalId, CancellationToken cancellationToken = default);
}
