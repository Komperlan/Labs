using System.Text.Json.Serialization;

namespace Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

[JsonDerivedType(typeof(CreatedPayload), typeDiscriminator: "createdpayload")]
[JsonDerivedType(typeof(ItemAddedPayload), typeDiscriminator: "itemaddedpayload")]
[JsonDerivedType(typeof(ItemRemovedPayload), typeDiscriminator: "itemremovedpayload")]
[JsonDerivedType(typeof(StatusChangedPayload), typeDiscriminator: "statuschangedpayload")]
public abstract record OrderHistoryPayload;