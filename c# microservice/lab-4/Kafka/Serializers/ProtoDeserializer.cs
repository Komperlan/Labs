using Confluent.Kafka;
using Google.Protobuf;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Serializers;

public class ProtoDeserializer<T> : IDeserializer<T> where T : IMessage<T>, new()
{
    private readonly MessageParser<T> _parser;

    public ProtoDeserializer()
    {
        _parser = new MessageParser<T>(() => new T());
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return _parser.ParseFrom(data.ToArray());
    }
}