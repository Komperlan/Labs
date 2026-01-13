namespace Itmo.CSharpMicroservices.Lab4.Kafka.Options;

public class KafkaProducerOptions
{
    public string? BootstrapServers { get; set; } = null;

    public string OrderCreationTopic { get; set; } = string.Empty;

    public int BatchSize { get; set; }
}