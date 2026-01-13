using Itmo.CSharpMicroservices.Lab2.Task1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab2.Task2;

public class ConfigurationService : IConfigurationSource
{
    private readonly CustomConfigurationProvider _configProvider;
    private readonly IConfigClient _configClient;
    private readonly int _configPageSize;
    private readonly IOptionsMonitor<TimeSpan> _optionRefreshInterval;

    public ConfigurationService(CustomConfigurationProvider configProvider, IConfigClient configClient, IOptionsMonitor<TimeSpan> optionRefreshInterval, int configPageSize = 10)
    {
        _configProvider = configProvider;
        _configClient = configClient;
        _configPageSize = configPageSize;
        _optionRefreshInterval = optionRefreshInterval;
    }

    public async void ProcessData()
    {
        using var timer = new PeriodicTimer(_optionRefreshInterval.CurrentValue);
        while (await timer.WaitForNextTickAsync())
        {
            QueryConfigurationsResponse? info = await _configClient.GetConfigurationItemDtosAsync(_configPageSize, CancellationToken.None);
            if (info == null)
            {
                continue;
            }

            _configProvider.Set(info.Items.ToDictionary(item => item.Key, item => item.Value));
        }
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder) => _configProvider;
}
