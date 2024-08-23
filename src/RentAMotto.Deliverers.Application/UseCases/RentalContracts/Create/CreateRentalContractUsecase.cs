using ErrorOr;
using RentAMotto.Domain;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;

public sealed class CreateRentalContractUsecase(
    IVehicleRepository vehicleRepository,
    IDeliveryDriverRepository deliveryDriverRepository,
    IRentalPlanRepository rentalPlanRepository,
    IRentalContractRepository rentalRepository) : ICreateRentalContractUsecase
{
    public readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    public readonly IDeliveryDriverRepository _deliveryDriverRepository = deliveryDriverRepository;
    public readonly IRentalPlanRepository _rentalPlanRepository = rentalPlanRepository;
    public readonly IRentalContractRepository _rentalRepository = rentalRepository;

    public async Task<ErrorOr<CreateRentalContractResult>> Handle(CreateRentalContractRequest request, CancellationToken cancellationToken = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
        if (!CanVehicleBeRented(vehicle, out var vehicleError))
            return vehicleError;

        var deliveryDriver = await _deliveryDriverRepository.GetByIdAsync(request.DeliveryDriverId, cancellationToken);
        if (!CanDeliveryDriverRent(deliveryDriver, out var deliveryDriverError))
            return deliveryDriverError;

        var rentalPlan = await _rentalPlanRepository.GetByIdAsync(request.RentalPlanId, cancellationToken);
        if (!CanRentalPlanBeUsed(rentalPlan, out var rentalPlanError))
            return rentalPlanError;

        var rent = RentalContract.Create(vehicle!, deliveryDriver!, rentalPlan!, request.ExpectedEndDate);

        await _rentalRepository.AddAsync(rent, cancellationToken);

        var result = (CreateRentalContractResult)rent;
        return result;
    }

    /// <summary>
    /// Valida se o veículo pode ser locado
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    private static bool CanVehicleBeRented(Vehicle? vehicle, out Error error)
    {
        if (vehicle is null)
        {
            error = ErrorCatalog.VehicleNotFound;
            return false;
        }

        if (vehicle.Status == Domain.DomainObjects.Enums.StatusType.Inactive)
        {
            error = ErrorCatalog.VehicleUnavailable;
            return false;
        }

        error = default;
        return true;
    }

    /// <summary>
    /// Valida o tipo de habilitação do entregador.
    /// Somente entregadores habilitados na categoria A podem efetuar uma locação
    /// </summary>
    /// <param name="deliveryDriver"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    private static bool CanDeliveryDriverRent(DeliveryDriver? deliveryDriver, out Error error)
    {
        if (deliveryDriver is null)
        {
            error = ErrorCatalog.DeliveryDriverNotFound;
            return false;
        }

        if (deliveryDriver.DrivingLicenceType != Domain.DomainObjects.Enums.DrivingLicenceType.A &&
            deliveryDriver.DrivingLicenceType != Domain.DomainObjects.Enums.DrivingLicenceType.AB)
        {
            error = ErrorCatalog.InvalidDrivingLicenceType;
            return false;
        }

        error = default;
        return true;
    }

    /// <summary>
    /// Valida o status do plano
    /// </summary>
    /// <param name="rentalPlan"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    private static bool CanRentalPlanBeUsed(RentalPlan? rentalPlan, out Error error)
    {
        if (rentalPlan is null)
        {
            error = ErrorCatalog.RentalPlanNotFound;
            return false;
        }

        if (rentalPlan.Status == Domain.DomainObjects.Enums.StatusType.Inactive)
        {
            error = ErrorCatalog.RentalPlanUnavailable;
            return false;
        }

        error = default;
        return true;
    }
}
