[![](https://img.shields.io/nuget/v/soenneker.granola.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclientutil/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclientutil/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.granola.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Granola.OpenApiClientUtil

Provides a lazily created Granola Kiota client over the shared Granola `HttpClient`.

## Install

```bash
dotnet add package Soenneker.Granola.OpenApiClientUtil
```

## Configuration

```json
{
  "Granola": {
    "ApiKey": "<API key>",
    "ClientBaseUrl": "https://public-api.granola.ai",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

Only `ApiKey` is required. The remaining values show their defaults.

## Register

```csharp
using Soenneker.Granola.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddGranolaOpenApiClientUtilAsScoped();
```

This deliberately registers `IGranolaOpenApiClientUtil` as scoped while registering `IGranolaOpenApiHttpClient` as singleton. Disposing a scope releases that utility's generated-client wrapper without tearing down the long-lived HTTP client used by later scopes.

Use `AddGranolaOpenApiClientUtilAsSingleton()` only when the generated-client wrapper itself should also live for the application lifetime.

## Usage

```csharp
GranolaOpenApiClient client = await clientUtil.Get(cancellationToken);

ListNotesOutput? page = await client.V1.Notes.GetAsync(config =>
{
    config.QueryParameters.PageSize = 50;
    config.QueryParameters.Cursor = cursor;
}, cancellationToken);

Note? note = await client.V1.Notes[noteId].GetAsync(
    cancellationToken: cancellationToken);
```

Within one utility instance, repeated and concurrent `Get()` calls reuse the same lazily initialized generated client.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `Get(cancellationToken)` | Gets or creates the generated client. | Cached for the utility lifetime. |
| `AddGranolaOpenApiClientUtilAsScoped()` | Registers a utility per scope over the singleton HTTP client. | Recommended for scoped consumers. |
| `AddGranolaOpenApiClientUtilAsSingleton()` | Registers both layers for the application lifetime. | Reuses one generated client everywhere. |

## Practical notes

- Cancellation can stop first-time client initialization and is forwarded separately to generated request methods.
- Let the DI container dispose the utility. Do not dispose the shared `HttpClient` obtained by the lower-level package.
