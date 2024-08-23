using ErrorOr;
using MassTransit;
using RentAMotto.Domain;
using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Events;
using RentAMotto.Domain.Repositories;
using System.Threading;

namespace RentAMotto.Admin.Application.UseCases.Motorcycle.Create;

public sealed class CreateMotorcycleUsecase(
    IVehicleRepository vehicleRepository,
    IPublishEndpoint publishEndpoint) : ICreateMotorcycleUsecase
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    private const int YEAR_OF_MANUFACTURE_TO_NOTIFY = 2024;

    public async Task<ErrorOr<CreateMotorcycleResult>> Handle(CreateMotorcycleRequest request, CancellationToken cancellationToken = default)
    {
        var (canRegister, errors) = await CanBeCreated(request, cancellationToken);
        if (!canRegister)
            return errors!;

        var vehicle = Vehicle.CreateMotorcycle(
            request.Make,
            request.Model,
            request.YearOfManufacture,
            request.NumberPlate);

        await _vehicleRepository.AddAsync(vehicle, cancellationToken);

        if (vehicle.YearOfManufacture == YEAR_OF_MANUFACTURE_TO_NOTIFY)
            await SendMessage(vehicle, cancellationToken);

        return new CreateMotorcycleResult { Id = vehicle.Id };
    }

    public async Task<(bool, List<Error>?)> CanBeCreated(CreateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await new Validator().ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
            return (false, validatorResult.Errors.ToErrorList());

        var numberPlateRegisterd = await _vehicleRepository.CountAsync(VehicleType.Motorcycle, request.NumberPlate, cancellationToken);
        if (numberPlateRegisterd > 0)
            return (false, [ErrorCatalog.VehicleNumberPlateAlreadyRegisterd]);

        return (true, null);
    }

    private async Task SendMessage(Vehicle vehicle, CancellationToken cancellationToken)
    {
        var message = new VehicleCreatedEvent
        {
            Id = vehicle.Id,
            Type = vehicle.Type,
            Make = vehicle.Make,
            Model = vehicle.Model,
            NumberPlate = vehicle.NumberPlate,
            YearOfManufacture = vehicle.YearOfManufacture,
            CreatedDate = vehicle.CreatedDate,
        };
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}
