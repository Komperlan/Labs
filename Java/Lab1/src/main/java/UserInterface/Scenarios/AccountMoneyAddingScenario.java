package UserInterface.Scenarios;

import BankSystem.Commans.AccountMoneyChanger;
import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;
import java.math.BigDecimal;

public class AccountMoneyAddingScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public AccountMoneyAddingScenario(User activeUser, Repository logRepository, Collection<User> userList)
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

        System.out.println("Add Money");
        System.out.println("Enter Money: ");
        Scanner money = new Scanner(System.in);

        var moneyChanger = new AccountMoneyChanger(AccountMoneyChanger.ChangingMoneyEnumType.AddMoney,
                new BigDecimal(money.nextDouble()),  LogRepository, ActiveUser);

        moneyChanger.Execute();

        return new OperationSuccesScenario(ActiveUser, LogRepository, UserList);
    }
}
