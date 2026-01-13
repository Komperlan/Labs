using System.Collections.Concurrent;

namespace Itmo.CSharpMicroservices.Lab1.Task2;

public class RequestClient : IRequestClient, ILibraryOperationHandler
{
    private readonly ILibraryOperationService _service;
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<ResponseModel>> _operations = new();

    public RequestClient(ILibraryOperationService service)
    {
        _service = service;
    }

    public void HandleOperationError(Guid requestId, Exception exception)
    {
        if (_operations.TryRemove(requestId, out TaskCompletionSource<ResponseModel>? tcs))
        {
            tcs.TrySetException(exception);
        }
    }

    public void HandleOperationResult(Guid requestId, byte[] data)
    {
        if (_operations.TryRemove(requestId, out TaskCompletionSource<ResponseModel>? tcs))
        {
            tcs.TrySetResult(new ResponseModel(data));
        }
    }

    public Task<ResponseModel> SendAsync(RequestModel request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<ResponseModel>(cancellationToken);
        }

        var requestId = Guid.NewGuid();
        var tcs = new TaskCompletionSource<ResponseModel>(new ResponseModel(request.Data));

        _operations.TryAdd(requestId, tcs);

        cancellationToken.Register(() =>
        {
            if (_operations.TryRemove(requestId, out TaskCompletionSource<ResponseModel>? s))
            {
                s.TrySetCanceled(cancellationToken);
            }
        });

        _service.BeginOperation(requestId, request, cancellationToken);
        return tcs.Task;
    }
}