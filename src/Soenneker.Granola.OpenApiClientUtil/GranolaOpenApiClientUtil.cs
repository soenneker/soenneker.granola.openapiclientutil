using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Granola.HttpClients.Abstract;
using Soenneker.Granola.OpenApiClientUtil.Abstract;
using Soenneker.Granola.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Granola.OpenApiClientUtil;

/// <inheritdoc cref="IGranolaOpenApiClientUtil"/>
public sealed class GranolaOpenApiClientUtil : IGranolaOpenApiClientUtil
{
    private readonly AsyncSingleton<GranolaOpenApiClient> _client;

    public GranolaOpenApiClientUtil(IGranolaOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<GranolaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Granola:ApiKey");
            string authHeaderName = configuration["Granola:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Granola:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

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
