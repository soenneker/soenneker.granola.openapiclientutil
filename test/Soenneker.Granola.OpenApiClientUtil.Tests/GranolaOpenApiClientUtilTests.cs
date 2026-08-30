using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Granola.HttpClients.Abstract;
using Soenneker.Granola.OpenApiClientUtil.Abstract;
using Soenneker.Granola.OpenApiClientUtil.Registrars;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Granola.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GranolaOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IGranolaOpenApiClientUtil _openapiclientutil;

    public GranolaOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IGranolaOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }

    [Test]
    public async Task Scoped_utility_keeps_http_client_singleton()
    {
        var services = new ServiceCollection();

        services.AddGranolaOpenApiClientUtilAsScoped();

        ServiceDescriptor httpClient = services.Single(descriptor => descriptor.ServiceType == typeof(IGranolaOpenApiHttpClient));
        ServiceDescriptor clientUtil = services.Single(descriptor => descriptor.ServiceType == typeof(IGranolaOpenApiClientUtil));

        await Assert.That(httpClient.Lifetime).IsEqualTo(ServiceLifetime.Singleton);
        await Assert.That(clientUtil.Lifetime).IsEqualTo(ServiceLifetime.Scoped);
    }
}
