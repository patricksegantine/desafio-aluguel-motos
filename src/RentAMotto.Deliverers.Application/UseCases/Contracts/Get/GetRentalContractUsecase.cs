using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Get;

public sealed class GetRentalContractUsecase(IRentalContractRepository rentalRepository) : IGetRentalContractUsecase
{
    private readonly IRentalContractRepository _rentalRepository = rentalRepository;

    public async Task<ErrorOr<GetRentalContractResult>> Handle(int rentalId, CancellationToken cancellationToken = default)
    {
        var rentalContract = await _rentalRepository.GetByIdAsync(rentalId, cancellationToken);

        if (rentalContract is null)
            return ErrorCatalog.RentalContractNotFound;

        var result = (GetRentalContractResult)rentalContract;
        return result;
    }
}
