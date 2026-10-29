using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        }
    }

    public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("TransactionId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(t => t.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.Currency)
                .HasColumnName("Currency")
                .HasColumnType("nvarchar(3)")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(t => t.SourceAccountId)
                .HasColumnName("SourceAccountId")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.TargetAccountId)
                .HasColumnName("TargetAccountId")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.TransactionType)
                .HasColumnName("TransactionType")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(t => t.ProcessedAt)
                .HasColumnName("ProcessedAt")
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(t => t.FailureReason)
                .HasColumnName("FailureReason")
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(t => t.ReferenceNumber)
                .HasColumnName("ReferenceNumber")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasIndex(t => t.SourceAccountId)
                .HasDatabaseName("IX_Transactions_SourceAccountId");

            builder.HasIndex(t => t.TargetAccountId)
                .HasDatabaseName("IX_Transactions_TargetAccountId");

            builder.HasIndex(t => t.Status)
                .HasDatabaseName("IX_Transactions_Status");

            builder.HasIndex(t => t.CreatedAt)
                .HasDatabaseName("IX_Transactions_CreatedAt");

            builder.HasIndex(t => t.ReferenceNumber)
                .HasDatabaseName("IX_Transactions_ReferenceNumber")
                .IsUnique();

            builder.HasIndex(t => new { t.SourceAccountId, t.CreatedAt })
                .HasDatabaseName("IX_Transactions_SourceAccountId_CreatedAt");

            builder.HasIndex(t => new { t.TargetAccountId, t.CreatedAt })
                .HasDatabaseName("IX_Transactions_TargetAccountId_CreatedAt");

            builder.HasCheckConstraint("CK_Transaction_Amount_Positive", "Amount > 0");

            builder.HasCheckConstraint("CK_Transaction_CreatedAt_NotFuture", "CreatedAt <= GETUTCDATE()");
        }
    }
}