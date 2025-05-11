using Itmo.ObjectOrientedProgramming.Lab3.Adressee.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Render;

public class FileRender : IRenderAble
{
    private readonly DisplayDriver _driver = new DisplayDriver();

    public FileRender(DisplayDriver driver)
    {
        _driver = driver;
    }

    public void Render(Message message)
    {
        using var sw = new StreamWriter("Message.Txt");
        sw.WriteLine(_driver.Color);
        sw.WriteLine(message.Title);
        sw.WriteLine(message.Body);
    }
}
