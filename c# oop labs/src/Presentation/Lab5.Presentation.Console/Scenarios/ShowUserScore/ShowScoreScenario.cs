using Lab5.Application.Contracts.Scores;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ShowUserScore;

public class ShowScoreScenario : IScenario
{
    private readonly IShowScoreService _showScoreService;

    private readonly long _userID;

    public ShowScoreScenario(IShowScoreService userService, long userID)
    {
        _showScoreService = userService;
        _userID = userID;
    }

    public string Name => "Your money";

    public void Run()
    {
        ShowScoreResult result = _showScoreService.ShowScore(_userID);

        string message = result switch
        {
            ShowScoreResult.Success => "Successful login",
            ShowScoreResult.NotFound => "Money not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}