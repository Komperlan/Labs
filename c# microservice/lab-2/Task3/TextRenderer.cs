using Spectre.Console;

namespace Itmo.CSharpMicroservices.Lab2.Task3;

public class TextRenderer : IRenderer
{
    public Task Render(string content)
    {
        AnsiConsole.Write(
            new FigletText(content)
            .LeftJustified()
            .Color(Color.Red));
        return Task.CompletedTask;
    }
}