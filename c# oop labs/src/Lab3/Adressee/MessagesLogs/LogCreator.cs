using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.MessagesLogs;

public class LogCreator : IAdressee
{
    private readonly IAdressee _innerAdresee;

    public LogCreator(IAdressee innerAdresee)
    {
        _innerAdresee = innerAdresee;
    }

    public void AddMessage(Message message)
    {
        CreateLog(message);
        _innerAdresee.AddMessage(message);
    }

    public void CreateLog(Message message)
    {
        Console.WriteLine(message + "is added to" + _innerAdresee);
    }
}
