using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Transaction> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Transaction>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Transaction>> GetByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> TransactionExistsAsync(string transactionId, CancellationToken cancellationToken = default);
    }
}