using Lab5.Application.Contracts.Scores;
using Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.ChangeScore;

public class RemoveScoreScenarioProvider : IScenarioProvider
{
    private readonly IChangeScoreService _service;
    private readonly ICurrentUserService _currentUser;

    public RemoveScoreScenarioProvider(
        IChangeScoreService service,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is not null)
        {
            scenario = null;

            return false;
        }

        scenario = new RemoveScoreScenario(_service);
        return true;
    }
}