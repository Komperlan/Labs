package UserInterface.Scenarios;

import BankSystem.Commans.AccountCreator;

import java.util.Collection;
import java.util.Scanner;

import BankSystem.Repository;
import BankSystem.User;

public class AccountCreatingScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public AccountCreatingScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Creating Account");
        System.out.println("Enter your firstname");
        Scanner firstname = new Scanner(System.in);
        System.out.println("Enter your lastname");
        Scanner lastname = new Scanner(System.in);
        System.out.println("Enter your password");
        Scanner password = new Scanner(System.in);
        int hashPassword = password.nextLine().hashCode();

        var creator = new AccountCreator(firstname.nextLine(), lastname.nextLine(), hashPassword, UserList, LogRepository);

        ActiveUser = creator.Execute();

        return new OperationSuccesScenario(ActiveUser, LogRepository, UserList);
    }
}
