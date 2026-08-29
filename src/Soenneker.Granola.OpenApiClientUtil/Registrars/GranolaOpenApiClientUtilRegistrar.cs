using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Granola.HttpClients.Registrars;
using Soenneker.Granola.OpenApiClientUtil.Abstract;

namespace Soenneker.Granola.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class GranolaOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="GranolaOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddGranolaOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGranolaOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IGranolaOpenApiClientUtil, GranolaOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="GranolaOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGranolaOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddGranolaOpenApiHttpClientAsSingleton()
                .TryAddScoped<IGranolaOpenApiClientUtil, GranolaOpenApiClientUtil>();

        return services;
    }
}
