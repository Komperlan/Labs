namespace Itmo.CSharpMicroservices.Lab4.GrpcService.Extensions;

public static class HtppClientExtension
{
    public static IServiceCollection AddHttp(this IServiceCollection services)
    {
        services.AddHttpClient("MyConfigClient", (provider, client) =>
        {
            IConfiguration config = provider.GetRequiredService<IConfiguration>();
            string baseUrl = config["ConfigurationService:Url"] ?? "http://localhost:8081";
            client.BaseAddress = new Uri(baseUrl);
        });

        return services;
    }
}
