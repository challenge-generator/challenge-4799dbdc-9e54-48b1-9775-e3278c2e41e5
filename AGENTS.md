# AGENTS.md

Instrucciones para el agente de IA que abra este repositorio (Claude Code, Cursor, Codex, Copilot, Gemini). Se cargan solas: no hay que pegar nada en ningun chat.

## Que es este repositorio

Es el codigo base de un reto de aprendizaje de Pragma: **Optimización del flujo de trabajo con IA generativa en Backend.Net**.

| | |
|---|---|
| Tema | Desarrollo |
| Nivel | advanced-l2 |
| Chapter | Backend |
| Especialidad | .NET |
| Stack | .NET 9 / ASP.NET Core 9 |
| Patron arquitectonico | capas estándar con separación de dominio (Domain Layer, Application Layer, Infrastructure Layer) |
| Tiempo estimado | 2 semanas |

## Receta del stack

Esqueleto obligatorio:

- `*.csproj en la raiz del proyecto`
- `Program.cs como entry point`
- `appsettings.json`
- `carpeta Domain con entidades e interfaces`
- `carpeta Application con casos de uso`
- `carpeta Infrastructure con repositorios y Controllers`

Dependencias:

- Microsoft.EntityFrameworkCore 9.0.0
- Microsoft.EntityFrameworkCore.SqlServer 9.0.0
- Microsoft.AspNetCore.Mvc.NewtonsoftJson 9.0.0
- Microsoft.AspNetCore.OpenApi 9.0.0
- Swashbuckle.AspNetCore 6.7.0
- MediatR 12.5.0
- FluentValidation.AspNetCore 11.3.0
- xunit n/a
- Moq n/a
- Microsoft.AspNetCore.Mvc.Testing 9.0.0
- Microsoft.EntityFrameworkCore.InMemory 9.0.0
- GitHub.Copilot n/a
- Amazon.Q n/a

## Tu tarea

Dejar este proyecto en estado **verificable**: que el comando de verificacion corra sin errores. Escribi los archivos en disco, en este repositorio. No generes ZIPs ni archivos adjuntos.

En orden:

1. Corre `dotnet build` y mira que falla.
2. Completa lo que falte de la lista de abajo: manifiesto de dependencias, punto de entrada, capa de interfaz y las capas del patron declarado.
3. Arregla SOLO los errores que impiden compilar o arrancar.
4. Volve a correr `dotnet build` hasta que pase.
5. Pará ahí.

## Regla dura: las fases son trabajo del humano

**PROHIBIDO implementar los entregables de las fases.** El valor del reto esta en que la persona los resuelva. Tu trabajo es que tenga un proyecto que arranca; el hueco pedagogico se queda como esta.

No resuelvas nada de esto:

- **Fase 1 — Integración de la herramienta de IA**: Configuración y documentación de la herramienta de IA generativa en el entorno de desarrollo.
- **Fase 2 — Optimización del código con IA**: Código optimizado con las sugerencias de la herramienta de IA y un informe de evaluación del impacto.
- **Fase 3 — Automatización de tareas repetitivas**: Configuración de la herramienta de IA para automatizar tareas repetitivas y un informe de evaluación del tiempo y calidad mejorados.

Distincion operativa:

- **Arreglar** (si): import faltante, tipo que no existe, dependencia sin declarar, error de sintaxis, archivo referenciado que no existe.
- **No tocar** (no): logica de negocio incompleta, validaciones ausentes, secretos hardcodeados, APIs deprecadas que funcionan, concurrencia insegura, patrones mejorables. Eso es lo que la persona tiene que encontrar.

## Superficie de practica (NO completes)

Estos archivos SON el ejercicio de la persona. No los implementes; deja stubs. No toques la logica que el reto pide completar.

- [ ] `tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.
- [ ] `tests/IntegrationTests/Infrastructure/TransactionRepositoryTests.cs` — El topic pide TDD/pruebas: este archivo es el ejercicio.

## Lo que falta y tenes que completar

### Archivos corruptos (1) — arreglá esto primero

El contenido de estos archivos no corresponde a su extension. Regeneralos completos:

- [ ] `src/WebApi/appsettings.json` — El contenido no corresponde a un archivo json. Hay que regenerarlo completo.

### 1. Archivos que la arquitectura declara (3 de 22)

La propuesta arquitectonica del reto los lista y no llegaron al repo. Crealos con implementacion real, respetando la capa en la que viven:

- [ ] `docs/Optimization_Report.md`
- [ ] `docs/Automation_Report.md`
- [ ] `BackendNetAI.sln`

### 2. Referencias colgando (1)

Salieron de un analisis estatico del codigo que SI esta en el repo. Cada una rompe la compilacion:

- [ ] `tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs` — `ProcessTransactionUseCase.ProcessAsync`
      Se invoca `ProcessAsync` sobre `ProcessTransactionUseCase`, pero esa clase no declara ese metodo. Agregalo con su implementacion real, o usa uno de los que si declara.

### Presentes (22)

- `src/WebApi/Program.cs`
- `src/WebApi/appsettings.json`
- `src/WebApi/WebApi.csproj`
- `src/Domain/Entities/Transaction.cs`
- `src/Domain/Interfaces/ITransactionRepository.cs`
- `src/Domain/Interfaces/INotificationService.cs`
- `src/Application/Application.csproj`
- `src/Application/DTOs/TransactionDto.cs`
- `src/Application/UseCases/ProcessTransactionUseCase.cs`
- `src/Domain/Domain.csproj`
- `src/Infrastructure/Infrastructure.csproj`
- `src/Infrastructure/Repositories/TransactionRepository.cs`
- `src/Infrastructure/Services/NotificationService.cs`
- `src/Infrastructure/Persistence/AppDbContext.cs`
- `src/WebApi/IA/IAConfiguration.cs`
- `src/WebApi/Controllers/TransactionsController.cs`
- `src/WebApi/IA/CodeOptimizer.cs`
- `src/WebApi/IA/TaskAutomation.cs`
- `tests/UnitTests/Application/ProcessTransactionUseCaseTests.cs`
- `tests/IntegrationTests/Infrastructure/TransactionRepositoryTests.cs`
- `docs/IA_Integration_Guide.md`
- `.gitignore`

### Capas del patron declarado

Cada una tiene que existir como directorio real con al menos un archivo. Codigo plano en la raiz no satisface el patron.

- `src/Domain`
- `src/Application`
- `src/Infrastructure`
- `src/WebApi`
- `tests/UnitTests`
- `tests/IntegrationTests`
- `docs`

## Verificacion

```bash
dotnet build
```

El comando tiene que pasar SIN implementar los archivos de la superficie de practica: solo andamiaje.

Ese comando pasando es la definicion de "terminado" para vos.

## Convenciones que tenes que respetar

- Un solo ecosistema: no declares librerias de otro lenguaje ni mezcles gestores de paquetes.
- Toda libreria que uses tiene que estar declarada en el manifiesto de dependencias.
- Todo import declarado tiene que usarse; todo tipo usado tiene que existir o venir de una dependencia declarada.
- El patron es **capas estándar con separación de dominio (Domain Layer, Application Layer, Infrastructure Layer)**: los contratos (interfaces, puertos) los define la capa interna y los implementa la externa, nunca al revés.
- Los archivos que crees llevan implementacion real, no stubs: sin `TODO`, sin cuerpos vacios, sin `// getters y setters`.

## Contexto del candidato

Sirve para calibrar el nivel del codigo, no para resolver las fases.

- Perfil: Chapter Backend, Especialidad Desarrollador, Tecnología .Net, Advanced
- Brecha que el reto ataca: Ha trabajado con algún asistente AI en desarrollo (Amazon Q Dev, Kiro, Github Copilot) y lo utiliza en su día a día como desarrollador. Cierre de brechas de conocimiento en el uso efectivo de herramientas de IA para acelerar el desarrollo, mejorar la calidad del código y aumentar la productividad
- Mision: Candidato con experiencia avanzada en Backend .Net, buscando optimizar su flujo de trabajo con IA generativa

---

*Generado por Challenge Generator — Pragma. `README.md` tiene el enunciado completo del reto para la persona. `PROMPT_MEJORA.md` es la variante para pegar en un chat, si se prefiere ese flujo.*
