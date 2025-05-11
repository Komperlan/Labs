using Lab5.Application.Contracts.Scores;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.ShowUserScore;

public class ShowScoreScenarioProvider : IScenarioProvider
{
    private readonly IShowScoreService _service;
    private readonly User _currentUser;

    public ShowScoreScenarioProvider(
        IShowScoreService service,
        User currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ShowScoreScenario(_service, _currentUser.Id);
        return true;
    }
}