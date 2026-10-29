# Prompt para Mejorar el Codigo Base

Copia y pega el contenido del bloque de abajo en un asistente de IA (Claude, ChatGPT)
para obtener un ZIP con el proyecto completo y arrancable.

Si preferis trabajar en tu editor con un agente local (Claude Code, Cursor, Copilot), usa `AGENTS.md` en vez de este archivo: dice lo mismo pero para que escriba los archivos en disco.

## Las dos reglas que no se negocian

1. **Completa el boilerplate.** Todo lo que el proyecto necesita para compilar y arrancar: manifiesto de dependencias, punto de entrada, configuracion, capa de interfaz, y las capas del patron arquitectonico declarado. Eso es andamiaje y es tu trabajo.
2. **NO resuelvas el reto.** Los entregables de las fases son el trabajo de la persona. El hueco pedagogico se deja como esta: el proyecto arranca, pero lo que el reto pide implementar NO esta implementado.

Dicho de otra forma: si algo impide compilar, arreglalo. Si algo es logica de negocio incompleta, validaciones ausentes, un secreto hardcodeado o un patron mejorable, dejalo exactamente como esta — es lo que la persona tiene que encontrar.

## Superficie de practica — NO resuelvas

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs.

- `tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- `tests/IntegrationTests/Infrastructure/TransactionRepositoryTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.

## Lo que le falta a este proyecto

Esto NO lo tenes que adivinar: salio de comparar el proyecto contra la arquitectura declarada del reto y de un analisis estatico del codigo. Completalo TODO.

### Archivos corruptos — arreglar primero

El contenido no corresponde a la extension. Regeneralos completos:

- `src/WebApi/appsettings.json` — El contenido no corresponde a un archivo json. Hay que regenerarlo completo.

### Archivos que la arquitectura del reto declara y no estan

Creálos con implementacion real, en la capa que les corresponde:

- `docs/Optimization_Report.md`
- `docs/Automation_Report.md`
- `BackendNetAI.sln`

### Referencias colgando en el codigo que si esta

Cada una rompe la compilacion:

- `tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs` — `ProcessTransactionUseCase.ProcessAsync`: Se invoca `ProcessAsync` sobre `ProcessTransactionUseCase`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.

## Como saber que terminaste

```bash
dotnet build
```

Ese comando corriendo sin errores es la definicion de "listo".

---

```
## Briefing del reto (autoridad)
Este bloque manda sobre los archivos adjuntos. El stack y el rol salen de AQUÍ, no de un topic genérico ni de markdown placeholder.

### Perfil
Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Advanced

### Brecha de conocimiento
Ha trabajado con algún asistente AI en desarrollo (Amazon Q Dev, Kiro, Github Copilot) y lo utiliza en su día a día como desarrollador. Cierre de brechas de conocimiento en el uso efectivo de herramientas de IA para acelerar el desarrollo, mejorar la calidad del código y aumentar la productividad

### Misión / candidato
Candidato con experiencia avanzada en Backend .Net, buscando optimizar su flujo de trabajo con IA generativa

### Reto
- Tema: Desarrollo
- Seniority: advanced-l2
- Tipo: practical
- Título: Optimización del flujo de trabajo con IA generativa en Backend.Net
- Tiempo estimado: 2 semanas

### Fases (trabajo del HUMANO — PROHIBIDO completarlas)
No implementes estos entregables. Dejalos como hueco pedagógico. El asistente solo materializa el proyecto arrancable para que el participante pueda trabajar.
- Fase 1: Integración de la herramienta de IA — objetivo: Configurar y utilizar una herramienta de IA generativa en el entorno de desarrollo. — entregable (NO resolver): Configuración y documentación de la herramienta de IA generativa en el entorno de desarrollo.
- Fase 2: Optimización del código con IA — objetivo: Utilizar la herramienta de IA para optimizar el código existente. — entregable (NO resolver): Código optimizado con las sugerencias de la herramienta de IA y un informe de evaluación del impacto.
- Fase 3: Automatización de tareas repetitivas — objetivo: Automatizar tareas repetitivas utilizando la herramienta de IA. — entregable (NO resolver): Configuración de la herramienta de IA para automatizar tareas repetitivas y un informe de evaluación del tiempo y calidad mejorados.

Eres un asistente experto en análisis, corrección y generación de archivos de cualquier tipo:
código fuente, documentación, hojas de cálculo, documentos Word, configuraciones, entre otros.
Voy a enviarte una cadena de texto que contiene uno o más archivos. Cada archivo está delimitado por un marcador con el siguiente formato:
// === ARCHIVO: ruta/del/archivo.extension ===
o también puede aparecer como:
## === ARCHIVO: ruta/del/archivo.extension ===
Lo que sigue al marcador puede ser:

El contenido real del archivo (código, texto, YAML, etc.)
Una descripción en lenguaje natural de lo que debe contener el archivo


TU TAREA
PASO 0 — ¿Esto es un proyecto o una carcasa?
Antes de extraer archivos, leé el Briefing (si está) y diagnosticá el adjunto.

Es CARCASA si ocurre CUALQUIERA de estas:
- No hay manifiesto de dependencias del stack del briefing (manifest.json de VTEX IO / package.json / pom.xml / build.gradle / requirements.txt / go.mod / *.tf / *.csproj, según corresponda)
- Hay un "binario" que en realidad es un comentario ("no puede ser mostrado como texto plano", placeholder .fig/.docx vacío)
- Los markdowns ya completan entregables de fases posteriores ("se implementó fade-in", lista de áreas ya resuelta)

Si es CARCASA:
- MATERIALIZÁ un proyecto que arranca en el stack del briefing (VTEX IO Store Framework, Angular, Terraform, pytest, Nest, etc.). Incluí manifiesto, punto de entrada y capa de interfaz reales.
- NO copies los markdowns de "solución" como si fueran el producto. Son ruido de generación.
- NO resuelvas las fases del briefing (están marcadas PROHIBIDO). Dejá el hueco pedagógico: el flujo existe, las microinteracciones/calidad/infra que el reto pide NO están hechas.
- Después seguí al PASO 5 (ZIP).

Si es un proyecto REAL (manifiesto + código que compila o arranca):
- Seguí PASO 1 en adelante. 🔴 compilación sí. 🟡 pedagógico no.

PASO 1 — Detección y extracción
Identifica todos los archivos presentes en la cadena. Para cada archivo extrae:

Su ruta completa (ej: src/main/java/com/pragma/Service.java)
Su contenido o descripción

PASO 2 — Clasificación por tipo
Clasifica cada archivo en una de estas categorías:
A) Código fuente (Java, Python, TypeScript, JavaScript, Kotlin, etc.)
B) Configuración / documentación (YAML, properties, Markdown, JSON, txt, etc.)
C) Excel (.xlsx, .xls, .csv)
D) Word (.docx, .doc)
E) Otro tipo de archivo binario o especial
PASO 3 — Clasificación de errores en código fuente

Objetivo prioritario: que el proyecto compile. No corrijas flujo de negocio ni lógica funcional.

Antes de modificar cualquier archivo de código fuente, clasifica cada problema encontrado en una de estas dos categorías:
🔴 ERROR DE COMPILACIÓN — corregir siempre
Son errores que impiden que el proyecto arranque, sin valor pedagógico:

Import faltante o incorrecto
Clase, método o variable referenciada que no existe en ningún archivo del proyecto
Error de sintaxis
Anotación con atributos inválidos
Dependencia ausente en pom.xml, package.json, etc.
Archivo referenciado que no existe y debe ser creado con implementación mínima

→ CORREGIR estos errores.
🟡 PROBLEMA FUNCIONAL O DE CALIDAD — preservar siempre
Son problemas que no impiden compilar. Pueden ser intencionales para el aprendizaje:

Clave secreta hardcodeada ("secret", "password123")
API deprecada que funciona pero tiene reemplazo moderno
Lógica de negocio incorrecta o incompleta
Código redundante o de baja legibilidad
Falta de validaciones en flujo de negocio
Patrones de diseño incorrectos pero funcionales
Concurrencia no segura
Configuración funcional pero no óptima

→ PRESERVAR tal cual. No corregir, no mejorar, no comentar.
PASO 4 — Procesamiento según tipo de archivo
Tipo A — Código fuente
Aplica únicamente las correcciones clasificadas como 🔴 ERROR DE COMPILACIÓN.
No alteres ningún elemento clasificado como 🟡 PROBLEMA FUNCIONAL O DE CALIDAD.
Si falta un archivo referenciado, créalo con la implementación mínima necesaria para compilar.
Tipo B — Configuración / documentación
Extrae el contenido tal cual, sin modificaciones salvo errores evidentes de sintaxis
(ej: YAML mal indentado).
Tipo C — Excel (.xlsx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un archivo Excel funcional con:

Fila de encabezados en negrita con color de fondo distintivo
Columnas con ancho ajustado al contenido
Tipos de dato correctos por columna
Validaciones si la descripción lo indica
Hojas nombradas descriptivamente si hay más de una
Filas de ejemplo si no hay datos reales

Tipo D — Word (.docx)
Si viene con contenido real, genera el archivo respetando ese contenido.
Si viene con descripción en lenguaje natural, genera un documento Word funcional con:

Estilos de título (Título 1, Título 2) para jerarquía de secciones
Fuente legible (Calibri o equivalente), tamaño 11-12pt para cuerpo
Márgenes estándar
Tabla de contenido si tiene múltiples secciones
Tablas con encabezados en negrita si aplica

Tipo E — Otro
Genera el archivo con el contenido o estructura más apropiada según la descripción.
PASO 5 — Exportación en ZIP
Empaqueta todos los archivos en un único archivo ZIP descargable respetando exactamente
la estructura de rutas indicada por los marcadores.
El ZIP debe incluir:

Archivos de código con únicamente los errores de compilación corregidos
Archivos de configuración y documentación sin cambios
Archivos nuevos creados para resolver dependencias de compilación faltantes
Archivos Excel y Word generados desde descripción

IMPORTANTE: El ZIP debe estar listo para descargar al finalizar. No preguntes si el usuario
quiere generarlo. Simplemente genera el archivo y proporciona el enlace de descarga; No debes desplegar en el chat el resumen de lo que arreglaste al Zip, solo entregalo.

REGLAS IMPORTANTES

No omitas ningún archivo aunque no tenga errores ni modificaciones
Respeta los nombres y rutas exactas indicadas por los marcadores
Si un archivo no tiene marcador claro, infiere el nombre desde su contenido
Si la cadena contiene solo documentación, placeholders o binarios fake, NO la reproduzcas:
aplicá PASO 0 (materializar el proyecto del briefing). Reproducir la carcasa es un fallo.
No agregues texto después del enlace de descarga del ZIP
No preguntes si el usuario quiere el ZIP: simplemente generalo siempre
Si detectas que falta un archivo de configuración necesario para compilar
(pom.xml, package.json, requirements.txt, build.gradle, etc.), créalo e inclúyelo
inferiendo su contenido desde los imports y frameworks detectados en el código
Nunca corrijas problemas 🟡 aunque parezcan obvios o fáciles de mejorar.
El participante que recibirá este proyecto los debe encontrar y resolver él mismo.


INPUT
Aquí está la cadena con los archivos:

// === ARCHIVO: src/WebApi/Program.cs ===
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using MediatR;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend.Net AI Integration API",
        Version = "v1",
        Description = "API para gestión de transacciones con integración de IA generativa",
        Contact = new OpenApiContact
        {
            Name = "Equipo Fintech",
            Email = "support@fintech.com"
        }
    });

    // Incluir comentarios XML si existen
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuración de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.Load("Application"),
    Assembly.GetExecutingAssembly()
));

// Configuración de FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

// Configuración de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
    )
);

// Configuración de servicios de IA (se implementará en otros lotes)
builder.Services.AddScoped<IAConfiguration>();
builder.Services.AddScoped<CodeOptimizer>();
builder.Services.AddScoped<TaskAutomation>();

// Configuración de repositorios
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Middleware de manejo global de excepciones
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend.Net AI Integration API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

// Middleware personalizado
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

// === ARCHIVO: src/WebApi/appsettings.json ===
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=BackendNetAI;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "IAConfiguration": {
    "CodeOptimizationEnabled": true,
    "TaskAutomationEnabled": true,
    "ModelProvider": "GitHubCopilot",
    "MaxSuggestions": 3,
    "TimeoutMilliseconds": 5000
  },
  "NotificationService": {
    "ApiKey": "your-notification-service-api-key",
    "BaseUrl": "https://api.notificationservice.com/v1"
  },
  "Swagger": {
    "Enabled": true
  }
}

// === ARCHIVO: src/WebApi/WebApi.csproj ===
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="9.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.7.0" />
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Application\Application.csproj" />
    <ProjectReference Include="..\Domain\Domain.csproj" />
    <ProjectReference Include="..\Infrastructure\Infrastructure.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Update="appsettings.Development.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>

// === ARCHIVO: src/Domain/Entities/Transaction.cs ===
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

// === ARCHIVO: src/Domain/Interfaces/ITransactionRepository.cs ===
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

// === ARCHIVO: src/Domain/Interfaces/INotificationService.cs ===
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

// === ARCHIVO: src/Application/Application.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MediatR" Version="12.5.0" />
    <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Domain\Domain.csproj" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/Application/DTOs/TransactionDto.cs ===
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

// === ARCHIVO: src/Application/UseCases/ProcessTransactionUseCase.cs ===
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

// === ARCHIVO: src/Domain/Domain.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>Domain</RootNamespace>
    <AssemblyName>Domain</AssemblyName>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <PropertyGroup>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);CS1591</NoWarn>
  </PropertyGroup>

  <PropertyGroup>
    <Authors>Backend Team</Authors>
    <Company>Fintech Company</Company>
    <Product>BackendNetAI</Product>
    <Description>Capa de dominio del proyecto de integración de IA generativa en Backend .NET</Description>
    <Copyright>Copyright © 2024</Copyright>
  </PropertyGroup>

  <PropertyGroup>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <WarningsAsErrors></WarningsAsErrors>
  </PropertyGroup>

  <PropertyGroup>
    <DebugType>embedded</DebugType>
    <DebugSymbols>true</DebugSymbols>
  </PropertyGroup>

  <ItemGroup>
    <Compile Remove="bin\**" />
    <Compile Remove="obj\**" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Remove="bin\**" />
    <EmbeddedResource Remove="obj\**" />
  </ItemGroup>

  <ItemGroup>
    <None Remove="bin\**" />
    <None Remove="obj\**" />
  </ItemGroup>

  <ItemGroup>
    <Compile Include="Entities\Transaction.cs" />
    <Compile Include="Interfaces\ITransactionRepository.cs" />
    <Compile Include="Interfaces\INotificationService.cs" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" Version="9.0.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

</Project>

// === ARCHIVO: src/Infrastructure/Infrastructure.csproj ===
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Domain\Domain.csproj" />
    <ProjectReference Include="..\Application\Application.csproj" />
  </ItemGroup>

</Project>

// === ARCHIVO: src/Infrastructure/Repositories/TransactionRepository.cs ===
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(AppDbContext context, ILogger<TransactionRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener una transacción con ID vacío");
            return null;
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (transaction == null)
        {
            _logger.LogInformation("Transacción con ID {TransactionId} no encontrada", id);
        }

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        transaction.Id = Guid.NewGuid();
        transaction.CreatedAt = DateTime.UtcNow;
        transaction.UpdatedAt = DateTime.UtcNow;

        if (transaction.Status == TransactionStatus.Pending)
        {
            transaction.ProcessedAt = null;
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} creada exitosamente", transaction.Id);
        return transaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        var existingTransaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == transaction.Id, cancellationToken);

        if (existingTransaction == null)
        {
            _logger.LogError("No se puede actualizar. Transacción {TransactionId} no encontrada", transaction.Id);
            throw new InvalidOperationException($"Transacción con ID {transaction.Id} no encontrada");
        }

        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Description = transaction.Description;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Status = transaction.Status;
        existingTransaction.UpdatedAt = DateTime.UtcNow;

        if (transaction.Status == TransactionStatus.Completed && existingTransaction.ProcessedAt == null)
        {
            existingTransaction.ProcessedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} actualizada exitosamente", transaction.Id);
        return existingTransaction;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó eliminar una transacción con ID vacío");
            return false;
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (transaction == null)
        {
            _logger.LogWarning("Transacción {TransactionId} no encontrada para eliminación", id);
            return false;
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Transacción {TransactionId} eliminada exitosamente", id);
        return true;
    }

    public async Task<IEnumerable<Transaction>> GetByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        if (startDate > endDate)
        {
            _logger.LogWarning("Fecha de inicio {StartDate} es mayor que fecha de fin {EndDate}", startDate, endDate);
            throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin");
        }

        return await _context.Transactions
            .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .CountAsync(t => t.Status == status, cancellationToken);
    }

    public async Task<decimal> GetTotalAmountByStatusAsync(TransactionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .Where(t => t.Status == status)
            .SumAsync(t => t.Amount, cancellationToken);
    }
}

// === ARCHIVO: src/Infrastructure/Services/NotificationService.cs ===
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

// === ARCHIVO: src/Infrastructure/Persistence/AppDbContext.cs ===
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

// === ARCHIVO: src/WebApi/IA/IAConfiguration.cs ===
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

// === ARCHIVO: src/WebApi/Controllers/TransactionsController.cs ===
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities;
using Application.DTOs;
using Application.UseCases;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;
        
        public TransactionsController(
            IMediator mediator,
            ILogger<TransactionsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] TransactionType? type = null,
            [FromQuery] TransactionStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            if (page < 1)
            {
                return BadRequest("El número de página debe ser mayor o igual a 1");
            }
            
            if (pageSize < 1 || pageSize > 1000)
            {
                return BadRequest("El tamaño de página debe estar entre 1 y 1000");
            }
            
            _logger.LogInformation(
                "Consultando transacciones - Página: {Page}, Tamaño: {PageSize}, Tipo: {Type}, Estado: {Status}",
                page, pageSize, type, status);
            
            try
            {
                var query = new GetAllTransactionsQuery(page, pageSize, type, status);
                var result = await _mediator.Send(query, cancellationToken);
                
                _logger.LogInformation("Se recuperaron {Count} transacciones", result.Count());
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar transacciones");
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Consultando transacción con ID: {TransactionId}", id);
            
            try
            {
                var query = new GetTransactionByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(
            [FromBody] CreateTransactionDto createDto,
            CancellationToken cancellationToken = default)
        {
            if (createDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío");
            }
            
            if (string.IsNullOrWhiteSpace(createDto.Description))
            {
                return BadRequest("La descripción es obligatoria");
            }
            
            if (createDto.Amount <= 0)
            {
                return BadRequest("El monto debe ser mayor que cero");
            }
            
            _logger.LogInformation(
                "Creando transacción: {Description}, Monto: {Amount}, Tipo: {Type}",
                createDto.Description, createDto.Amount, createDto.Type);
            
            try
            {
                var command = new CreateTransactionCommand(
                    createDto.Description,
                    createDto.Amount,
                    createDto.Type,
                    createDto.Category);
                
                var result = await _mediator.Send(command, cancellationToken);
                
                _logger.LogInformation(
                    "Transacción creada exitosamente con ID: {TransactionId}",
                    result.Id);
                
                return CreatedAtAction(
                    nameof(GetTransactionById),
                    new { id = result.Id },
                    result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear transacción");
                return UnprocessableEntity(ex.Errors.Select(e => new 
                {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear transacción");
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(
            [FromRoute] Guid id,
            [FromBody] UpdateTransactionDto updateDto,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            if (updateDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío");
            }
            
            _logger.LogInformation("Actualizando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new UpdateTransactionCommand(
                    id,
                    updateDto.Description,
                    updateDto.Amount,
                    updateDto.Status);
                
                var result = await _mediator.Send(command, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada para actualizar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción actualizada exitosamente: {TransactionId}", id);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> DeleteTransaction(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Eliminando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new DeleteTransactionCommand(id);
                var result = await _mediator.Send(command, cancellationToken);
                
                if (!result)
                {
                    _logger.LogWarning("Transacción no encontrada para eliminar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción eliminada exitosamente: {TransactionId}", id);
                
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflicto al eliminar transacción {TransactionId}", id);
                return Conflict($"No se puede eliminar la transacción {id}: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPost("{id:guid}/process")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<TransactionDto>> ProcessTransaction(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Procesando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new ProcessTransactionCommand(id);
                var result = await _mediator.Send(command, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada para procesar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción procesada exitosamente: {TransactionId}", id);
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflicto al procesar transacción {TransactionId}", id);
                return Conflict($"Error al procesar la transacción {id}: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
    }
    
    public record GetAllTransactionsQuery(
        int Page,
        int PageSize,
        TransactionType? Type,
        TransactionStatus? Status) : IRequest<IEnumerable<TransactionDto>>;
    
    public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto?>;
    
    public record CreateTransactionCommand(
        string Description,
        decimal Amount,
        TransactionType Type,
        string? Category) : IRequest<TransactionDto>;
    
    public record UpdateTransactionCommand(
        Guid Id,
        string? Description,
        decimal? Amount,
        TransactionStatus? Status) : IRequest<TransactionDto?>;
    
    public record DeleteTransactionCommand(Guid Id) : IRequest<bool>;
    
    public record ProcessTransactionCommand(Guid Id) : IRequest<TransactionDto?>;
}

// === ARCHIVO: src/WebApi/IA/CodeOptimizer.cs ===
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

// === ARCHIVO: src/WebApi/IA/TaskAutomation.cs ===
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

// === ARCHIVO: tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs ===
using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Application;

public class ProcessTransactionUseCaseTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly ProcessTransactionUseCase _sut;

    public ProcessTransactionUseCaseTests()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _notificationServiceMock = new Mock<INotificationService>();
        _sut = new ProcessTransactionUseCase(
            _transactionRepositoryMock.Object,
            _notificationServiceMock.Object);
    }

    [Fact]
    public async Task ProcessAsync_ValidTransaction_ReturnsSuccess()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Pending,
            Amount = 1000m,
            Description = "Test transaction"
        };

        _transactionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        // Act
        var result = await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task ProcessAsync_DuplicateTransaction_ReturnsFailure()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Pending,
            Amount = 500m,
            Description = "Duplicate test"
        };

        _transactionRepositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task ProcessAsync_ValidatesAmount_ThrowsWhenNegative()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Pending,
            Amount = -100m,
            Description = "Invalid amount"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await _sut.ProcessAsync(transaction, CancellationToken.None));
    }

    [Fact]
    public async Task ProcessAsync_SendsNotification_OnSuccess()
    {
        // Arrange
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Pending,
            Amount = 2000m,
            Description = "Notification test"
        };

        _transactionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        // Act
        await _sut.ProcessAsync(transaction, CancellationToken.None);

        // Assert
        _notificationServiceMock.Verify(
            n => n.SendAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

// === ARCHIVO: tests/IntegrationTests/Infrastructure/TransactionRepositoryTests.cs ===
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Infrastructure;

public class TransactionRepositoryTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ValidTransaction_PersistsToDatabase()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Pending,
            Amount = 1500m,
            Description = "Test persistence",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await repository.AddAsync(transaction, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        var saved = await context.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
        Assert.NotNull(saved);
        Assert.Equal(TransactionType.Transfer, saved.Type);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingTransaction_ReturnsTransaction()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Completed,
            Amount = 500m,
            Description = "Test retrieval",
            CreatedAt = DateTime.UtcNow
        };
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(transaction.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
    }

    [Fact]
    public async Task GetByTypeAndStatusAsync_ReturnsMatchingTransactions()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        var t1 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Transfer, Status = TransactionStatus.Completed, Amount = 100m, CreatedAt = DateTime.UtcNow };
        var t2 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Transfer, Status = TransactionStatus.Completed, Amount = 200m, CreatedAt = DateTime.UtcNow };
        var t3 = new Transaction { Id = Guid.NewGuid(), Type = TransactionType.Payment, Status = TransactionStatus.Completed, Amount = 300m, CreatedAt = DateTime.UtcNow };

        await context.Transactions.AddRangeAsync(t1, t2, t3);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByTypeAndStatusAsync(
            TransactionType.Transfer,
            TransactionStatus.Completed,
            CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ExistsAsync_ExistingId_ReturnsTrue()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Pending,
            Amount = 1000m,
            CreatedAt = DateTime.UtcNow
        };
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        // Act
        var exists = await repository.ExistsAsync(transaction.Id, CancellationToken.None);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_NonExistingId_ReturnsFalse()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        // Act
        var exists = await repository.ExistsAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.False(exists);
    }
}

// === ARCHIVO: docs/IA_Integration_Guide.md ===
# Guía de Integración de IA Generativa en el Proyecto

## Introducción

Este documento describe cómo integrar herramientas de IA generativa en el flujo de desarrollo del proyecto Backend.Net. La integración permite a los desarrolladores experimentar con asistentes de IA para acelerar tareas de codificación, optimización y automatización.

## Herramientas Soportadas

El proyecto está configurado para funcionar con las siguientes herramientas de IA generativa:

- **GitHub Copilot**: Asistente de código basado en modelos de OpenAI, integrado directamente en Visual Studio y VS Code.
- **Amazon Q**: Asistente de IA de AWS para desarrollo cloud-native, con conocimiento del ecosistema AWS.

Ambas herramientas se declaran en el archivo `.csproj` del proyecto WebApi como dependencias de desarrollo (`devDependencies`).

## Configuración de GitHub Copilot

### Requisitos Previos

- Suscripción a GitHub Copilot (individual o empresarial).
- Visual Studio 2022+ o VS Code con extensión de Copilot instalada.
- Cuenta de GitHub autenticada en el IDE.

### Activación en el IDE

1. Abrir Visual Studio o VS Code.
2. Navegar a Extensiones → Administrar extensiones.
3. Buscar "GitHub Copilot" e instalar.
4. Reiniciar el IDE.
5. Iniciar sesión con la cuenta de GitHub que tenga Copilot habilitado.

### Configuración de Sugerencias

En Visual Studio:
- Ir a Herramientas → Opciones → GitHub Copilot.
- Ajustar la longitud máxima de sugerencias.
- Habilitar o deshabilitar la completación automática de funciones completas.

En VS Code:
- Archivo → Preferencias → Configuración → Extensions → Copilot.
- Configurar `"github.copilot.inlineSuggest.enable": true` para sugerencias inline.

## Configuración de Amazon Q

### Requisitos Previos

- IDE compatible: VS Code, JetBrains IDEs, Visual Studio.
- Extensión de Amazon Q instalada.
- Credenciales de AWS con permisos para Amazon Q (IAM).

### Activación en el IDE

1. Instalar la extensión "Amazon Q" desde el marketplace del IDE.
2. Abrir el panel de Amazon Q desde la barra lateral.
3. Autenticarse con las credenciales de AWS IAM.
4. Seleccionar el perfil de AWS apropiado para el proyecto.

### Integración con Servicios AWS

Amazon Q puede analizar el código y sugerir integraciones con servicios AWS. Para el proyecto actual, las integraciones relevantes incluyen:

- **AWS SQS/SNS**: Para implementar el patrón de mensajería asíncrona.
- **AWS Lambda**: Para funciones serverless que complementen el backend.
- **Amazon DynamoDB**: Para escenarios que requieran base de datos NoSQL.

## Uso en el Flujo de Desarrollo

### Generación de Código Inicial

Copilot puede generar código boilerplate basado en comentarios o nombres de funciones. Ejemplo:

```csharp
// Crear un método que procese una transacción asynchronously
public async Task<TransactionResult> ProcessTransactionAsync(TransactionRequest request)
{
    // Copilot sugerirá la implementación completa
}
```

### Optimización de Código Existente

Seleccionar el código a optimizar y usar el comando de contexto:
- VS Code: `Ctrl + Shift + P` → "Copilot: Optimize Code"
- Visual Studio: Clic derecho → "GitHub Copilot" → "Optimize Code"

Amazon Q ofrece análisis más profundo:
- `Amazon Q: Explain` - Explica el código seleccionado.
- `Amazon Q: Refactor` - Propone refactorizaciones.
- `Amazon Q: Create Unit Test` - Genera pruebas unitarias.

### Automatización de Tareas Repetitivas

Ambas herramientas soportan la creación de snippets y macros:

1. Seleccionar el código repetitivo.
2. Crear un snippet con: `Copilot: Create Snippet`.
3. Asignar un atajo de teclado para invocarlo rápidamente.

## Configuración del Proyecto

El proyecto incluye archivos de configuración específicos para IA:

- `src/WebApi/IA/IAConfiguration.cs`: Configuración de las opciones de IA.
- `src/WebApi/IA/CodeOptimizer.cs`: Clase que coordina las optimizaciones sugeridas.
- `src/WebApi/IA/TaskAutomation.cs`: Configuración de automatizaciones.

Estos archivos permiten personalizar el comportamiento de las herramientas según las necesidades del equipo.

## Mejores Prácticas

### Revisión Obligatoria

NUNCA aceptar sugerencias de IA sin revisar. Las sugerencias pueden contenir:
- Errores lógicos no detectados.
- Vulnerabilidades de seguridad.
- Ineficiencias de rendimiento.
- Código que no sigue las convenciones del proyecto.

### Contexto de Proyecto

Proporcionar contexto suficiente a la IA:
- Nombres descriptivos de variables y funciones.
- Comentarios que expliquen el propósito del código.
- Documentación en archivos README.md.

### Pruebas Obligatorias

Todo código generado o sugerido por IA debe pasar por:
1. Revisión de código por un par.
2. Ejecución de pruebas unitarias existentes.
3. Verificación de cobertura de código.
4. Pruebas de integración en entorno de staging.

## Solución de Problemas Comunes

### Copilot no ofrece sugerencias

- Verificar que la extensión esté instalada y habilitada.
- Confirmar que el archivo está guardado con extensión válida (.cs, .cshtml, etc.).
- Revisar la configuración de privacidad en la cuenta de GitHub.

### Amazon Q no se conecta

- Verificar credenciales de AWS en la extensión.
- Confirmar que el rol IAM tiene permisos para Amazon Q.
- Revisar la configuración de proxy si se trabaja detrás de un firewall.

### Sugerencias de baja calidad

- Proporcionar más contexto en comentarios.
- Dividir funciones grandes en funciones más pequeñas.
- Usar nombres más descriptivos para funciones y variables.

## Referencias

- Documentación oficial de GitHub Copilot: https://docs.github.com/en/copilot
- Documentación de Amazon Q: https://docs.aws.amazon.com/amazonq/
- Guía de estilo de código del proyecto: consultar README.md en raíz.

// === ARCHIVO: .gitignore ===
## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.
##
## Get latest from https://github.com/github/gitignore/blob/main/VisualStudio.gitignore

# User-specific files
*.rsuser
*.suo
*.user
*.userosscache
*.sln.docstates

# User-specific files (MonoDevelop/Xamarin Studio)
*.userprefs

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Ww][Ii][Nn]32/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/

# Visual Studio 2015/2017 cache/options directory
.vs/
# Uncomment if you have tasks that create the project's static files in wwwroot
#wwwroot/

# Visual Studio 2017 auto generated files
Generated\ Files\/

# MSTest test Results
[Tt]est[Rr]esult*/
[Bb]uild[Ll]og.*

# NUnit
*.VisualState.xml
TestResult.xml
nunit-*.xml

# Build Results of an ATL Project
[Dd]ebugPS/
[Rr]eleasePS/
dlldata.c

# Benchmark Results
BenchmarkDotNet.Artifacts/

# .NET Core
project.lock.json
project.fragment.lock.json
artifacts/

# ASP.NET Scaffolding
ScaffoldingReadMe.txt

# StyleCop
StyleCopReport.xml

# Files built by Visual Studio
*_i.c
*_p.c
*_i.h
*.ilk
*.meta
*.obj
*.iobj
*.pch
*.pdb
*.ipdb
*.pgc
*.pgd
*.rsp
*.resources
*.tlh
*.tlh
*.tmp
*.tmp_proj
*_wpftmp.cs
*.vspscc
*.vssscc
.builds
*.pidb
*.svclog
*.scc

# Chutzpah Test files
_Chutzpah*

# Visual C++ cache files
ipch/
*.aps
*.ncb
*.opendb
*.opensdf
*.sdf
*.cachefile
*.VC.db
*.VC.VC.opendb

# Visual Studio profiler
*.psess
*.vsp
*.vspx
*.sap

# Visual Studio Trace Files
*.e2e

# TFS 2012 Local Workspace
$tf/

# Guidance Automation Toolkit
*.gpState

# ReSharper is a .NET coding add-in
_ReSharper*/
*.[Rr]e[Ss]harper
*.DotSettings.user

# JustCode is a .NET coding add-in
.JustCode

# TeamCity is a build add-in
_TeamCity*

# DotCover is a Code Coverage Tool
*.dotCover

# AxoCover is a Code Coverage Tool
.axoCover/*
!.axoCover/settings.json

# Coverlet is a free, cross platform Code Coverage Tool
coverage*.json
coverage*.xml
coverage*.info

# Visual Studio code coverage results
*.coverage
*.coveragexml

# NCrunch
_NCrunch_*
.*crunch*.local.xml
nCrunchTemp_*

# MightyMoose
*.mm.
AutoTest.Net/

# Web workbench (sass)
.sass-cache/

# Installshield output folder
[Ee]xpress/

# DocProject is a documentation generator add-in
DocProject/buildhelp/
DocProject/Help/*.HxT
DocProject/Help/*.HxC
DocProject/Help/*.hhc
DocProject/Help/*.hhk
DocProject/Help/*.hhp
DocProject/Help/Html2
DocProject/Help/*.html
DocProject/Help/*.htm
DocProject/Help/*.hxs
DocProject/Help/*.hxt
DocProject/Help/*.hxh
DocProject/Help/*.hxq
DocProject/Help/*.hxt
DocProject/Help/*.hxs
DocProject/Help/*.hxt
DocProject/Help/*.hxt

# Click-Once directory
publish/

# Publish Web Output
*.[Pp]ublish.xml
*.azurePubxml
# Note: Comment the next line if you want to checkin your web deploy settings,
# but database connection strings (with potential passwords) will be unencrypted
*.pubxml
*.publishproj

# Microsoft Azure Web App publish settings. Comment the next line if you want to
# checkin your Azure settings file but know that your project will be deployed.
# Publishing includes the following files:
# web.config
# app.config
# we want to ignore those so we don't check in passwords
# But do want to see what other settings are there
# app.config

# NuGet Packages
*.nupkg
# The packages folder can be ignored because of Package Restore
**/[Pp]ackages/*
# except build/, which is used as an MSBuild target.
!**/[Pp]ackages/build/
# Uncomment if necessary however generally it will be regenerated when needed
#!**/[Pp]ackages/repositories.config
# NPuzzle: ignore packages created by dotnet pack
#dotnet_core_library

# NuGet v3's project.json files produces more ignorable files
*.nuget.props
*.nuget.targets

# Microsoft Azure Build Output
csx/
*.build.csdef

# Microsoft Azure Emulator
emulator/

# Microsoft Azure Execution
__pycache__/

# Microsoft Azure Mobile Service's built folder
mobilegalleryoutput/

# Microsoft Azure Mobile Service
.mobilegallery

# Visual Studio Solution Files
*.sln

# Visual Studio code workspace files
*.lsw

# Local Debug法庭 Data
.vs/

# Secrets - DO NOT CHECK IN
appsettings.*.json
!appsettings.json
!appsettings.Development.json

# Node.js
node_modules/
npm-debug.log
yarn-error.log

# npm package manager
generate-lock-profile.ps1
package-lock.json
yarn.lock

# Rider
.idea/

# VS Code
.vscode/

# Docker
.dockerignore

# Build cache
*.log

# Test results
TestResults/
coverage/

# Reject attempts to commit temporary or generated files
*.tmp
*.temp
*.bak
*.swp
*~
.DS_Store
Thumbs.db

# Azure Functions
local.settings.json

# Secrets management
*.pfx
*.key
*.pem
*.crt
secrets/
vault/
```
