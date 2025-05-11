using Itmo.ObjectOrientedProgramming.Lab3.Adressee;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class Topic
{
    public string Name { get; }

    private readonly ICollection<IAdressee> _adressees;

    public Topic(string name, ICollection<IAdressee> adressees)
    {
        Name = name;
        _adressees = adressees;
    }

    public void SendMessage(Messages.Message message)
    {
        foreach (IAdressee adressee in _adressees)
        {
            adressee.AddMessage(message);
        }
    }
}