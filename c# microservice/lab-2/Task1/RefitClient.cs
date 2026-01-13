using Refit;
using System.Runtime.CompilerServices;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public class RefitClient : IConfigClient, IGetAllAble
{
    private readonly string _baseurl;

    public RefitClient(string baseurl)
    {
        _baseurl = baseurl;
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

    public Task<QueryConfigurationsResponse?> GetConfigurationItemDtosAsync(int pageSize, CancellationToken cancellationToken, string? pageToken = null)
    {
        IConfigurationApi refitApi = RestService.For<IConfigurationApi>(_baseurl);

        return refitApi.GetConfigurationItemDtosAsync(pageSize, cancellationToken, pageToken);
    }
}
