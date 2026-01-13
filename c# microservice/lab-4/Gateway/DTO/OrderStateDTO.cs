namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

public enum OrderStateDTO
{
    Created,
    Processing,
    Completed,
    Cancelled,
    Approval,
    Packed,
    Packing,
    InDelivery,
}