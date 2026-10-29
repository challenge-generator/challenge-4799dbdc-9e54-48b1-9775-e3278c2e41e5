using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Infrastructure;

public class TransactionRepositoryTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ValidTransaction_PersistsToDatabase()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Pending,
            Amount = 1500m,
            Description = "Test persistence",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await repository.AddAsync(transaction, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        var saved = await context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
        Assert.NotNull(saved);
        Assert.Equal(TransactionType.Transfer, saved.Type);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingTransaction_ReturnsTransaction()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Completed,
            Amount = 500m,
            Description = "Test retrieval",
            CreatedAt = DateTime.UtcNow
        };
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(transaction.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact]
    public async Task GetByTypeAndStatusAsync_ReturnsMatchingTransactions()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        var t1 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Transfer, Status = TransactionStatus.Completed, Amount = 100m, CreatedAt = DateTime.UtcNow };
        var t2 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Transfer, Status = TransactionStatus.Completed, Amount = 200m, CreatedAt = DateTime.UtcNow };
        var t3 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Payment, Status = TransactionStatus.Completed, Amount = 300m, CreatedAt = DateTime.UtcNow };

        await context.Transactions.AddRangeAsync(t1, t2, t3);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByTypeAndStatusAsync(
            TransactionType.Transfer,
            TransactionStatus.Completed,
            CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ExistsAsync_ExistingId_ReturnsTrue()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Pending,
            Amount = 1000m,
            CreatedAt = DateTime.UtcNow
        };
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        // Act
        var exists = await repository.ExistsAsync(transaction.Id, CancellationToken.None);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_NonExistingId_ReturnsFalse()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        // Act
        var exists = await repository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.False(exists);
    }
}