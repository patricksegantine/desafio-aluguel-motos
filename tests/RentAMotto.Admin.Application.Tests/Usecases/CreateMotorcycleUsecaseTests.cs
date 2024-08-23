using FluentAssertions;
using MassTransit;
using Moq;
using RentAMotto.Admin.Application.Tests.Builders;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Create;
using RentAMotto.Domain;
using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Admin.Application.Tests.Usecases;

public class CreateMotorcycleUsecaseTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly CreateMotorcycleUsecase _usecase;

    public CreateMotorcycleUsecaseTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _usecase = new CreateMotorcycleUsecase(_vehicleRepositoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenMakeIsEmpty()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().WithMake(string.Empty).Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleMakeMustNotBeEmpty.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenModelIsEmpty()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().WithModel(string.Empty).Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleModelMustNotBeEmpty.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenYearOfManufactureIsOld()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().WithYearOfManufacture(DateTime.Now.Year - 6).Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleYearOfManufactureMustNotBeOldThan(5).Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenNumberPlateIsEmpty()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().WithNumberPlate(string.Empty).Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleNumberPlateMustNotBeEmpty.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenNumberPlateIsAlreadyRegistered()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().Build();
        _vehicleRepositoryMock.Setup(repo => repo.CountAsync(VehicleType.Motorcycle, request.NumberPlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleNumberPlateAlreadyRegisterd.Code);
    }

    [Fact]
    public async Task Handle_ShouldCreateMotorcycle_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateMotorcycleRequestBuilder().Build();
        _vehicleRepositoryMock.Setup(repo => repo.CountAsync(VehicleType.Motorcycle, request.NumberPlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeFalse();
        _vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
