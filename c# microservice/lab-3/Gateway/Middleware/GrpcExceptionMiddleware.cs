using Grpc.Core;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Middleware;

public class GrpcExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GrpcExceptionMiddleware> _logger;

    public GrpcExceptionMiddleware(RequestDelegate next, ILogger<GrpcExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RpcException grpcEx)
        {
            _logger.LogError(grpcEx, "gRPC error intercepted");
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = grpcEx.StatusCode switch
            {
                StatusCode.InvalidArgument => StatusCodes.Status400BadRequest,
                StatusCode.NotFound => StatusCodes.Status404NotFound,
                StatusCode.FailedPrecondition => StatusCodes.Status412PreconditionFailed,
                StatusCode.PermissionDenied => StatusCodes.Status403Forbidden,
                StatusCode.OK => StatusCodes.Status200OK,
                StatusCode.Cancelled => StatusCodes.Status499ClientClosedRequest,
                StatusCode.Unknown => StatusCodes.Status500InternalServerError,
                StatusCode.DeadlineExceeded => StatusCodes.Status504GatewayTimeout,
                StatusCode.AlreadyExists => StatusCodes.Status409Conflict,
                StatusCode.Unauthenticated => StatusCodes.Status401Unauthorized,
                StatusCode.ResourceExhausted => StatusCodes.Status429TooManyRequests,
                StatusCode.Aborted => StatusCodes.Status409Conflict,
                StatusCode.OutOfRange => StatusCodes.Status400BadRequest,
                StatusCode.Unimplemented => StatusCodes.Status501NotImplemented,
                StatusCode.Internal => StatusCodes.Status500InternalServerError,
                StatusCode.Unavailable => StatusCodes.Status503ServiceUnavailable,
                StatusCode.DataLoss => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };

            await context.Response.WriteAsJsonAsync(new { error = grpcEx.Status.Detail });
        }
    }
}
