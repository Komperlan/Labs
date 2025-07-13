package UserInterface.Scenarios;

import BankSystem.Commans.TransactionsShower;
import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;

import java.util.Collection;
import java.util.List;
import java.util.Scanner;

public class TransactionShowingScenario extends AbstractScenario {
    private User ActiveUser;
    private Repository LogRepository;
    private Collection<User> UserList;

    public TransactionShowingScenario(User activeUser, Repository logRepository, Collection<User> userList)
    {
        ActiveUser = activeUser;
        this.LogRepository = logRepository;
        UserList = userList;
    }

    @Override
    public AbstractScenario Run() {
        System.out.println("Show Transactions");

        var Shower = new TransactionsShower();

        Collection<Transactions> transactions = Shower.ShowTransactions(ActiveUser, LogRepository);

        for(Transactions transaction : transactions){
            System.out.println(transaction.getTransactionType());
        }

        return new MainMenuScenario(ActiveUser, LogRepository, UserList);
    }
}
