using FluentAssertions;
using Moq;
using Neleus.LambdaCompare;
using RentAMotto.Deliverers.Application.Tests.Builders;
using RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;
using RentAMotto.Domain;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;
using RentAMotto.Tests.Shared.Builders;
using System.Linq.Expressions;

namespace RentAMotto.Deliverers.Application.Tests.Usecases.Deliverers;

public class RegisterDeliveryDriverUsecaseTests
{
    private readonly Mock<IDeliveryDriverRepository> _deliveryDriverRepositoryMock;
    private readonly RegisterDeliveryDriverUsecase _usecase;

    public RegisterDeliveryDriverUsecaseTests()
    {
        _deliveryDriverRepositoryMock = new Mock<IDeliveryDriverRepository>();
        _usecase = new RegisterDeliveryDriverUsecase(_deliveryDriverRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenNameIsInvalid()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().WithName("A").Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCnpjIsInvalid()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().WithCnpj("123").Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenBirthdayIsInvalid()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().WithBirthday(DateTime.Now.AddYears(-101)).Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDrivingLicenceNumberIsInvalid()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().WithDrivingLicenceNumber("123").Build();

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCnpjIsAlreadyRegistered()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().Build();
        _deliveryDriverRepositoryMock
            .Setup(repo => repo.CountAsyc(It.IsAny<Expression<Func<DeliveryDriver, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.CnpjAlreadyRegisterd.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDrivingLicenceNumberIsAlreadyRegistered()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().Build();
        _deliveryDriverRepositoryMock.Setup(repo => repo.CountAsyc(It.Is<Expression<Func<DeliveryDriver, bool>>>(expr => Lambda.Eq(expr, dd => dd.Cnpj == request.Cnpj)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
        _deliveryDriverRepositoryMock.Setup(repo => repo.CountAsyc(It.Is<Expression<Func<DeliveryDriver, bool>>>(expr => Lambda.Eq(expr, dd => dd.DrivingLicenceNumber == request.DrivingLicenceNumber)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.DrivingLicenceAlreadyRegisterd.Code);
    }

    [Fact]
    public async Task Handle_ShouldRegisterDeliveryDriver_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterDeliveryDriverRequestBuilder().Build();
        _deliveryDriverRepositoryMock.Setup(repo => repo.CountAsyc(It.IsAny<Expression<Func<DeliveryDriver, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeFalse();
        _deliveryDriverRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<DeliveryDriver>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
