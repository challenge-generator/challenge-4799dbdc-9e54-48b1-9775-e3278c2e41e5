using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace WebApi.IA
{
    public class CodeOptimizer
    {
        private readonly HttpClient _httpClient;
        private readonly IAConfiguration _configuration;
        private readonly ILogger<CodeOptimizer> _logger;
        private readonly Dictionary<string, CachedOptimization> _cache;
        private readonly SemaphoreSlim _semaphore;
        
        public CodeOptimizer(
            HttpClient httpClient,
            IAConfiguration configuration,
            ILogger<CodeOptimizer> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            if (!_configuration.ValidateConfiguration())
            {
                throw new InvalidOperationException(
                    "La configuración de IA no es válida. Verifique los parámetros en appsettings.json");
            }
            
            _cache = new Dictionary<string, CachedOptimization>();
            _semaphore = new SemaphoreSlim(_configuration.MaxRetries, _configuration.MaxRetries);
            
            ConfigureHttpClient();
        }
        
        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_configuration.EndpointUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_configuration.RequestTimeoutSeconds);
            
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.ApiKey}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            
            foreach (var header in _configuration.CustomHeaders)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        
        public async Task<OptimizationResult> OptimizeCodeAsync(
            string code,
            string? language = null,
            OptimizationLevel level = OptimizationLevel.Balanced,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El código no puede estar vacío", nameof(code));
            }
            
            var cacheKey = GenerateCacheKey(code, language, level);
            
            if (_configuration.EnableCache && TryGetCachedResult(cacheKey, out var cached))
            {
                _logger.LogInformation("Usando resultado cacheado para optimización de código");
                return cached;
            }
            
            await _semaphore.WaitAsync(cancellationToken);
            
            try
            {
                _logger.LogInformation("Solicitando optimización de código a la herramienta de IA");
                
                var prompt = BuildOptimizationPrompt(code, language, level);
                var requestBody = new
                {
                    model = _configuration.DefaultModel,
                    messages = new[]
                    {
                        new { role = "system", content = GetSystemPrompt(level) },
                        new { role = "user", content = prompt }
                    },
                    temperature = _configuration.DefaultTemperature,
                    max_tokens = _configuration.MaxTokens
                };
                
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/v1/chat/completions", httpContent, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("Error de la API de IA: {StatusCode} - {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException(
                        $"Error al comunicarse con la API de IA: {response.StatusCode}");
                }
                
                var responseJson = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: cancellationToken);
                
                if (responseJson?.Choices == null || responseJson.Choices.Length == 0)
                {
                    throw new InvalidOperationException("La respuesta de la API de IA no contiene resultados");
                }
                
                var optimizedCode = responseJson.Choices[0].Message.Content;
                var result = new OptimizationResult
                {
                    OriginalCode = code,
                    OptimizedCode = ExtractCodeFromResponse(optimizedCode),
                    Suggestions = ExtractSuggestions(optimizedCode),
                    ExecutionTime = responseJson.Usage?.TotalTokens ?? 0,
                    ModelUsed = responseJson.Model ?? _configuration.DefaultModel
                };
                
                if (_configuration.EnableCache)
                {
                    AddToCache(cacheKey, result);
                }
                
                _logger.LogInformation("Optimización completada: {TokenCount} tokens utilizados", result.ExecutionTime);
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de red al optimizar código");
                throw;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError(ex, "Timeout al optimizar código");
                throw new TimeoutException("La solicitud de optimización excedió el tiempo límite", ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }
        
        public async Task<CodeAnalysisResult> AnalyzeCodeAsync(
            string code,
            string? language = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El código no puede estar vacío", nameof(code));
            }
            
            var prompt = $"Analiza el siguiente código y proporciona:\n" +
                        "1. Complejidad ciclomática\n" +
                        "2. Posibles problemas de rendimiento\n" +
                        "3. Problemas de seguridad\n" +
                        "4. Código duplicado\n" +
                        "5. Mejores prácticas violadas\n\n" +
                        $"Código a analizar:\n```{language ?? "text"}\n{code}\n```";
            
            var requestBody = new
            {
                model = _configuration.DefaultModel,
                messages = new[]
                {
                    new { role = "system", content = "Eres un experto en análisis de código. Proporciona análisis detallado y accionable." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3,
                max_tokens = _configuration.MaxTokens
            };
            
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/v1/chat/completions", httpContent, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: cancellationToken);
            
            return new CodeAnalysisResult
            {
                Analysis = responseJson?.Choices?[0].Message.Content ?? string.Empty,
                TokenCount = responseJson?.Usage?.TotalTokens ?? 0
            };
        }
        
        public async Task<RefactoringSuggestion> SuggestRefactoringAsync(
            string code,
            string? targetPattern = null,
            CancellationToken cancellationToken = default)
        {
            var patternDescription = targetPattern ?? "mejores prácticas de código limpio";
            var prompt = $"Sugiere refactorizaciones para el siguiente código aplicando {patternDescription}. " +
                        "Proporciona el código refactorizado con explicaciones paso a paso.\n\n" +
                        $"Código original:\n{code}";
            
            var requestBody = new
            {
                model = _configuration.DefaultModel,
                messages = new[]
                {
                    new { role = "system", content = "Eres un experto en refactorización de código. Proporciona código limpio y bien estructurado." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.5,
                max_tokens = _configuration.MaxTokens
            };
            
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/v1/chat/completions", httpContent, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: cancellationToken);
            var content = responseJson?.Choices?[0].Message.Content ?? string.Empty;
            
            return new RefactoringSuggestion
            {
                OriginalCode = code,
                RefactoredCode = ExtractCodeFromResponse(content),
                Explanation = ExtractExplanation(content),
                PatternApplied = targetPattern ?? "Clean Code"
            };
        }
        
        public void ClearCache()
        {
            _cache.Clear();
            _logger.LogInformation("Caché de optimización limpiada");
        }
        
        public int GetCacheSize() => _cache.Count;
        
        private string BuildOptimizationPrompt(string code, string? language, OptimizationLevel level)
        {
            var languageHint = language ?? "csharp";
            var levelInstructions = level switch
            {
                OptimizationLevel.Minimal => "Realiza solo optimizaciones de rendimiento críticas.",
                OptimizationLevel.Balanced => "Equilibra rendimiento y legibilidad. Optimiza donde sea necesario sin comprometer la claridad.",
                OptimizationLevel.Aggressive => "Aplica todas las optimizaciones posibles incluyendo refactorización avanzada y patrones de alto rendimiento.",
                _ => "Equilibra rendimiento y legibilidad."
            };
            
            return $"Optimiza el siguiente código({languageHint}) siguiendo las mejores prácticas. {levelInstructions}\n\n" +
                   $"Código original:\n```{languageHint}\n{code}\n```\n\n" +
                   "Proporciona: 1) Código optimizado, 2) Lista de cambios realizados, 3) Impacto esperado en rendimiento.";
        }
        
        private string GetSystemPrompt(OptimizationLevel level)
        {
            return level switch
            {
                OptimizationLevel.Minimal => 
                    "Eres un asistente de optimización de código conservador. Solo sugiere cambios que mejoran significativamente el rendimiento sin alterar la funcionalidad.",
                OptimizationLevel.Aggressive => 
                    "Eres un asistente de optimización de código agresivo. Sugiere todas las optimizaciones posibles incluyendo refactorización, uso de patrones de rendimiento y reducción de asignaciones.",
                _ => 
                    "Eres un asistente de optimización de código. Sugiere mejoras que equilibran rendimiento y legibilidad del código."
            };
        }
        
        private string GenerateCacheKey(string code, string? language, OptimizationLevel level)
        {
            var input = $"{code}|{language}|{level}";
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hashBytes);
        }
        
        private bool TryGetCachedResult(string cacheKey, out OptimizationResult result)
        {
            if (_cache.TryGetValue(cacheKey, out var cached) && !cached.IsExpired)
            {
                result = cached.Result;
                return true;
            }
            
            result = null!;
            return false;
        }
        
        private void AddToCache(string cacheKey, OptimizationResult result)
        {
            var cached = new CachedOptimization(result, TimeSpan.FromMinutes(_configuration.CacheDurationMinutes));
            _cache[cacheKey] = cached;
            
            CleanupExpiredCache();
        }
        
        private void CleanupExpiredCache()
        {
            var expiredKeys = _cache
                .Where(kvp => kvp.Value.IsExpired)
                .Select(kvp => kvp.Key)
                .ToList();
            
            foreach (var key in expiredKeys)
            {
                _cache.Remove(key);
            }
        }
        
        private string ExtractCodeFromResponse(string response)
        {
            var codeBlockPattern = @"```(?:\w+)?\n([\s\S]*?)\n```";
            var match = System.Text.RegularExpressions.Regex.Match(response, codeBlockPattern);
            
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            
            return response;
        }
        
        private List<string> ExtractSuggestions(string response)
        {
            var suggestions = new List<string>();
            var lines = response.Split('\n');
            var inList = false;
            
            foreach (var line in lines)
            {
                if (line.TrimStart().StartsWith("-") || line.TrimStart().StartsWith("*"))
                {
                    inList = true;
                    suggestions.Add(line.TrimStart().TrimStart('-', '*').Trim());
                }
                else if (inList && string.IsNullOrWhiteSpace(line))
                {
                    inList = false;
                }
            }
            
            return suggestions;
        }
        
        private string ExtractExplanation(string response)
        {
            var codeIndex = response.IndexOf("```");
            if (codeIndex > 0)
            {
                return response.Substring(0, codeIndex).Trim();
            }
            
            return response;
        }
    }
    
    public enum OptimizationLevel
    {
        Minimal,
        Balanced,
        Aggressive
    }
    
    public class OptimizationResult
    {
        public string OriginalCode { get; set; } = string.Empty;
        public string OptimizedCode { get; set; } = string.Empty;
        public List<string> Suggestions { get; set; } = new();
        public int ExecutionTime { get; set; }
        public string ModelUsed { get; set; } = string.Empty;
    }
    
    public class CodeAnalysisResult
    {
        public string Analysis { get; set; } = string.Empty;
        public int TokenCount { get; set; }
    }
    
    public class RefactoringSuggestion
    {
        public string OriginalCode { get; set; } = string.Empty;
        public string RefactoredCode { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string PatternApplied { get; set; } = string.Empty;
    }
    
    internal class CachedOptimization
    {
        public OptimizationResult Result { get; }
        public DateTime CachedAt { get; }
        public TimeSpan Duration { get; }
        
        public CachedOptimization(OptimizationResult result, TimeSpan duration)
        {
            Result = result;
            CachedAt = DateTime.UtcNow;
            Duration = duration;
        }
        
        public bool IsExpired => DateTime.UtcNow > CachedAt.Add(Duration);
    }
    
    internal class ApiResponse
    {
        public string? Model { get; set; }
        public Choice[]? Choices { get; set; }
        public Usage? Usage { get; set; }
    }
    
    internal class Choice
    {
        public Message Message { get; set; } = new();
    }
    
    internal class Message
    {
        public string Content { get; set; } = string.Empty;
    }
    
    internal class Usage
    {
        public int TotalTokens { get; set; }
    }
}