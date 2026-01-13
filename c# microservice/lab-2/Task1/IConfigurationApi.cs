using Refit;
using System.ComponentModel.DataAnnotations;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public interface IConfigurationApi
{
    [Get("/configurations")]
    Task<QueryConfigurationsResponse?> GetConfigurationItemDtosAsync(
        [Query][Range(1, 200)] int pageSize,
        CancellationToken cancellationToken,
        [Query] string? pageToken = null);
}
