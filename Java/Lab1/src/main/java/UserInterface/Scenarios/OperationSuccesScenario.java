package UserInterface.Scenarios;

import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;

public class OperationSuccesScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public OperationSuccesScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Your action has been completed");
        System.out.println("enter something to go to main menu");
        Scanner operation = new Scanner(System.in);
        if(operation.nextLine() == "something")
        {
            System.out.println("he-he ha-ha");
        }

        return new MainMenuScenario(ActiveUser, LogRepository, UserList);
    }
}