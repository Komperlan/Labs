package UserInterface.Scenarios;

import BankSystem.Commans.AccountCreator;
import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;

public class ScenarioUnsuccesScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public ScenarioUnsuccesScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Your action has not been completed");
        System.out.println("Enter something to go to main menu");
        Scanner operation = new Scanner(System.in);
        if(operation.nextLine() == "something")
        {
            System.out.println("he-he ha-ha");
        }

        return new MainMenuScenario(ActiveUser, LogRepository, UserList);
    }
}
