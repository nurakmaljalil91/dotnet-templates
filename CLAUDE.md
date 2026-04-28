# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Purpose

This is a .NET template repository. The primary artifact is `WebApplicationTemplate/`, a `dotnet new` template (`web-api-template`) for generating Clean Architecture ASP.NET Core Web API solutions targeting .NET 10.

## Template Management

Install the template from the repo root:
```bash
dotnet new install .
```

Create a new project from the template:
```bash
dotnet new web-api-template -n MyService --safeName MyService
```

Uninstall:
```bash
dotnet new uninstall .
```

The `--safeName` parameter replaces all occurrences of `WebApplicationTemplate` (class names, namespaces, project names). Both `-n` and `--safeName` should match.

## Working Inside the Template Solution

All `dotnet` commands must run from `WebApplicationTemplate/`:

```bash
# Restore
dotnet restore WebApplicationTemplate.slnx

# Build
dotnet build WebApplicationTemplate.slnx --configuration Release --no-restore

# Run all tests
dotnet test WebApplicationTemplate.slnx --configuration Release --no-build

# Run a single test by name
dotnet test --filter "FullyQualifiedName~TodoLists_FullFlow_Works"

# Run tests in a specific project
dotnet test tests/IntegrationTests/IntegrationTests.csproj
```

Integration tests use an in-memory database and do not require a running PostgreSQL instance — `ApiFactory` sets `UseInMemoryDatabase=true`.

## Architecture

The template follows Clean Architecture with four layers:

```
Domain       → no dependencies on other layers
Application  → depends on Domain
Infrastructure → depends on Application (implements interfaces)
WebAPI       → depends on Application; entry point
```

**Domain** (`src/Domain/`) — entities, enums, value objects, shared primitives:
- `BaseEntity<TId>` — generic entity base with a typed `Id`
- `BaseAuditableEntity<TId>` — extends `BaseEntity` with Created/Modified audit fields
- `BaseResponse<T>` — standard API envelope (`Success`, `Message`, `Data`, `Errors`)
- `ValueObject` — base for DDD value objects (e.g. `Colour`)

**Application** (`src/Application/`) — CQRS handlers, validators, pipeline behaviors, interfaces:
- Each feature folder (e.g. `TodoLists/`) has `Commands/`, `Queries/`, and `Models/` subfolders
- Command/query, its handler, and its FluentValidation validator are co-located in the **same file**
- Uses [Mediator](https://github.com/martinothamar/Mediator) (source-generated, not MediatR) — `IRequest<T>`, `IRequestHandler<TRequest, TResponse>`, `IPipelineBehavior`
- Pipeline behaviors (registered automatically via `AddMediator`): `AuthorizationBehaviour` → `LoggingBehaviour` → `PerformanceBehaviour` → `UnhandledExceptionBehaviour` → `ValidationBehaviour`

**Infrastructure** (`src/Infrastructure/`) — EF Core, services:
- `ApplicationDbContext` uses PostgreSQL via Npgsql with snake_case naming and NodaTime
- `UseInMemoryDatabase` config key switches between in-memory (tests/dev) and PostgreSQL (prod)
- `AuditableEntityInterceptor` automatically sets `CreatedAt`/`ModifiedAt` on `SaveChanges`
- Entity type configurations live in `Data/Configurations/` (one file per entity)

**WebAPI** (`src/WebAPI/`) — thin controllers, middleware, DI setup:
- Controllers only call `_mediator.Send(...)` — no business logic
- `ExceptionHandlingMiddleware` maps `ValidationException` → 400, `NotFoundException` → 404, `ForbiddenAccessException` → 403, `UnauthorizedAccessException` → 401
- `CorrelationIdMiddleware` attaches a correlation ID to every request
- `AuthController` (`POST /api/Auth/login`) issues JWT tokens for dev/demo — **not for production**; replace with a real identity provider
- JWT configuration lives in `appsettings.json` under the `Jwt` section

## Key Conventions

**Application-level authorization** — decorate commands/queries with `[Authorize(Roles = "Admin")]` from `Application.Common.Security`. `AuthorizationBehaviour` enforces this before the handler runs, independently of ASP.NET Core's `[Authorize]`.

**All API responses use `BaseResponse<T>`** — use `BaseResponse<T>.Ok(data, message)` for success and `BaseResponse<T>.Fail(message, errors)` for failure.

**Validation** — add a nested `AbstractValidator<TCommand>` class in the same file as the command. `ValidationBehaviour` runs all registered validators and throws `Application.Common.Exceptions.ValidationException`, which `ExceptionHandlingMiddleware` converts to a 400 with the `Errors` dictionary.

**Central package management** — all NuGet package versions are pinned in `Directory.Packages.props`. Add `<PackageVersion>` entries there; reference packages in `.csproj` without a `Version` attribute.

**Build settings** — `Directory.Build.props` enables nullable reference types, implicit usings, `GenerateDocumentationFile`, and `SonarAnalyzer.CSharp` for all projects. XML doc comments are required on all public members in `src/` projects (enforced by the doc file generation).

## Docker

Development (uses `docker-compose.override.yml` automatically):
```bash
docker compose up --build
```

Production:
```bash
docker compose -f docker-compose.yml -f docker-compose.prod.yml up --build -d
```

## CI

GitHub Actions (`.github/workflows/ci.yml`) runs on every PR: restore → build (Release) → test (Release). All commands run from the `WebApplicationTemplate/` working directory.
