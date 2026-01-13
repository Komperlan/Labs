namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;

public record FinishDeliveryDto(bool IsSuccessful, string? FailureReason = null);