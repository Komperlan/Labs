using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Render;

public class MessengerRender : IRenderAble
{
    public MessengerRender()
    {
    }

    public void Render(Message message)
    {
        Console.WriteLine("(Messenger) " + message.Title);
        Console.WriteLine("(Messenger) " + message.Body);
    }
}
