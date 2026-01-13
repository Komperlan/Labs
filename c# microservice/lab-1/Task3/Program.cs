using System.Threading.Channels;

namespace Itmo.CSharpMicroservices.Lab1.Task3;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var implementation = new MessageProcessor(
            new[] { new ConsoleMessageHandler() },
            new MessageProcessorOptions(
                Capacity: 1024,
                BatchSize: 50,
                BatchFlushInterval: TimeSpan.FromMilliseconds(250),
                FullMode: BoundedChannelFullMode.DropOldest));

        #pragma warning disable CA1859
        IMessageProcessor processor = implementation;
        IMessageSender sender = implementation;
        #pragma warning restore CA1859

        Task processingTask = processor.ProcessAsync(CancellationToken.None);

        await Parallel.ForEachAsync(Enumerable.Range(1, 1000), async (i, ct) =>
            await sender.SendAsync(new Message($"msg-{i}", $"payload #{i}"), ct));

        processor.Complete();
        await processingTask;
    }
}
