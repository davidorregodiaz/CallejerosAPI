# AGENTS.md - CallejerosAPI


## Estructura del Proyecto

Este proyecto es una API ASP.NET Core 9 con arquitectura DDD (Domain-Driven Design):

- `src/Callejeros.API/` - API web (controllers, middleware, configuration)
- `src/Callejeros.Domain/` - Dominio (aggregates, entities, domain events, repository interfaces)
- `src/Callejeros.Infrastructure/` - Infraestructura (EF Core, repositorios, servicios externos)
- `src/Callejeros.Shared/` - Utilidades compartidas (result types, helpers)

Los nombres de proyectos siguen el prefijo `Callejeros-`.

## Reglas de Código

### Convenciones Generales

- Usar siempre `record` para DTOs e inmutables.
- Preferir `string` sobre `String` en C#.
- Usar `var` cuando el tipo sea obvio.
- No abbreviate nombres de variables o parámetros (usa `userId` no `uid`).
- Usar `nameof()` en lugar de strings hardcodeados para nombres de propiedades.
- Usar null-conditional operator (`?.`) y null-coalescing (`??`) cuando aplique.

### Patrones DDD

- Los aggregates viven en `AggregatesModel/`.
- Repository interfaces van en el dominio; implementaciones en infraestructura.
- Domain events van en `Events/`.
- Entities usan `Id` como propiedad primaria (Guid por convención).
- Value objects son `record` inmutables.

### Entity Framework Core

- Usar `IQueryable<T>` en repository interfaces para permitir composicion.
- Siempre usar `AsNoTracking()` para queries de solo lectura.
- No usar `Include` en queries grandes; usar split queries o projection.
- Preferir Fluent API sobre DataAnnotations para configuración.
- Naming convention: `OnModelCreating` para configuraciones.

### Controllers y Endpoints

- Usar `[ApiController]` en todos los controllers.
- Preferir endpoints minimal con Carter o controller base tradicional.
- Siempre retornar `IActionResult<T>` o `ActionResult<T>`.
- Usar `[ProducesResponseType]` para documentar respuestas.
- Preferir `ProblemDetails` para errores HTTP estándar.

### Testing

-命名: `*.Tests.csproj` para proyectos de tests.
- Usar xUnit como framework de testing.
- Usar FluentAssertions para aserciones.
- Nombre de clases de test: `ClassNameTests`.
- Nombre de métodos: `MethodName_Scenario_ExpectedBehavior`.

### Estructura de Carpetas por Capa

```
src/
├── Callejeros.API/
│   ├── Controllers/
│   ├── Filters/
│   ├── Middleware/
│   └── Configuration/
├── Callejeros.Domain/
│   ├── AggregatesModel/
│   │   ├── AnimalAggregate/
│   │   ├── UserAggregate/
│   │   └── AdoptionAggregate/
│   ├── Events/
│   │   ├── Animal/
│   │   └── Adoption/
│   └── ValueObjects/
├── Callejeros.Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   └── Services/
└── Callejeros.Shared/
    ├── Results/
    └── Extensions/
```

## Comandos Útiles

### Desarrollo

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar con hot reload
dotnet watch --project src/Callejeros.API

# Solo ejecutar
dotnet run --project src/Callejeros.API
```

### Base de Datos

```bash
# Crear migración
dotnet ef migrations add MigrationName --project src/Callejeros.Infrastructure

# Aplicar migraciones
dotnet ef database update --project src/Callejeros.Infrastructure
```

### Testing

```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar tests con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

### Linting y Formato

```bash
# Formatear código
dotnet format

# Verificar análisis estática
dotnet build
```

## Configuración

- Archivo de configuración: `appsettings.Development.json` (no versionar).
- Variables de entorno tienen precedencia sobre `appsettings`.
- Connection strings van en `ConnectionStrings`.
- Secrets sensibles en User Secrets o environment variables.

## Docker

```bash
# Levantar servicios (PostgreSQL, Minio)
docker-compose up -d
```

## Notas Importantes

- **Regla de negocio**: Queda prohibida la venta de animales; la API solo permite gestión de adopciones.
- JWT authentication para usuarios.
- PostgreSQL como base de datos principal.
- Minio para almacenamiento de archivos (imágenes de animales).
- SMTP para notificaciones por email.
