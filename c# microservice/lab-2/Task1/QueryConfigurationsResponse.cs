using System.Text.Json.Serialization;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public record QueryConfigurationsResponse(
    [property: JsonPropertyName("items")] IEnumerable<ConfigurationItemDto> Items,
    [property: JsonPropertyName("pageToken")] string? PageToken);