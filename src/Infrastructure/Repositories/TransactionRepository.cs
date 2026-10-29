using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(AppDbContext context, ILogger<TransactionRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener una transacción con ID vacío");
            return null;
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (transaction == null)
        {
            _logger.LogInformation("Transacción con ID {TransactionId} no encontrada", id);
        }

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        transaction.Id = Guid.NewGuid();
        transaction.CreatedAt = DateTime.UtcNow;
        transaction.UpdatedAt = DateTime.UtcNow;

        if (transaction.Status == TransactionStatus.Pending)
        {
            transaction.ProcessedAt = null;
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} creada exitosamente", transaction.Id);
        return transaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        var existingTransaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == transaction.Id, cancellationToken);

        if (existingTransaction == null)
        {
            _logger.LogError("No se puede actualizar. Transacción {TransactionId} no encontrada", transaction.Id);
            throw new InvalidOperationException($"Transacción con ID {transaction.Id} no encontrada");
        }

        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Description = transaction.Description;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Status = transaction.Status;
        existingTransaction.UpdatedAt = DateTime.UtcNow;

        if (transaction.Status == TransactionStatus.Completed && existingTransaction.ProcessedAt == null)
        {
            existingTransaction.ProcessedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} actualizada exitosamente", transaction.Id);
        return existingTransaction;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó eliminar una transacción con ID vacío");
            return false;
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (transaction == null)
        {
            _logger.LogWarning("Transacción {TransactionId} no encontrada para eliminación", id);
            return false;
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} eliminada exitosamente", id);
        return true;
    }

    public async Task<IEnumerable<Transaction>> GetByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        if (startDate > endDate)
        {
            _logger.LogWarning("Fecha de inicio {StartDate} es mayor que fecha de fin {EndDate}", startDate, endDate);
            throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin");
        }

        return await _context.Transactions
            .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .CountAsync(t => t.Status == status, cancellationToken);
    }

    public async Task<decimal> GetTotalAmountByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .Where(t => t.Status == status)
            .SumAsync(t => t.Amount, cancellationToken);
    }
}