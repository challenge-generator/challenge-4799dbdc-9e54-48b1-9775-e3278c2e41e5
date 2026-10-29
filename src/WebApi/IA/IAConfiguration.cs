using Microsoft.Extensions.Configuration;

namespace WebApi.IA
{
    public class IAConfiguration
    {
        private readonly IConfiguration _configuration;
        
        public string EndpointUrl { get; }
        public string ApiKey { get; }
        public string DefaultModel { get; }
        public double DefaultTemperature { get; }
        public int MaxTokens { get; }
        public int RequestTimeoutSeconds { get; }
        public int MaxRetries { get; }
        public bool EnableCache { get; }
        public int CacheDurationMinutes { get; }
        public Dictionary<string, string> CustomHeaders { get; }
        
        public IAConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
            var iaSection = _configuration.GetSection("IA");
            
            if (!iaSection.Exists())
            {
                throw new InvalidOperationException(
                    "La sección 'IA' no está configurada en appsettings.json. " +
                    "Configure los parámetros de la herramienta de IA generativa.");
            }
            
            EndpointUrl = iaSection["EndpointUrl"] 
                ?? throw new InvalidOperationException("IA:EndpointUrl es obligatorio");
            
            ApiKey = iaSection["ApiKey"] 
                ?? throw new InvalidOperationException("IA:ApiKey es obligatorio");
            
            DefaultModel = iaSection["DefaultModel"] ?? "gpt-4";
            
            if (!double.TryParse(iaSection["DefaultTemperature"], out var temperature))
            {
                temperature = 0.7;
            }
            DefaultTemperature = temperature;
            
            if (!int.TryParse(iaSection["MaxTokens"], out var maxTokens))
            {
                maxTokens = 2000;
            }
            MaxTokens = maxTokens;
            
            if (!int.TryParse(iaSection["RequestTimeoutSeconds"], out var timeout))
            {
                timeout = 30;
            }
            RequestTimeoutSeconds = timeout;
            
            if (!int.TryParse(iaSection["MaxRetries"], out var retries))
            {
                retries = 3;
            }
            MaxRetries = retries;
            
            EnableCache = bool.TryParse(iaSection["EnableCache"], out var enableCache) && enableCache;
            
            if (!int.TryParse(iaSection["CacheDurationMinutes"], out var cacheDuration))
            {
                cacheDuration = 60;
            }
            CacheDurationMinutes = cacheDuration;
            
            CustomHeaders = new Dictionary<string, string>();
            var headersSection = iaSection.GetSection("CustomHeaders");
            if (headersSection.Exists())
            {
                foreach (var header in headersSection.GetChildren())
                {
                    CustomHeaders[header.Key] = header.Value ?? string.Empty;
                }
            }
        }
        
        public bool ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(EndpointUrl))
                return false;
            
            if (string.IsNullOrWhiteSpace(ApiKey))
                return false;
            
            if (DefaultTemperature < 0 || DefaultTemperature > 2)
                return false;
            
            if (MaxTokens <= 0 || MaxTokens > 100000)
                return false;
            
            if (RequestTimeoutSeconds <= 0)
                return false;
            
            return true;
        }
    }
}