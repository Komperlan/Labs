using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab2.Task2;

public class OptionsMonitorWrapper : IOptionsMonitor<TimeSpan>
{
    public OptionsMonitorWrapper(TimeSpan value)
    {
        CurrentValue = value;
    }

    public TimeSpan CurrentValue { get; }

    public TimeSpan Get(string? name) => CurrentValue;

    public IDisposable? OnChange(Action<TimeSpan, string?> listener) => null;
}