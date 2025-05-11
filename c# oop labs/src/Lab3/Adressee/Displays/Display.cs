using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Render;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.Displays;

public class Display : IAdressee
{
    private readonly IRenderAble _renderer;

    private Message _message = new Message(" ", " ", 1);

    public void AddMessage(Message message)
    {
        _message = message;
    }

    public Display(int degreeOfFiltration, IRenderAble renderer)
    {
        _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    }

    public void Render()
    {
        _renderer.Render(_message);
    }
}
