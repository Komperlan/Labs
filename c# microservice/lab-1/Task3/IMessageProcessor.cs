namespace Itmo.CSharpMicroservices.Lab1.Task3;

public interface IMessageProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken);

    void Complete();
}
