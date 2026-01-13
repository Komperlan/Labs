using System.Threading.Channels;

namespace Itmo.CSharpMicroservices.Lab1.Task3;

public sealed record MessageProcessorOptions(
    int Capacity = 10_000,
    int BatchSize = 256,
    TimeSpan? BatchFlushInterval = null,
    BoundedChannelFullMode FullMode = BoundedChannelFullMode.DropOldest,
    bool SingleReader = true,
    bool SingleWriter = false,
    bool AllowSynchronousContinuations = false);