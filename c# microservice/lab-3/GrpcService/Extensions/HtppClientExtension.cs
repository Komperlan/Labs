namespace Itmo.CSharpMicroservices.Lab2.GrpcService.Extensions;

public static class HtppClientExtension
{
    public static IServiceCollection AddHttp(this IServiceCollection services)
    {
        services.AddHttpClient("MyConfigClient", (provider, client) =>
        {
            IConfiguration config = provider.GetRequiredService<IConfiguration>();
            string baseUrl = config["ConfigurationService:Url"] ?? "http://localhost:8080";
            client.BaseAddress = new Uri(baseUrl);
        });

        return services;
    }
}
