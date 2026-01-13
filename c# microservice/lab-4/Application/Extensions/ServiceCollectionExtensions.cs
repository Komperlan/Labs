using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Persistance;
using Itmo.CSharpMicroservices.Lab4.Persistance.Migrations;
using Itmo.CSharpMicroservices.Lab4.Persistance.Repository;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Itmo.CSharpMicroservices.Lab3.Persistance.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseOptions"));

        services.AddSingleton(sp =>
        {
            DatabaseOptions options = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            return new NpgsqlDataSourceBuilder(options.ConnectionString).Build();
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();

        return services;
    }

    public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetSection("DatabaseOptions:ConnectionString").Value)
                .ScanIn(typeof(CreateProductOrderSchema).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .Configure<SelectingProcessorAccessorOptions>(cfg =>
            {
                cfg.ProcessorId = "PostgreSQL";
            });

        return services;
    }
}
