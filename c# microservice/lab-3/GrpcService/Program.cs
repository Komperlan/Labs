using Itmo.CSharpMicroservices.Lab2.GrpcService.Extensions;
using Itmo.CSharpMicroservices.Lab2.Task1;
using Itmo.CSharpMicroservices.Lab2.Task2;
using Itmo.CSharpMicroservices.Lab3.Core.Service;
using Itmo.CSharpMicroservices.Lab3.GrpcService.Interceptors;
using Itmo.CSharpMicroservices.Lab3.GrpcService.Services;
using Itmo.CSharpMicroservices.Lab3.Persistance.Extensions;
using Microsoft.Extensions.Options;

namespace Itmo.CSharpMicroservices.Lab3.GrpcService;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttp();
        builder.Services.AddSingleton<IConfigClient, MyConfigClient>();

        builder.Services.AddSingleton<CustomConfigurationProvider>();
        builder.Services.AddSingleton<IConfigurationSource, ConfigurationService>();

        builder.Services.AddSingleton<IOptionsMonitor<TimeSpan>>(provider => new OptionsMonitorWrapper(TimeSpan.FromSeconds(100)));
        builder.Services.AddSingleton<ConfigurationService>();

        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddMigrations(builder.Configuration);

        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<ProductService>();

        builder.Services.AddGrpc(options =>
        {
            options.Interceptors.Add<OrderServiceInterceptor>();
        });

        WebApplication app = builder.Build();

        ConfigurationService configService = app.Services.GetRequiredService<ConfigurationService>();
        _ = Task.Run(() => configService.ProcessData());

        app.Services.MigrateDatabase();
        app.MapGrpcService<OrderGrpcService>();
        app.MapGrpcService<ProductGrpcService>();

        app.Run();
    }
}
