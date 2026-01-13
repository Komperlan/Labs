using Refit;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public class MyConfigClient : IConfigClient, IGetAllAble
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MyConfigClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async IAsyncEnumerable<ConfigurationItemDto> GetAll([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var allItems = new List<ConfigurationItemDto>();
        string? pageToken = null;

        do
        {
            QueryConfigurationsResponse? response = await GetConfigurationItemDtosAsync(100, cancellationToken, pageToken);
            if (response == null)
            {
                break;
            }

            if (response.Items != null)
            {
                foreach (ConfigurationItemDto item in response.Items)
                {
                    yield return item;
                }
            }

            pageToken = response.PageToken;
        }
        while (!string.IsNullOrEmpty(pageToken) && !cancellationToken.IsCancellationRequested);
    }

    public async Task<QueryConfigurationsResponse?> GetConfigurationItemDtosAsync(
        [Query][Range(1, 200)] int pageSize,
        CancellationToken cancellationToken,
        [Query] string? pageToken = null)
    {
        using HttpClient client = _httpClientFactory.CreateClient("MyConfigClient");
        string url = $"configurations?pageSize={pageSize}";

        if (pageToken != null)
        {
            url += $"&pageToken={Uri.EscapeDataString(pageToken)}";
        }

        HttpResponseMessage response = await client.GetAsync(url);

        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<QueryConfigurationsResponse>(json);
    }
}
