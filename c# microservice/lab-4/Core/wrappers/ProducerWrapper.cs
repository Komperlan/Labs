using Google.Protobuf.WellKnownTypes;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Orders.Kafka.Contracts;

namespace Itmo.CSharpMicroservices.Lab4.Core.Wrapper;

public class ProducerWrapper
{
    private readonly IKafkaProducer<OrderCreationKey, OrderCreationValue> _producer;

    public ProducerWrapper(IKafkaProducer<OrderCreationKey, OrderCreationValue> producer)
    {
        _producer = producer;
    }

    public async Task ProduceOrderCreated(Order order)
    {
        await _producer.ProduceAsync("order_creation", new OrderCreationKey { OrderId = order.Id }, new OrderCreationValue
        {
            OrderCreated = new OrderCreationValue.Types.OrderCreated
            {
                OrderId = order.Id,
                CreatedAt = Timestamp.FromDateTime(order.CreatedAt),
            },
        });
    }

    public async Task ProduceOrderProcessed(Order order)
    {
        await _producer.ProduceAsync("order_creation", new OrderCreationKey { OrderId = order.Id }, new OrderCreationValue
        {
            OrderProcessingStarted = new OrderCreationValue.Types.OrderProcessingStarted
            {
                OrderId = order.Id,
                StartedAt = Timestamp.FromDateTime(DateTime.UtcNow),
            },
        });
    }
}
