using System.Text.Json.Serialization;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

[JsonDerivedType(typeof(CreatedPayloadDTO), typeDiscriminator: "createdpayload")]
[JsonDerivedType(typeof(ItemAddedPayloadDTO), typeDiscriminator: "itemaddedpayload")]
[JsonDerivedType(typeof(ItemRemovedPayloadDTO), typeDiscriminator: "itemremovedpayload")]
[JsonDerivedType(typeof(StatusChangedPayloadDTO), typeDiscriminator: "statuschangedpayload")]
public abstract record OrderHistoryPayloadDTO;