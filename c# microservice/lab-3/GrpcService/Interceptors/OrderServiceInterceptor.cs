using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Text.Json;

namespace Itmo.CSharpMicroservices.Lab3.GrpcService.Interceptors;

public class OrderServiceInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception caught in interceptor: {ex}");
            Status status = MapExceptionToStatus(ex);

            var errorDetails = new
            {
                message = ex.Message,
                detail = ex.InnerException?.Message,
            };

            string detailJson = JsonSerializer.Serialize(errorDetails);

            throw new RpcException(new Status(status.StatusCode, status.Detail ?? ex.Message), detailJson);
        }
    }

    private Status MapExceptionToStatus(Exception ex)
    {
        return ex switch
        {
            ArgumentException => new Status(StatusCode.InvalidArgument, "Invalid argument"),
            KeyNotFoundException => new Status(StatusCode.NotFound, "Not found"),
            InvalidOperationException => new Status(StatusCode.FailedPrecondition, "Invalid operation"),
            UnauthorizedAccessException => new Status(StatusCode.PermissionDenied, "Permission denied"),
            _ => new Status(StatusCode.Internal, "Internal server error"),
        };
    }
}
