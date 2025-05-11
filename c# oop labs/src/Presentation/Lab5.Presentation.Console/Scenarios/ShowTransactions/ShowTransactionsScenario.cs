using Lab5.Application.Contracts.Transactions;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ShowTransactions;

public class ShowTransactionsScenario : IScenario
{
    private readonly IShowTransactionsService _showScoreService;

    public ShowTransactionsScenario(IShowTransactionsService userService, long userID)
    {
        _showScoreService = userService;
    }

    public string Name => "Show Transactions";

    public void Run()
    {
        ICollection<string>? result = _showScoreService.ShowTransactions();

        if (result == null)
        {
            AnsiConsole.WriteLine("Transactions not found");
            return;
        }

        foreach (string str in result)
        {
            AnsiConsole.WriteLine(str);
        }

        AnsiConsole.Ask<string>("Ok");
    }
}