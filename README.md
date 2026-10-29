# Optimización del flujo de trabajo con IA generativa en Backend.Net

El equipo de desarrollo de una empresa fintech está buscando mejorar su productividad y calidad del código mediante la integración de herramientas de IA generativa en su flujo de trabajo diario. El objetivo es que los desarrolladores experimentados en Backend.Net aprendan a utilizar estas herramientas de manera efectiva para acelerar el desarrollo y optimizar el código. Los desarrolladores interactúan con el sistema de gestión de proyectos, la base de datos de clientes, y el servicio de notificaciones. El sistema debe manejar volúmenes de hasta 10,000 transacciones por segundo con una latencia máxima de 500 milisegundos.

## Informacion General

| Campo | Valor |
|-------|-------|
| **Tema** | Desarrollo |
| **Nivel** | advanced-l2 |
| **Tipo** | practical |
| **Tiempo estimado** | 2 semanas |

## Fases del Reto

### Fase 0: Configuración del Proyecto

**Objetivo:** Obtener el proyecto base funcional enviando el Código Base a un asistente de IA, que lo analizará, corregirá errores y generará un ZIP listo para usar.

**Tiempo estimado:** 15-30 minutos

**Instrucciones:**

- Asegúrate de tener instalado para ejecutar el proyecto: .NET SDK 8+, VS Code o Visual Studio.
- Copia todo el contenido del campo **Código Base** de este reto — incluyendo el texto de instrucciones que aparece al inicio.
- Abre un asistente de IA (Claude en claude.ai, ChatGPT o Gemini — se recomienda Claude), pega el contenido copiado en el chat y envíalo.
- El asistente analizará los archivos, corregirá errores y generará un archivo ZIP descargable. Descárgalo y extráelo en la carpeta donde quieras trabajar.
- Ejecuta `dotnet build`. Si no hay errores, estás listo.

**Entregable:** El proyecto compila/arranca sin errores.

<details>
<summary>Pistas de conocimiento</summary>

- Copia el Código Base completo incluyendo el texto de instrucciones al inicio — esas instrucciones le indican al asistente exactamente qué hacer con los archivos.
- Si el asistente no genera el ZIP automáticamente al terminar el análisis, escríbele: "genera el ZIP ahora".
- Si el proyecto tiene errores al arrancar, comparte el mensaje de error con el mismo asistente para que lo corrija.

</details>

### Fase 1: Integración de la herramienta de IA

**Objetivo:** Configurar y utilizar una herramienta de IA generativa en el entorno de desarrollo.

**Tiempo estimado:** 3 días

**Instrucciones:**

- Identificar una herramienta de IA generativa adecuada para el flujo de trabajo de desarrollo en Backend.Net.
- Configurar la herramienta en el entorno de desarrollo.
- Utilizar la herramienta para generar código y documentación.

**Entregable:** Configuración y documentación de la herramienta de IA generativa en el entorno de desarrollo.

<details>
<summary>Pistas de conocimiento</summary>

- Beneficios de utilizar herramientas de IA en el desarrollo de software.
- Características y limitaciones de las herramientas de IA generativa.

</details>

### Fase 2: Optimización del código con IA

**Objetivo:** Utilizar la herramienta de IA para optimizar el código existente.

**Tiempo estimado:** 4 días

**Instrucciones:**

- Seleccionar un fragmento de código existente que pueda ser optimizado.
- Utilizar la herramienta de IA para sugerir mejoras y refactorizaciones.
- Implementar las sugerencias y evaluar el impacto en la calidad y rendimiento del código.

**Entregable:** Código optimizado con las sugerencias de la herramienta de IA y un informe de evaluación del impacto.

<details>
<summary>Pistas de conocimiento</summary>

- Prácticas de refactorización y optimización de código.
- Evaluación de la calidad y rendimiento del código.

</details>

### Fase 3: Automatización de tareas repetitivas

**Objetivo:** Automatizar tareas repetitivas utilizando la herramienta de IA.

**Tiempo estimado:** 5 días

**Instrucciones:**

- Identificar tareas repetitivas en el flujo de trabajo de desarrollo.
- Configurar la herramienta de IA para automatizar estas tareas.
- Evaluación del tiempo y calidad mejorados tras la automatización.

**Entregable:** Configuración de la herramienta de IA para automatizar tareas repetitivas y un informe de evaluación del tiempo y calidad mejorados.

<details>
<summary>Pistas de conocimiento</summary>

- Identificación de tareas repetitivas en el flujo de trabajo.
- Configuración y uso de herramientas de IA para automatización.

</details>

## Dimensiones Evaluadas

- **queEs**: ¿Qué es una herramienta de IA generativa y cómo se integra en un entorno de desarrollo Backend .Net?
- **paraQueSirve**: ¿Para qué sirve utilizar una herramienta de IA generativa en el desarrollo de software?
- **comoSeUsa**: ¿Cómo se utiliza una herramienta de IA generativa para optimizar el código y automatizar tareas?
- **erroresComunes**: ¿Cuáles son los errores comunes al utilizar herramientas de IA generativa en el desarrollo de software?
- **queDecisionesImplica**: ¿Qué decisiones implica la integración y uso de una herramienta de IA generativa en el flujo de trabajo de desarrollo?

## Criterios de Evaluacion

- Configuración y documentación de la herramienta de IA generativa en el entorno de desarrollo.
- Código optimizado con las sugerencias de la herramienta de IA y un informe de evaluación del impacto.
- Configuración de la herramienta de IA para automatizar tareas repetitivas y un informe de evaluación del tiempo y calidad mejorados.

## Como trabajar con un asistente de IA

Hay dos caminos, elegi uno:

- **AGENTS.md** (recomendado) — instrucciones nativas del repo. Abri esta carpeta con tu agente local (Claude Code, Cursor, Codex, Copilot, Gemini) y las carga solo. Sabe que archivos faltan y con que comando se verifica, y completa el scaffold escribiendo en disco.
- **PROMPT_MEJORA.md** — para copiar y pegar en un chat (claude.ai, ChatGPT). Devuelve un ZIP con el proyecto. Sirve si no tenes un agente en el IDE.

Ninguno de los dos resuelve las fases del reto: eso es tu trabajo.

## Verificacion

El proyecto esta listo para trabajar cuando este comando corre sin errores:

```bash
dotnet build
```

---

*Reto generado automaticamente por Challenge Generator - Pragma*
