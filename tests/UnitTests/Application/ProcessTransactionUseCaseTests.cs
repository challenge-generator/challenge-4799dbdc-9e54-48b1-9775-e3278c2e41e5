using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Application;

public class ProcessTransactionUseCaseTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly ProcessTransactionUseCase _sut;

    public ProcessTransactionUseCaseTests()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _notificationServiceMock = new Mock<INotificationService>();
        _sut = new ProcessTransactionUseCase(
            _transactionRepositoryMock.Object,
            _notificationServiceMock.Object);
    }

    [Fact]
    public async Task ProcessAsync_ValidTransaction_ReturnsSuccess()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Pending,
            Amount = 1000m,
            Description = "Test transaction"
        };

        _transactionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        // Act
        var result = await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task ProcessAsync_DuplicateTransaction_ReturnsFailure()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Pending,
            Amount = 500m,
            Description = "Duplicate test"
        };

        _transactionRepositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task ProcessAsync_ValidatesAmount_ThrowsWhenNegative()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Pending,
            Amount = -100m,
            Description = "Invalid amount"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await _sut.ProcessAsync(transaction, CancellationToken.None));
    }

    [Fact]
    public async Task ProcessAsync_SendsNotification_OnSuccess()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Pending,
            Amount = 2000m,
            Description = "Notification test"
        };

        _transactionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        // Act
        await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        _notificationServiceMock.Verify(
            n => n.SendAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}