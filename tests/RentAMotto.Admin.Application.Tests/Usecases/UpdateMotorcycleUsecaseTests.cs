using FluentAssertions;
using Moq;
using RentAMotto.Admin.Application.Tests.Builders;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Update;
using RentAMotto.Domain;
using RentAMotto.Domain.DomainObjects.Enums;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Admin.Application.Tests.Usecases;

public class UpdateMotorcycleUsecaseTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly UpdateMotorcycleUsecase _usecase;

    public UpdateMotorcycleUsecaseTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _usecase = new UpdateMotorcycleUsecase(_vehicleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenVehicleNotFound()
    {
        // Arrange
        var request = new UpdateMotorcycleRequestBuilder().Build();
        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Vehicle?)null);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleNotFound.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenNumberPlateIsEmpty()
    {
        // Arrange
        var request = new UpdateMotorcycleRequestBuilder().WithNumberPlate(string.Empty).Build();
        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Vehicle());

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
        var request = new UpdateMotorcycleRequestBuilder().Build();
        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Vehicle());
        _vehicleRepositoryMock.Setup(repo => repo.CountAsync(VehicleType.Motorcycle, request.NumberPlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.VehicleNumberPlateAlreadyRegisterd.Code);
    }

    [Fact]
    public async Task Handle_ShouldUpdateMotorcycle_WhenRequestIsValid()
    {
        // Arrange
        var request = new UpdateMotorcycleRequestBuilder().Build();
        var vehicle = new Vehicle();
        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(vehicle);
        _vehicleRepositoryMock.Setup(repo => repo.CountAsync(VehicleType.Motorcycle, request.NumberPlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeFalse();
        _vehicleRepositoryMock.Verify(repo => repo.UpdateAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
    }
}
