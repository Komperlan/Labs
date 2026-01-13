using Confluent.Kafka;
using Google.Protobuf;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Serializers;

public class ProtoSerializer<T> : ISerializer<T> where T : IMessage<T>, new()
{
    public byte[]? Serialize(T data, SerializationContext context)
    {
        return data.ToByteArray() ?? null;
    }
}