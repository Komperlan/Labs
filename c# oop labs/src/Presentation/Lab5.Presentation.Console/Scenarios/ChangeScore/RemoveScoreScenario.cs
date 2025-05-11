using Lab5.Application.Contracts.Scores;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ChangeScore;

public class RemoveScoreScenario : IScenario
{
    private readonly IChangeScoreService _addScoreService;

    public RemoveScoreScenario(IChangeScoreService userService)
    {
        _addScoreService = userService;
    }

    public string Name => "Remove Score";

    public void Run()
    {
        long money = AnsiConsole.Ask<long>("Enter your money");

        RemoveScoreResult result = _addScoreService.RemoveScore(money);

        string message = result switch
        {
            RemoveScoreResult.Success => "Successful take your money",
            RemoveScoreResult.NotEnoughMoney => "money not enough",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}