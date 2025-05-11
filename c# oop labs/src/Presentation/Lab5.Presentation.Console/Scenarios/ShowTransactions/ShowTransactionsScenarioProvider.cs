using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.ShowTransactions;

public class ShowTransactionsScenarioProvider : IScenarioProvider
{
    private readonly IShowTransactionsService _service;
    private readonly ICurrentUserService _currentUser;

    public ShowTransactionsScenarioProvider(
        IShowTransactionsService service,
        ICurrentUserService currentUser,
        User user)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null)
        {
            scenario = null;

            return false;
        }

        scenario = new ShowTransactionsScenario(_service, _currentUser.User.Id);
        return true;
    }
}