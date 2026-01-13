using Itmo.CSharpMicroservices.Lab2.Task1;
using Itmo.CSharpMicroservices.Lab2.Task2;
using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.Extensions;
using Itmo.CSharpMicroservices.Lab3.Gateway.Middleware;
using Itmo.CSharpMicroservices.Lab3.Gateway.Services;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace Itmo.CSharpMicroservices.Lab3.Gateway;

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

        builder.Services.AddScoped<IOrderService, OrderServiceWrapper>();
        builder.Services.AddScoped<IProductService, ProductServiceWrapper>();

        builder.Services.AddSwaggerGen();

        builder.Services.AddGrpcService();

        builder.Services.AddControllers().AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        WebApplication app = builder.Build();

        app.UseMiddleware<GrpcExceptionMiddleware>();

        ConfigurationService configService = app.Services.GetRequiredService<ConfigurationService>();
        _ = Task.Run(() => configService.ProcessData());

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}