using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Render;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.Messengers;

public class Messenger : IAdressee
{
    private readonly IRenderAble _renderer = new MessengerRender();

    private Message _message = new Message(string.Empty, string.Empty, 1);

    public Messenger(IRenderAble renderer)
    {
        _renderer = renderer;
    }

    public void AddMessage(Message message)
    {
        _message = message;
    }

    public void Render()
    {
        _renderer.Render(_message);
    }
}
