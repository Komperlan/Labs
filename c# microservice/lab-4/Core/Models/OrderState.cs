namespace Itmo.CSharpMicroservices.Lab4.Core.Models;

public enum OrderState
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