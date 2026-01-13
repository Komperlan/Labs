using System.Threading.Channels;

namespace Itmo.CSharpMicroservices.Lab1.Task3;

public class MessageProcessor : IMessageSender, IMessageProcessor
{
    private readonly Channel<Message> _channel;
    private readonly IReadOnlyList<IMessageHandler> _handlers;
    private readonly MessageProcessorOptions _options;

    public ValueTask SendAsync(Message message, CancellationToken cancellationToken) =>
        _channel.Writer.WriteAsync(message, cancellationToken);

    public MessageProcessor(
        IEnumerable<IMessageHandler> handlers,
        MessageProcessorOptions options,
        Action<Message>? onDrop = null)
    {
        _handlers = handlers.ToList();
        _options = options;

        var bco = new BoundedChannelOptions(_options.Capacity)
        {
            FullMode = _options.FullMode,
            SingleReader = _options.SingleReader,
            SingleWriter = _options.SingleWriter,
            AllowSynchronousContinuations = _options.AllowSynchronousContinuations,
        };

        _channel = onDrop is null
            ? Channel.CreateBounded<Message>(bco)
            : Channel.CreateBounded(bco, onDrop);
    }

    public void Complete()
    {
        _channel.Writer.TryComplete();
    }

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        if (_options.BatchFlushInterval is TimeSpan flush)
        {
            await foreach (IReadOnlyList<Message> batch in _channel.Reader
                .ReadAllAsync(cancellationToken)
                .ChunkAsync(_options.BatchSize, flush))
            {
                foreach (IMessageHandler h in _handlers)
                    await h.HandleAsync(batch, cancellationToken);
            }

            return;
        }

        await RunWithSizeOnlyAsync(cancellationToken);
    }

    private async Task RunWithSizeOnlyAsync(CancellationToken ct)
    {
        var batch = new List<Message>(_options.BatchSize);

        await foreach (Message msg in _channel.Reader.ReadAllAsync(ct))
        {
            batch.Add(msg);
            if (batch.Count >= _options.BatchSize)
            {
                await DispatchAsync(batch, ct);
                batch.Clear();
            }
        }

        if (batch.Count > 0)
            await DispatchAsync(batch, ct);
    }

    private async Task DispatchAsync(List<Message> batch, CancellationToken ct)
    {
        Message[] frozen = batch.ToArray();
        foreach (IMessageHandler h in _handlers)
            await h.HandleAsync(frozen, ct);
    }
}
