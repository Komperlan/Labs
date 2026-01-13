using Confluent.Kafka;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;

public interface IKafkaMessageHandler<TKey, TValue>
{
    Task HandleAsync(IReadOnlyCollection<ConsumeResult<TKey, TValue>> messages, CancellationToken cancellationToken);
}