using Spectre.Console;

namespace Itmo.CSharpMicroservices.Lab2.Task3;

public class PictureRenderer : IRenderer
{
    public Task Render(string content)
    {
        byte[] data = Convert.FromBase64String(content);
        using var ms = new MemoryStream(data);
        CanvasImage image = new CanvasImage(ms).MaxWidth(40);
        AnsiConsole.Clear();
        AnsiConsole.Write(image);
        return Task.CompletedTask;
    }
}
