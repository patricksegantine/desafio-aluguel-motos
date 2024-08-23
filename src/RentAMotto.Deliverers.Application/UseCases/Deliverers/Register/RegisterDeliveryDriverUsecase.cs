using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;

public sealed class RegisterDeliveryDriverUsecase(IDeliveryDriverRepository deliveryDriverRepository) : IRegisterDeliveryDriverUsecase
{
    private readonly IDeliveryDriverRepository _deliveryDriverRepository = deliveryDriverRepository;

    public async Task<ErrorOr<RegisterDeliveryDriverResult>> Handle(RegisterDeliveryDriverRequest request, CancellationToken cancellationToken = default)
    {
        var (canRegister, errors) = await CanBeRegisterd(request, cancellationToken);
        if (!canRegister)
            return errors!;

        var deliveryDriver = DeliveryDriver.Create(
            name: request.Name,
            cnpj: request.Cnpj,
            birthday: request.Birthday,
            drivingLicenceNumber: request.DrivingLicenceNumber,
            drivingLicenceType: request.DrivingLicenceType);

        await _deliveryDriverRepository.AddAsync(deliveryDriver, cancellationToken);

        return new RegisterDeliveryDriverResult { Id = deliveryDriver.Id };
    }

    public async Task<(bool, List<Error>?)> CanBeRegisterd(RegisterDeliveryDriverRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await new Validator().ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
            return (false, validatorResult.Errors.ToErrorList());

        var cnpjRegisterd = await _deliveryDriverRepository.CountAsyc(dd => dd.Cnpj == request.Cnpj, cancellationToken);
        if (cnpjRegisterd > 0)
            return (false, [ErrorCatalog.CnpjAlreadyRegisterd]);

        var drivingLicenceRegisterd = await _deliveryDriverRepository.CountAsyc(dd => dd.DrivingLicenceNumber == request.DrivingLicenceNumber, cancellationToken);
        if (drivingLicenceRegisterd > 0)
            return (false, [ErrorCatalog.DrivingLicenceAlreadyRegisterd]);

        return (true, null);
    }
}
