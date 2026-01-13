using System.ComponentModel.DataAnnotations;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public interface IConfigClient
{
    Task<QueryConfigurationsResponse?> GetConfigurationItemDtosAsync(
        [Range(1, 200)] int pageSize,
        CancellationToken cancellationToken,
        string? pageToken = null);
}
