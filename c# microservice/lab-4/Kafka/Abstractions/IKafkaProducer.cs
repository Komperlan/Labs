namespace Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;

public interface IKafkaProducer<in TKey, in TValue>
{
    Task ProduceAsync(string topic, TKey key, TValue value, CancellationToken cancellationToken = default);
}