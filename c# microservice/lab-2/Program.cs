using Itmo.CSharpMicroservices.Lab2.Task3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab2;

public static class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.Configure<DisplayOptions>(options =>
        {
            options.Content = "GG";
            options.Mode = "text";
        });

        services.AddSingleton(sp =>
        {
            IOptionsMonitor<DisplayOptions> options = sp.GetRequiredService<IOptionsMonitor<DisplayOptions>>();
            var renderers = new Dictionary<string, IRenderer>
            {
                { "text", new TextRenderer() },
                { "imagebase64", new PictureRenderer() },
                { "imageurl", new PictureFromURLRenderer() },
            };
            return new UserInterfaceService(options, renderers);
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        UserInterfaceService ui = serviceProvider.GetRequiredService<UserInterfaceService>();
        ui.RenderContent();
    }
}
