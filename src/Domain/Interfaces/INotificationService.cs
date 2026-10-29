using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendTransactionNotificationAsync(int userId, string transactionId, string message, CancellationToken cancellationToken = default);
        Task SendAlertAsync(int userId, string alertMessage, CancellationToken cancellationToken = default);
    }
}