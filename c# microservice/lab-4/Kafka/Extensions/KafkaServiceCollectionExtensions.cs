using Google.Protobuf;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Kafka.Consumer;
using Itmo.CSharpMicroservices.Lab4.Kafka.Options;
using Itmo.CSharpMicroservices.Lab4.Kafka.Producer;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab4.Kafka.Extensions;

public static class KafkaServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaProducer<TKey, TValue>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TKey : IMessage<TKey>, new()
        where TValue : IMessage<TValue>, new()
    {
        services.Configure<KafkaProducerOptions>(configuration.GetSection("Kafka"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaProducerOptions>>().Value);
        services.AddScoped<IKafkaProducer<TKey, TValue>, KafkaProducer<TKey, TValue>>();

        return services;
    }

    public static IServiceCollection AddKafkaConsumer<TKey, TValue, THandler>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TKey : IMessage<TKey>, new()
        where TValue : IMessage<TValue>, new()
        where THandler : class, IKafkaMessageHandler<TKey, TValue>
    {
        services.Configure<KafkaConsumerOptions>(configuration.GetSection("Kafka"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaConsumerOptions>>().Value);
        services.AddScoped<IKafkaMessageHandler<TKey, TValue>, THandler>();
        services.AddHostedService<KafkaConsumerBackgroundService<TKey, TValue>>();

        return services;
    }
}
