using Lab5.Application.Contracts.Scores;
using Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.ChangeScore;

public class AddingScoreScenarioProvider : IScenarioProvider
{
    private readonly IChangeScoreService _service;
    private readonly ICurrentUserService _currentUser;

    public AddingScoreScenarioProvider(
        IChangeScoreService service,
        ICurrentUserService currentUser)
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

        scenario = new AddingScoreScenario(_service);
        return true;
    }
}