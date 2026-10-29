using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El identificador de cuenta de origen es obligatorio")]
    public Guid SourceAccountId { get; set; }

    [Required(ErrorMessage = "El identificador de cuenta de destino es obligatorio")]
    public Guid DestinationAccountId { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "El tipo de transacción es obligatorio")]
    public TransactionType Type { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public TransactionStatus Status { get; set; }

    public string? FailureReason { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public TransactionDto()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = TransactionStatus.Pending;
    }

    public Transaction ToEntity()
    {
        return new Transaction
        {
            Id = this.Id,
            SourceAccountId = this.SourceAccountId,
            DestinationAccountId = this.DestinationAccountId,
            Amount = this.Amount,
            Type = this.Type,
            Description = this.Description,
            CreatedAt = this.CreatedAt,
            Status = this.Status,
            FailureReason = this.FailureReason,
            ProcessedAt = this.ProcessedAt
        };
    }

    public static TransactionDto FromEntity(Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            SourceAccountId = transaction.SourceAccountId,
            DestinationAccountId = transaction.DestinationAccountId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Description = transaction.Description,
            CreatedAt = transaction.CreatedAt,
            Status = transaction.Status,
            FailureReason = transaction.FailureReason,
            ProcessedAt = transaction.ProcessedAt
        };
    }

    public bool IsValidForProcessing()
    {
        return SourceAccountId != Guid.Empty
            && DestinationAccountId != Guid.Empty
            && Amount > 0
            && SourceAccountId != DestinationAccountId;
    }

    public void MarkAsProcessed()
    {
        Status = TransactionStatus.Completed;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = TransactionStatus.Failed;
        FailureReason = reason;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkAsRejected(string reason)
    {
        Status = TransactionStatus.Rejected;
        FailureReason = reason;
        ProcessedAt = DateTime.UtcNow;
    }
}

public class CreateTransactionRequest
{
    [Required(ErrorMessage = "El identificador de cuenta de origen es obligatorio")]
    public Guid SourceAccountId { get; set; }

    [Required(ErrorMessage = "El identificador de cuenta de destino es obligatorio")]
    public Guid DestinationAccountId { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "El tipo de transacción es obligatorio")]
    public TransactionType Type { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Description { get; set; }
}

public class TransactionResponse
{
    public Guid Id { get; set; }
    public Guid SourceAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? FailureReason { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public static TransactionResponse FromDto(TransactionDto dto)
    {
        return new TransactionResponse
        {
            Id = dto.Id,
            SourceAccountId = dto.SourceAccountId,
            DestinationAccountId = dto.DestinationAccountId,
            Amount = dto.Amount,
            Type = dto.Type.ToString(),
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            Status = dto.Status.ToString(),
            FailureReason = dto.FailureReason,
            ProcessedAt = dto.ProcessedAt
        };
    }
}