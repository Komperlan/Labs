using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.Groups;

public class Group : IAdressee
{
    public ICollection<IAdressee> Adressees { get; } = new List<IAdressee>();

    public void AddMessage(Message message)
    {
        foreach (IAdressee item in Adressees)
        {
            item.AddMessage(message);
        }
    }

    public Group(ICollection<IAdressee> adressees)
    {
        Adressees = adressees ?? throw new ArgumentNullException(nameof(adressees));
    }
}
