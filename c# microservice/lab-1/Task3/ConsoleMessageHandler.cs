using System.Text;

namespace Itmo.CSharpMicroservices.Lab1.Task3;

public class ConsoleMessageHandler : IMessageHandler
{
    public ValueTask HandleAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();

        foreach (Message m in messages)
        {
            sb.Append('[').Append(m.Title).Append("] ").AppendLine(m.Text);
        }

        Console.WriteLine(sb.ToString());
        return ValueTask.CompletedTask;
    }
}