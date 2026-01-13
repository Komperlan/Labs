using Itmo.CSharpMicroservices.Lab2.Task2;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Extensions;

public static class GrpcServiceExtension
{
    public static IServiceCollection AddGrpcService(this IServiceCollection services)
    {
        services.AddGrpcClient<GrpcOrderService.GrpcOrderServiceClient>((provider, o) =>
        {
            CustomConfigurationProvider customProvider = provider.GetRequiredService<CustomConfigurationProvider>();
            if (customProvider.TryGet("Address", out string? address) && !string.IsNullOrEmpty(address))
            {
                o.Address = new Uri(address);
            }
            else
            {
                o.Address = new Uri("http://localhost:5254");
            }
        });

        services.AddGrpcClient<GrpcProductService.GrpcProductServiceClient>((provider, o) =>
        {
            CustomConfigurationProvider customProvider = provider.GetRequiredService<CustomConfigurationProvider>();
            if (customProvider.TryGet("Address", out string? address) && !string.IsNullOrEmpty(address))
            {
                o.Address = new Uri(address);
            }
            else
            {
                o.Address = new Uri("http://localhost:5254");
            }
        });

        return services;
    }
}
