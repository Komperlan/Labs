using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.Users;

public class AdresseeUser : IAdressee
{
    public string Name { get; }

    public Dictionary<Guid, UserMessage> Messages { get; } = new Dictionary<Guid, UserMessage>();

    public AdresseeUser(string name, IEnumerable<UserMessage> messages)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Messages = messages?.ToDictionary(x => x.InnerMessage.ID, x => x) ?? throw new ArgumentNullException(nameof(messages));
    }

    public void AddMessage(Message message)
    {
        Messages.Add(message.ID, new UserMessage(message));
    }

    public UserMessage GetMessage(Guid id)
    {
        try
        {
            UserMessage message = Messages[id];
        }
        catch
        {
            return new UserMessage(new Message(" ", " ", 1));
        }

        Messages[id].ChangeReadStatus();
        return Messages[id];
    }
}
