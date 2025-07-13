package UserInterface.Scenarios;

import BankSystem.Commans.AccountMoneyChanger;
import BankSystem.Repository;
import BankSystem.User;

import java.util.Collection;
import java.util.Scanner;
import java.math.BigDecimal;

public class AccountMoneyWidthdrawingScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public AccountMoneyWidthdrawingScenario(User activeUser, Repository LogRepository, Collection<User> UserList)
    {
        ActiveUser = activeUser;
        LogRepository = LogRepository;
        UserList = UserList;
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

        var moneyChanger = new AccountMoneyChanger(AccountMoneyChanger.ChangingMoneyEnumType.WithdrawMoney,
                new BigDecimal(money.nextDouble()),  LogRepository, ActiveUser);

        try {
            moneyChanger.Execute();
        }
        catch (Exception e) {
            return new ScenarioUnsuccesScenario(ActiveUser, LogRepository, UserList);
        }
        return new OperationSuccesScenario(ActiveUser, LogRepository, UserList);
    }
}
