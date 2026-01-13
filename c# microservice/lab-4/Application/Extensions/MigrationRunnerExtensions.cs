using FluentMigrator.Runner;

namespace Itmo.CSharpMicroservices.Lab4.Persistance.Extensions;

public static class MigrationRunnerExtensions
{
    public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();

        return serviceProvider;
    }
}
