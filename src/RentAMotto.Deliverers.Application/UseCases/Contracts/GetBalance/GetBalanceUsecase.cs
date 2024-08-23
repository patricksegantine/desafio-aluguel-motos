using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

public sealed class GetBalanceUsecase(IRentalContractRepository rentalRepository) : IGetBalanceUsecase
{
    private readonly IRentalContractRepository _rentalRepository = rentalRepository;

    public async Task<ErrorOr<GetBalanceResult>> Handle(GetBalanceRequest request, CancellationToken cancellationToken = default)
    {
        var rentalContract = await _rentalRepository.GetByIdAsync(request.Id, cancellationToken);
        if (rentalContract is null)
            return ErrorCatalog.RentalContractNotFound;

        var returnDate = request.ReturnDate ?? DateTime.UtcNow;

        if (rentalContract.Status == Domain.DomainObjects.Enums.RentalStatusType.Closed)
            return (GetBalanceResult)rentalContract;

        var (amount, fine) = rentalContract.CalculateAmountAndFineBalance(returnDate);
        var result = new GetBalanceResult
        {
            Id = rentalContract.Id,
            RentalAmount = amount,
            FineAmount = fine,
            TotalAmount = amount + fine
        };

        return result;
    }
}
