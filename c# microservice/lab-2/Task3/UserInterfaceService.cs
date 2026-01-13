using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab2.Task3;

public class UserInterfaceService
{
    private readonly IOptionsMonitor<DisplayOptions> _options;
    private readonly Dictionary<string, IRenderer> _renderers;

    public UserInterfaceService(IOptionsMonitor<DisplayOptions> options, Dictionary<string, IRenderer> renderers)
    {
        _options = options;
        _renderers = renderers;
    }

    public void RenderContent()
    {
        _renderers[_options.CurrentValue.Mode].Render(_options.CurrentValue.Content);
        _options.OnChange(x => _renderers[x.Mode].Render(x.Content));
    }
}