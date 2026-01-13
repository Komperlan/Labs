namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;

public record ApproveOrderDto(bool IsApproved, string ApprovedBy, string? FailureReason = null);