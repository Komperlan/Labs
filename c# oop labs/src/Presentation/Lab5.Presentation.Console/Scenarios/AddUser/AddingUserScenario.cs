using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.AddUser;

public class AddingUserScenario : IScenario
{
    private readonly IAddUserService _addUserService;

    public AddingUserScenario(IAddUserService userService)
    {
        _addUserService = userService;
    }

    public string Name => "Add User";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Enter your name");

        long password = AnsiConsole.Ask<long>("Enter your password");

        AddUserResult result = _addUserService.AddUser(name, password);

        string message = result switch
        {
            AddUserResult.Success => "Your account is created",
            AddUserResult.Unsuccess => "Your account is not created",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}