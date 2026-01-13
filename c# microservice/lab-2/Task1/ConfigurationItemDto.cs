using System.Text.Json.Serialization;

namespace Itmo.CSharpMicroservices.Lab2.Task1;

public readonly record struct ConfigurationItemDto(
    [property: JsonPropertyName("key")] string Key,
    [property: JsonPropertyName("value")] string? Value);