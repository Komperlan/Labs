using Lab5.Application.BankSystem;
using Lab5.Application.Contracts.Scores;
using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IShowScoreService, ShowScoreService>();
        collection.AddScoped<IShowTransactionsService, ShowTransactionsService>();
        collection.AddScoped<IChangeScoreService, ChangeScoreService>();
        collection.AddScoped<IAddUserService, AddUserService>();

        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserManager>());

        return collection;
    }
}