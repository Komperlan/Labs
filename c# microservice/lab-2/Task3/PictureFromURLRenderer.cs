using Spectre.Console;

namespace Itmo.CSharpMicroservices.Lab2.Task3;

public class PictureFromURLRenderer : IRenderer
{
    public async Task Render(string content)
    {
        byte[] data = await new HttpClient().GetByteArrayAsync(content);
        using var ms = new MemoryStream(data);
        CanvasImage image = new CanvasImage(ms).MaxWidth(40);
        AnsiConsole.Clear();
        AnsiConsole.Write(image);
    }
}
