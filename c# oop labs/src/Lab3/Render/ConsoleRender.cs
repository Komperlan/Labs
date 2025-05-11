using Itmo.ObjectOrientedProgramming.Lab3.Adressee.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Render;

public class ConsoleRender : IRenderAble
{
    public DisplayDriver Driver { get; set; } = new DisplayDriver();

    public void Render(Message message)
    {
        Driver.Clear();
        Console.WriteLine(Driver.Color.Text(message.Title));
        Console.WriteLine(Driver.Color.Text(message.Body));
    }
}
