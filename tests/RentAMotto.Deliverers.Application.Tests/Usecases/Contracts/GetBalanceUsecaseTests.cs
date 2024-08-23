using FluentAssertions;
using Moq;
using RentAMotto.Deliverers.Application.Tests.Builders;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;
using RentAMotto.Domain;
using RentAMotto.Domain.Entities;
using RentAMotto.Domain.Repositories;
using RentAMotto.Tests.Shared.Builders;

namespace RentAMotto.Deliverers.Application.Tests.Usecases.Contracts;

public class GetBalanceUsecaseTests
{
    private readonly Mock<IRentalContractRepository> _rentalRepositoryMock;
    private readonly GetBalanceUsecase _usecase;

    public GetBalanceUsecaseTests()
    {
        _rentalRepositoryMock = new Mock<IRentalContractRepository>();
        _usecase = new GetBalanceUsecase(_rentalRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRentalContractNotFound()
    {
        // Arrange
        var request = new GetBalanceRequestBuilder().Build();
        _rentalRepositoryMock.Setup(repo => repo.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((RentalContract?)null);

        // Act
        var result = await _usecase.Handle(request);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == ErrorCatalog.RentalContractNotFound.Code);
    }
}
