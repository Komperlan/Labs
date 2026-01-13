namespace Itmo.CSharpMicroservices.Lab1.Task3;

public interface IMessageHandler
{
    ValueTask HandleAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
}