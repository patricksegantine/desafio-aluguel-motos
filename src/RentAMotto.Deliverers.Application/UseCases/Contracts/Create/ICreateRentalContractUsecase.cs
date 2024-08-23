using ErrorOr;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;

public interface ICreateRentalContractUsecase
{
    Task<ErrorOr<CreateRentalContractResult>> Handle(CreateRentalContractRequest request, CancellationToken cancellationToken = default);
}
