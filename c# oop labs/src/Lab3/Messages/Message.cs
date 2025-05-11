namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class Message
{
    public string Title { get; }

    public Guid ID { get; }

    public string Body { get; }

    public int Important { get; }

    public Message(string title, string body, int important)
    {
        ID = Guid.NewGuid();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Body = body ?? throw new ArgumentNullException(nameof(body));
        Important = important;
    }
}
