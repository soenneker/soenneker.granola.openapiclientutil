using Soenneker.Granola.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Granola.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IGranolaOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached Granola OpenAPI client.
    /// </summary>
    ValueTask<GranolaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
