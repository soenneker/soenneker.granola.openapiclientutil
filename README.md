[![](https://img.shields.io/nuget/v/soenneker.granola.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.granola.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclientutil/)

# Soenneker.Granola.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Granola.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Granola.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddGranolaOpenApiClientUtilAsSingleton();
```

Adds `GranolaOpenApiClientUtil` as a singleton service.

## What you get

- `IGranolaOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `GranolaOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IGranolaOpenApiClientUtil.Get(cancellationToken)` | Gets the shared generated client used to call the Granola API. | The cached generated client; repeated calls reuse the same instance until this service is disposed. |
| `GranolaOpenApiClientUtilRegistrar.AddGranolaOpenApiClientUtilAsSingleton(services)` | Adds `GranolaOpenApiClientUtil` as a singleton service. | Returns `IServiceCollection`. |
| `GranolaOpenApiClientUtilRegistrar.AddGranolaOpenApiClientUtilAsScoped(services)` | Adds `GranolaOpenApiClientUtil` as a scoped service. | Returns `IServiceCollection`. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
