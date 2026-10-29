using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }

        [Required]
        [StringLength(36)]
        public string TransactionId { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Amount { get; private set; }

        [Required]
        public TransactionType Type { get; private set; }

        [Required]
        public TransactionStatus Status { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        [Required]
        public DateTime UpdatedAt { get; private set; }

        [StringLength(200)]
        public string Description { get; private set; }

        [StringLength(50)]
        public string Reference { get; private set; }

        public Transaction()
        {
            // Constructor necesario para EF Core
        }

        public Transaction(string transactionId, int userId, decimal amount, TransactionType type, string description = null, string reference = null)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("El ID de transacción no puede ser nulo o vacío.", nameof(transactionId));

            if (userId <= 0)
                throw new ArgumentException("El ID de usuario debe ser mayor que cero.", nameof(userId));

            if (amount <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.", nameof(amount));

            TransactionId = transactionId;
            UserId = userId;
            Amount = amount;
            Type = type;
            Description = description;
            Reference = reference;
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(TransactionStatus newStatus)
        {
            if (newStatus == Status)
                return;

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string newDescription)
        {
            if (!string.IsNullOrWhiteSpace(newDescription) && newDescription.Length > 200)
                throw new ArgumentException("La descripción no puede exceder 200 caracteres.", nameof(newDescription));

            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateReference(string newReference)
        {
            if (!string.IsNullOrWhiteSpace(newReference) && newReference.Length > 50)
                throw new ArgumentException("La referencia no puede exceder 50 caracteres.", nameof(newReference));

            Reference = newReference;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Payment,
        Refund
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Reversed,
        Cancelled
    }
}
"