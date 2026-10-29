using System.Net.Http.Json;
using System.Text.Json;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<NotificationService> _logger;
    private readonly string _externalServiceUrl;
    private readonly int _maxRetries;
    private readonly int _timeoutSeconds;

    public NotificationService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<NotificationService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _externalServiceUrl = _configuration["NotificationService:ExternalUrl"] 
            ?? throw new ArgumentException("NotificationService:ExternalUrl no configurado");
        _maxRetries = _configuration.GetValue<int>("NotificationService:MaxRetries", 3);
        _timeoutSeconds = _configuration.GetValue<int>("NotificationService:TimeoutSeconds", 30);

        _httpClient.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);
    }

    public async Task<bool> SendAsync(string recipient, string subject, string message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(recipient))
        {
            _logger.LogWarning("Intento de enviar notificación a destinatario vacío");
            throw new ArgumentException("El destinatario no puede estar vacío", nameof(recipient));
        }

        if (string.IsNullOrWhiteSpace(subject))
        {
            _logger.LogWarning("Intento de enviar notificación con asunto vacío");
            throw new ArgumentException("El asunto no puede estar vacío", nameof(subject));
        }

        var notification = new
        {
            Recipient = recipient,
            Subject = subject,
            Message = message,
            Timestamp = DateTime.UtcNow,
            Priority = "normal"
        };

        return await SendWithRetryAsync(notification, cancellationToken);
    }

    public async Task<bool> SendBatchAsync(IEnumerable<string> recipients, string subject, string message, CancellationToken cancellationToken = default)
    {
        var recipientList = recipients?.ToList() ?? new List<string>();

        if (recipientList.Count == 0)
        {
            _logger.LogWarning("Intento de enviar notificación por lote sin destinatarios");
            return false;
        }

        var successfulSends = 0;
        var failedSends = 0;

        foreach (var recipient in recipientList)
        {
            try
            {
                var notification = new
                {
                    Recipient = recipient,
                    Subject = subject,
                    Message = message,
                    Timestamp = DateTime.UtcNow,
                    Priority = "batch"
                };

                var result = await SendWithRetryAsync(notification, cancellationToken);
                if (result)
                {
                    successfulSends++;
                }
                else
                {
                    failedSends++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar notificación a {Recipient}", recipient);
                failedSends++;
            }
        }

        _logger.LogInformation("Envío por lote completado: {Successful} exitosos, {Failed} fallidos de {Total}", 
            successfulSends, failedSends, recipientList.Count);

        return successfulSends > 0;
    }

    public async Task<string?> GetStatusAsync(string notificationId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(notificationId))
        {
            _logger.LogWarning("Consulta de estado con ID de notificación vacío");
            return null;
        }

        try
        {
            var response = await _httpClient.GetAsync(
                $"{_externalServiceUrl}/notifications/{notificationId}/status",
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var statusResponse = await response.Content.ReadFromJsonAsync<NotificationStatusResponse>(
                    cancellationToken: cancellationToken);
                return statusResponse?.Status;
            }

            _logger.LogWarning("Estado de notificación {NotificationId} no encontrado", notificationId);
            return null;
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "Timeout consultando estado de notificación {NotificationId}", notificationId);
            throw new TimeoutException($"Timeout al consultar estado de notificación {notificationId}", ex);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de HTTP consultando estado de notificación {NotificationId}", notificationId);
            throw;
        }
    }

    private async Task<bool> SendWithRetryAsync(object notification, CancellationToken cancellationToken)
    {
        var lastException = Exception();

        for (int attempt = 1; attempt <= _maxRetries; attempt++)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_externalServiceUrl}/notifications",
                    notification,
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Notificación enviada exitosamente en intento {Attempt}", attempt);
                    return true;
                }

                _logger.LogWarning("Intento {Attempt} falló con código {StatusCode}", 
                    attempt, response.StatusCode);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _logger.LogError("Notificación rechazada por el servicio externo");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                lastException = ex;
                _logger.LogWarning(ex, "Intento {Attempt} falló con error de HTTP", attempt);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                lastException = ex;
                _logger.LogWarning(ex, "Intento {Attempt} falló por timeout", attempt);
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("Solicitud cancelada después de {Attempt} intentos", attempt);
                throw;
            }

            if (attempt < _maxRetries)
            {
                var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
                _logger.LogInformation("Reintentando en {Delay} segundos", delay.TotalSeconds);
                await Task.Delay(delay, cancellationToken);
            }
        }

        _logger.LogError(lastException, "Todos los intentos de envío de notificación fallaron");
        return false;
    }

    private static Exception Exception()
    {
        return new Exception("Excepción placeholder para inicialización");
    }

    private class NotificationStatusResponse
    {
        public string? Status { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}