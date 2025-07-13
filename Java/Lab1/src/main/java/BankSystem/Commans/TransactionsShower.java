package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;

import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.util.UUID;

public class TransactionsShower {
    public static Collection<Transactions> ShowTransactions(User user, Repository transactionLog) {
        return transactionLog.GetTransactions(user.getID());
    }
}
