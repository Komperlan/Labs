using Lab5.Application.Contracts.Scores;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ChangeScore;

public class AddingScoreScenario : IScenario
{
    private readonly IChangeScoreService _addScoreService;

    public AddingScoreScenario(IChangeScoreService userService)
    {
        _addScoreService = userService;
    }

    public string Name => "Add Score";

    public void Run()
    {
        long money = AnsiConsole.Ask<long>("Enter your money");

        AddScoreResult result = _addScoreService.AddScore(money);

        string message = result switch
        {
            AddScoreResult.Success => "Successful enter money",
            AddScoreResult.NotFound => "money not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}