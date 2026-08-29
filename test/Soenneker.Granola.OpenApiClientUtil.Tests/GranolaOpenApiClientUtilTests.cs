using Soenneker.Granola.OpenApiClientUtil.Abstract;
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
}
