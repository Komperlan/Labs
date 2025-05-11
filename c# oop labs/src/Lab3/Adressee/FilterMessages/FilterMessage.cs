using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.FilterMessages;

public class FilterMessage : IAdressee
{
    public int DegreeOfFiltration { get; }

    private readonly IAdressee _innerAdresee;

    public FilterMessage(int degreeOfFiltration, IAdressee innerAdresee)
    {
        DegreeOfFiltration = degreeOfFiltration;
        _innerAdresee = innerAdresee;
    }

    public void AddMessage(Message message)
    {
        if (message.Important <= DegreeOfFiltration)
        {
            return;
        }

        _innerAdresee.AddMessage(message);
    }
}
