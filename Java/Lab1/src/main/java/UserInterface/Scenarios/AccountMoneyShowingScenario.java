package UserInterface.Scenarios;

import BankSystem.Commans.AccountMoneyShower;
import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;

public class AccountMoneyShowingScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public AccountMoneyShowingScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        if(ActiveUser == null)
        {
            return new ScenarioUnsuccesScenario(ActiveUser, LogRepository, UserList);
        }

        var moneyShower = new AccountMoneyShower();

        System.out.println("Show Money");
        System.out.println(moneyShower.ShowBalance(ActiveUser));

        return new OperationSuccesScenario(ActiveUser, LogRepository, UserList);
    }
}
