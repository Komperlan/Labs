package UserInterface.Scenarios;

import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;

public class MainMenuScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public MainMenuScenario(User activeUser, Repository logRepository, Collection<User> userList) {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Menu");

        if(ActiveUser != null) {
            System.out.println(ActiveUser.getFirstname() + " " + ActiveUser.getLastname());
        }

        System.out.println("Choose operation: ");
        System.out.println("1) Log in");
        System.out.println("2) Create account");
        System.out.println("3) Add money");
        System.out.println("4) widthdrawing money");

        Scanner operation = new Scanner(System.in);

        switch (operation.nextInt()){
            case 1:
                return new EnteringInAccountScenario(ActiveUser, LogRepository, UserList);
            case 2:
                return new AccountCreatingScenario(ActiveUser, LogRepository, UserList);
            case 3:
                return new AccountMoneyAddingScenario(ActiveUser, LogRepository, UserList);
            case 4:
                return new AccountMoneyWidthdrawingScenario(ActiveUser, LogRepository, UserList);
            default:
                return new ScenarioUnsuccesScenario(ActiveUser, LogRepository, UserList);
        }
    }
}