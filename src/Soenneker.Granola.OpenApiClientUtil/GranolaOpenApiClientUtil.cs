using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Granola.HttpClients.Abstract;
using Soenneker.Granola.OpenApiClientUtil.Abstract;
using Soenneker.Granola.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Granola.OpenApiClientUtil;

public sealed class GranolaOpenApiClientUtil : IGranolaOpenApiClientUtil
{
    private readonly AsyncSingleton<GranolaOpenApiClient> _client;

    public GranolaOpenApiClientUtil(IGranolaOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<GranolaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new GranolaOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<GranolaOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
