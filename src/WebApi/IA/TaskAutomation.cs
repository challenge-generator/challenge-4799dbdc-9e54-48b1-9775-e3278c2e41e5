using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace WebApi.IA;

public class TaskAutomation
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly INotificationService _notificationService;
    private readonly HttpClient _aiClient;
    private readonly ILogger<TaskAutomation> _logger;

    public TaskAutomation(
        ITransactionRepository transactionRepository,
        INotificationService notificationService,
        HttpClient aiClient,
        ILogger<TaskAutomation> logger)
    {
        _transactionRepository = transactionRepository;
        _notificationService = notificationService;
        _aiClient = aiClient;
        _logger = logger;
    }

    public async Task<List<AutomatableTask>> IdentifyAutomatableTasksAsync(
        IEnumerable<Transaction> recentTransactions,
        CancellationToken cancellationToken = default)
    {
        var automatableTasks = new List<AutomatableTask>();
        var transactionGroups = recentTransactions
            .GroupBy(t => new { t.Type, t.Status, t.Amount.Range })
            .Where(g => g.Count() >= 10)
            .ToList();

        foreach (var group in transactionGroups)
        {
            var pattern = await DetectPatternAsync(group.ToList(), cancellationToken);
            if (pattern.Confidence > 0.75m)
            {
                automatableTasks.Add(new AutomatableTask
                {
                    TaskName = $"Procesar transacciones {group.Key.Type} - {group.Key.Status}",
                    Pattern = pattern,
                    EstimatedTimeSaved = group.Count() * 500,
                    Frequency = group.Count()
                });
            }
        }

        _logger.LogInformation(
            "Identificadas {Count} tareas automatizables de {Total} transacciones analizadas",
            automatableTasks.Count,
            recentTransactions.Count());

        return automatableTasks;
    }

    public async Task<AutomationResult> ExecuteAutomatedTaskAsync(
        AutomatableTask task,
        CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        var processedCount = 0;
        var errors = new List<string>();

        try
        {
            var transactions = await _transactionRepository
                .GetByTypeAndStatusAsync(
                    Enum.Parse<TransactionType>(task.Pattern.TransactionType),
                    Enum.Parse<TransactionStatus>(task.Pattern.TransactionStatus),
                    cancellationToken);

            foreach (var transaction in transactions.Take(1000))
            {
                try
                {
                    await ProcessWithAutomationRulesAsync(transaction, task.Pattern, cancellationToken);
                    processedCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Error en TX {transaction.Id}: {ex.Message}");
                    _logger.LogWarning(ex, "Error procesando transacción {TransactionId}", transaction.Id);
                }
            }

            var duration = DateTime.UtcNow - startTime;
            var result = new AutomationResult
            {
                TaskName = task.TaskName,
                ProcessedCount = processedCount,
                ErrorCount = errors.Count,
                DurationMs = duration.TotalMilliseconds,
                TimeSavedMs = task.EstimatedTimeSaved,
                Success = errors.Count == 0
            };

            await _notificationService.SendAsync(
                "automation@company.com",
                $"Automatización completada: {task.TaskName}",
                JsonSerializer.Serialize(result),
                cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falló la automatización de {TaskName}", task.TaskName);
            return new AutomationResult
            {
                TaskName = task.TaskName,
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private async Task<TransactionPattern> DetectPatternAsync(
        List<Transaction> transactions,
        CancellationToken cancellationToken)
    {
        var request = new
        {
            prompt = $"Analiza estos {transactions.Count} transacciones y detecta patrones repetitivos. " +
                     "Transacciones: {JsonSerializer.Serialize(transactions.Take(5))}",
            max_tokens = 500
        };

        try
        {
            var response = await _aiClient.PostAsJsonAsync("/analyze-pattern", request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<PatternAnalysisResponse>(cancellationToken: cancellationToken);

            return new TransactionPattern
            {
                TransactionType = result?.DetectedType ?? "Unknown",
                TransactionStatus = result?.DetectedStatus ?? "Unknown",
                Confidence = result?.Confidence ?? 0.5m
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Falló análisis con IA, usando heurística local");
            return DetectPatternLocally(transactions);
        }
    }

    private TransactionPattern DetectPatternLocally(List<Transaction> transactions)
    {
        var mostCommonType = transactions
            .GroupBy(t => t.Type)
            .OrderByDescending(g => g.Count())
            .First().Key;

        var mostCommonStatus = transactions
            .GroupBy(t => t.Status)
            .OrderByDescending(g => g.Count())
            .First().Key;

        return new TransactionPattern
        {
            TransactionType = mostCommonType.ToString(),
            TransactionStatus = mostCommonStatus.ToString(),
            Confidence = 0.85m
        };
    }

    private async Task ProcessWithAutomationRulesAsync(
        Transaction transaction,
        TransactionPattern pattern,
        CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        _logger.LogDebug("Aplicando reglas de automatización a {TransactionId}", transaction.Id);
    }
}

public class AutomatableTask
{
    public string TaskName { get; set; } = string.Empty;
    public TransactionPattern Pattern { get; set; } = new();
    public int EstimatedTimeSaved { get; set; }
    public int Frequency { get; set; }
}

public class TransactionPattern
{
    public string TransactionType { get; set; } = string.Empty;
    public string TransactionStatus { get; set; } = string.Empty;
    public decimal Confidence { get; set; }
}

public class AutomationResult
{
    public string TaskName { get; set; } = string.Empty;
    public int ProcessedCount { get; set; }
    public int ErrorCount { get; set; }
    public double DurationMs { get; set; }
    public int TimeSavedMs { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public class PatternAnalysisResponse
{
    public string? DetectedType { get; set; }
    public string? DetectedStatus { get; set; }
    public decimal Confidence { get; set; }
}

public static class AmountRangeExtension
{
    public static string Range(this decimal amount)
    {
        return amount switch
        {
            < 100 => "small",
            < 1000 => "medium",
            < 10000 => "large",
            _ => "xlarge"
        };
    }
}