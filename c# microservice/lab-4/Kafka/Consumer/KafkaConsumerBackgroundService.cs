using Confluent.Kafka;
using Google.Protobuf;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Kafka.Options;
using Itmo.CSharpMicroservices.Lab4.Kafka.Serializers;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Consumer;

public class KafkaConsumerBackgroundService<TKey, TValue> : BackgroundService
    where TKey : IMessage<TKey>, new()
    where TValue : IMessage<TValue>, new()
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptions<KafkaConsumerOptions> _options;

    public KafkaConsumerBackgroundService(IServiceScopeFactory scopeFactory, IOptions<KafkaConsumerOptions> options)
    {
        _scopeFactory = scopeFactory;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var config = new ConsumerConfig
        {
            BootstrapServers = _options.Value.BootstrapServers,
            GroupId = _options.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
        };

        using IConsumer<TKey, TValue> consumer = new ConsumerBuilder<TKey, TValue>(config)
            .SetKeyDeserializer(new ProtoDeserializer<TKey>())
            .SetValueDeserializer(new ProtoDeserializer<TValue>())
            .Build();

        consumer.Subscribe(_options.Value.OrderProcessingTopic);

        var batch = new List<ConsumeResult<TKey, TValue>>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                for (int i = 0; i < _options.Value.BatchSize; i++)
                {
                    ConsumeResult<TKey, TValue> consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(100));

                    if (consumeResult == null)
                    {
                        break;
                    }

                    batch.Add(consumeResult);
                }

                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    IKafkaMessageHandler<TKey, TValue> handler = scope.ServiceProvider.GetRequiredService<IKafkaMessageHandler<TKey, TValue>>();

                    await handler.HandleAsync(batch, stoppingToken);
                }

                foreach (ConsumeResult<TKey, TValue> msg in batch)
                {
                    consumer.Commit(msg);
                }

                batch.Clear();
            }
            catch
            {
            }
        }

        consumer.Close();
    }
}