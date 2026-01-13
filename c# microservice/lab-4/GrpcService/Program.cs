using Itmo.CSharpMicroservices.Lab2.Task1;
using Itmo.CSharpMicroservices.Lab2.Task2;
using Itmo.CSharpMicroservices.Lab3.Persistance.Extensions;
using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Handler;
using Itmo.CSharpMicroservices.Lab4.Core.Service;
using Itmo.CSharpMicroservices.Lab4.Core.Wrapper;
using Itmo.CSharpMicroservices.Lab4.GrpcService.Extensions;
using Itmo.CSharpMicroservices.Lab4.GrpcService.Interceptors;
using Itmo.CSharpMicroservices.Lab4.GrpcService.Services;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Kafka.Extensions;
using Itmo.CSharpMicroservices.Lab4.Persistance.Extensions;
using Microsoft.Extensions.Options;
using Orders.Kafka.Contracts;

namespace Itmo.CSharpMicroservices.Lab4.GrpcService;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttp();
        builder.Services.AddSingleton<IConfigClient, MyConfigClient>();

        builder.Services.AddSingleton<CustomConfigurationProvider>();
        builder.Services.AddSingleton<IConfigurationSource, ConfigurationService>();

        builder.Services.AddSingleton<IOptionsMonitor<TimeSpan>>(provider => new OptionsMonitorWrapper(TimeSpan.FromSeconds(1000)));
        builder.Services.AddSingleton<ConfigurationService>();

        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddMigrations(builder.Configuration);

        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<ProductService>();

        builder.Services.AddGrpc(options =>
        {
            options.Interceptors.Add<OrderServiceInterceptor>();
        });

        builder.Services.AddScoped<ProducerWrapper>();
        builder.Services.AddKafkaProducer<OrderCreationKey, OrderCreationValue>(builder.Configuration);

        builder.Services.AddScoped<IKafkaMessageHandler<OrderProcessingKey, OrderProcessingValue>, OrderProcessingMessageHandler>();
        builder.Services.AddKafkaConsumer<OrderProcessingKey, OrderProcessingValue, OrderProcessingMessageHandler>(builder.Configuration);

        WebApplication app = builder.Build();

        app.Services.MigrateDatabase();
        app.MapGrpcService<OrderGrpcService>();
        app.MapGrpcService<ProductGrpcService>();

        app.Run();
    }
}
