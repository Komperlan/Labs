namespace Itmo.CSharpMicroservices.Lab4.Kafka.Options;

public class KafkaConsumerOptions
{
    public string BootstrapServers { get; set; } = string.Empty;

    public string OrderProcessingTopic { get; set; } = string.Empty;

    public string GroupId { get; set; } = string.Empty;

    public int BatchSize { get; set; }
}