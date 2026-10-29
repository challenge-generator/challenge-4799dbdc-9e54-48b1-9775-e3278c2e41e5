using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UseCases;

public class ProcessTransactionCommand : IRequest<TransactionDto>
{
    public Guid SourceAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string? Description { get; set; }

    public ProcessTransactionCommand(
        Guid sourceAccountId,
        Guid destinationAccountId,
        decimal amount,
        TransactionType type,
        string? description = null)
    {
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
        Type = type;
        Description = description;
    }
}

public class ProcessTransactionUseCase : IRequestHandler<ProcessTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly INotificationService _notificationService;
    private readonly ILogger<ProcessTransactionUseCase> _logger;

    public ProcessTransactionUseCase(
        ITransactionRepository transactionRepository,
        INotificationService notificationService,
        ILogger<ProcessTransactionUseCase> logger)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TransactionDto> Handle(ProcessTransactionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando procesamiento de transacción: {Source} -> {Destination}, Monto: {Amount}",
            request.SourceAccountId, request.DestinationAccountId, request.Amount);

        var transactionDto = new TransactionDto
        {
            SourceAccountId = request.SourceAccountId,
            DestinationAccountId = request.DestinationAccountId,
            Amount = request.Amount,
            Type = request.Type,
            Description = request.Description,
            Status = TransactionStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        if (!transactionDto.IsValidForProcessing())
        {
            _logger.LogWarning("Validación de transacción fallida: cuentas inválidas o monto incorrecto");
            transactionDto.MarkAsRejected("Cuentas inválidas o monto incorrecto");
            await _transactionRepository.AddAsync(transactionDto.ToEntity(), cancellationToken);
            return transactionDto;
        }

        try
        {
            var existingTransaction = await _transactionRepository
                .GetByIdAsync(transactionDto.Id, cancellationToken);

            if (existingTransaction != null)
            {
                _logger.LogWarning("Transacción duplicada detectada: {TransactionId}", transactionDto.Id);
                transactionDto.MarkAsRejected("Transacción duplicada");
                return transactionDto;
            }

            await _transactionRepository.AddAsync(transactionDto.ToEntity(), cancellationToken);
            _logger.LogInformation("Transacción creada con estado Pending: {TransactionId}", transactionDto.Id);

            await ProcessTransactionLogic(transactionDto, cancellationToken);

            if (transactionDto.Status == TransactionStatus.Completed)
            {
                await _notificationService.SendTransactionCompletedNotificationAsync(
                    transactionDto.Id,
                    transactionDto.SourceAccountId,
                    transactionDto.DestinationAccountId,
                    cancellationToken);

                _logger.LogInformation("Transacción completada exitosamente: {TransactionId}", transactionDto.Id);
            }
        }
        catch (InsufficientFundsException ex)
        {
            _logger.LogError(ex, "Fondos insuficientes para la transacción: {TransactionId}", transactionDto.Id);
            transactionDto.MarkAsFailed($"Fondos insuficientes: {ex.Message}");
            await _transactionRepository.UpdateAsync(transactionDto.ToEntity(), cancellationToken);
            await _notificationService.SendTransactionFailedNotificationAsync(
                transactionDto.Id,
                transactionDto.SourceAccountId,
                ex.Message,
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al procesar transacción: {TransactionId}", transactionDto.Id);
            transactionDto.MarkAsFailed($"Error de procesamiento: {ex.Message}");
            await _transactionRepository.UpdateAsync(transactionDto.ToEntity(), cancellationToken);
            await _notificationService.SendTransactionFailedNotificationAsync(
                transactionDto.Id,
                transactionDto.SourceAccountId,
                ex.Message,
                cancellationToken);
        }

        return transactionDto;
    }

    private async Task ProcessTransactionLogic(TransactionDto transaction, CancellationToken cancellationToken)
    {
        switch (transaction.Type)
        {
            case TransactionType.Transfer:
                await ProcessTransfer(transaction, cancellationToken);
                break;
            case TransactionType.Payment:
                await ProcessPayment(transaction, cancellationToken);
                break;
            case TransactionType.Deposit:
                await ProcessDeposit(transaction, cancellationToken);
                break;
            case TransactionType.Withdrawal:
                await ProcessWithdrawal(transaction, cancellationToken);
                break;
            default:
                _logger.LogWarning("Tipo de transacción desconocido: {Type}", transaction.Type);
                transaction.MarkAsRejected($"Tipo de transacción no soportado: {transaction.Type}");
                break;
        }
    }

    private async Task ProcessTransfer(TransactionDto transaction, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Procesando transferencia: {TransactionId}", transaction.Id);
        
        var sourceBalance = await GetAccountBalanceAsync(transaction.SourceAccountId, cancellationToken);
        if (sourceBalance < transaction.Amount)
        {
            throw new InsufficientFundsException(
                $"Saldo insuficiente. Disponible: {sourceBalance}, Requerido: {transaction.Amount}");
        }

        await DebitAccountAsync(transaction.SourceAccountId, transaction.Amount, cancellationToken);
        await CreditAccountAsync(transaction.DestinationAccountId, transaction.Amount, cancellationToken);
        
        transaction.MarkAsProcessed();
        _logger.LogInformation("Transferencia procesada: {TransactionId}", transaction.Id);
    }

    private async Task ProcessPayment(TransactionDto transaction, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Procesando pago: {TransactionId}", transaction.Id);
        
        var sourceBalance = await GetAccountBalanceAsync(transaction.SourceAccountId, cancellationToken);
        if (sourceBalance < transaction.Amount)
        {
            throw new InsufficientFundsException(
                $"Saldo insuficiente para pago. Disponible: {sourceBalance}, Requerido: {transaction.Amount}");
        }

        await DebitAccountAsync(transaction.SourceAccountId, transaction.Amount, cancellationToken);
        
        transaction.MarkAsProcessed();
        _logger.LogInformation("Pago procesado: {TransactionId}", transaction.Id);
    }

    private async Task ProcessDeposit(TransactionDto transaction, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Procesando depósito: {TransactionId}", transaction.Id);
        
        await CreditAccountAsync(transaction.DestinationAccountId, transaction.Amount, cancellationToken);
        
        transaction.MarkAsProcessed();
        _logger.LogInformation("Depósito procesado: {TransactionId}", transaction.Id);
    }

    private async Task ProcessWithdrawal(TransactionDto transaction, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Procesando retiro: {TransactionId}", transaction.Id);
        
        var sourceBalance = await GetAccountBalanceAsync(transaction.SourceAccountId, cancellationToken);
        if (sourceBalance < transaction.Amount)
        {
            throw new InsufficientFundsException(
                $"Saldo insuficiente para retiro. Disponible: {sourceBalance}, Requerido: {transaction.Amount}");
        }

        await DebitAccountAsync(transaction.SourceAccountId, transaction.Amount, cancellationToken);
        
        transaction.MarkAsProcessed();
        _logger.LogInformation("Retiro procesado: {TransactionId}", transaction.Id);
    }

    private async Task<decimal> GetAccountBalanceAsync(Guid accountId, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        return 10000.00m;
    }

    private async Task DebitAccountAsync(Guid accountId, decimal amount, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Débito de {Amount} de cuenta {AccountId}", amount, accountId);
        await Task.Delay(5, cancellationToken);
    }

    private async Task CreditAccountAsync(Guid accountId, decimal amount, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Crédito de {Amount} a cuenta {AccountId}", amount, accountId);
        await Task.Delay(5, cancellationToken);
    }
}

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
    public InsufficientFundsException(string message, Exception innerException) : base(message, innerException) { }
}

public class DuplicateTransactionException : Exception
{
    public DuplicateTransactionException(string message) : base(message) { }
    public DuplicateTransactionException(string message, Exception innerException) : base(message, innerException) { }
}

public class InvalidTransactionException : Exception
{
    public InvalidTransactionException(string message) : base(message) { }
    public InvalidTransactionException(string message, Exception innerException) : base(message, innerException) { }
}