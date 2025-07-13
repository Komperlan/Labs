package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Collection;

import static org.junit.jupiter.api.Assertions.*;

class TransactionsShowerTest {
    @Test
    void schouldReturnNewAccountHaveCreatingAccountTransactions() {
        var users = new ArrayList<User>();
        var repo = new Repository();
        var creator = new AccountCreator("Edward", "Elric", 2003, users, repo);

        User user = creator.Execute();

        var shower = new TransactionsShower();
        Collection<Transactions> transactions = shower.ShowTransactions(user, repo);

        assertEquals(transactions.size(), 1);

        Transactions transaction = transactions.iterator().next();

        assertEquals(transaction.getTransactionType(), Transactions.TransactionEnumType.CreateAccount);
        assertEquals(transaction.getUserID(), user.getID());
    }
}