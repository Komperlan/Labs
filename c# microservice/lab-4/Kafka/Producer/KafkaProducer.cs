using Confluent.Kafka;
using Google.Protobuf;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Kafka.Options;
using Itmo.CSharpMicroservices.Lab4.Kafka.Serializers;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Producer;

public class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>, IDisposable
    where TKey : IMessage<TKey>, new()
    where TValue : IMessage<TValue>, new()
{
    private readonly IProducer<TKey, TValue> _producer;

    public KafkaProducer(IOptions<KafkaProducerOptions> options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            EnableIdempotence = true,
            Acks = Acks.All,
        };

        _producer = new ProducerBuilder<TKey, TValue>(config)
            .SetKeySerializer(new ProtoSerializer<TKey>())
            .SetValueSerializer(new ProtoSerializer<TValue>())
            .Build();
    }

    public async Task ProduceAsync(string topic, TKey key, TValue value, CancellationToken cancellationToken = default)
    {
        await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value }, cancellationToken);
    }

    public void Dispose() => _producer?.Dispose();
}
