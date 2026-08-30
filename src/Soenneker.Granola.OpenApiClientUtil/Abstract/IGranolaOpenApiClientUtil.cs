using Soenneker.Granola.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Granola.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IGranolaOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the shared generated client used to call the Granola API.
    /// </summary>
    /// <param name="cancellationToken">Stops client initialization if the shared instance has not been created yet.</param>
    /// <returns>The generated client cached for this utility's lifetime.</returns>
    ValueTask<GranolaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
