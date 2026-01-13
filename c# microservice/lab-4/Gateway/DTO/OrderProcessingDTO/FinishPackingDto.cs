namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;

public record FinishPackingDto(bool IsSuccessful, string? FailureReason = null);