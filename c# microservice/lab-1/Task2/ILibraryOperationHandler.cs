namespace Itmo.CSharpMicroservices.Lab1.Task2;

public interface ILibraryOperationHandler
{
    void HandleOperationResult(Guid requestId, byte[] data);

    void HandleOperationError(Guid requestId, Exception exception);
}