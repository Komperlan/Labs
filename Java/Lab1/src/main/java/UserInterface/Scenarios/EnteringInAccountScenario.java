package UserInterface.Scenarios;

import BankSystem.Commans.AccountCreator;
import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;

public class EnteringInAccountScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public EnteringInAccountScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Enter your firstname");
        Scanner firstname = new Scanner(System.in);
        System.out.println("Enter your lastname");
        Scanner lastname = new Scanner(System.in);
        System.out.println("Enter your password");
        Scanner password = new Scanner(System.in);
        int hashPassword = password.nextLine().hashCode();

        for(User user : UserList)
        {
            if(user.getFirstname() == firstname.nextLine() && user.getLastname() == lastname.nextLine() && user.getPassword() == hashPassword)
            {
                ActiveUser = user;
            };
        }

        if(ActiveUser == null)
        {
            return new ScenarioUnsuccesScenario(ActiveUser, LogRepository, UserList);
        }

        return new OperationSuccesScenario(ActiveUser, LogRepository, UserList);
    }
}
