package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Collection;

import static org.junit.jupiter.api.Assertions.*;
import java.math.BigDecimal;

class AccountMoneyShowerTest {
    @Test
    void schouldReturnNewAccountHaventMoney() {
        var users = new ArrayList<User>();
        var repo = new Repository();
        var creator = new AccountCreator("Edward", "Elric", 2003, users, repo);

        User user = creator.Execute();

        var shower = new AccountMoneyShower();

        assertEquals(shower.ShowBalance(user), new BigDecimal(0));
    }

    @Test
    void schouldReturnTrue_WhenAccountHaveCorrectMoney() {
        var users = new ArrayList<User>();
        var repo = new Repository();
        var creator = new AccountCreator("Edward", "Elric", 2003, users, repo);

        User user = creator.Execute();

        var shower = new AccountMoneyShower();

        var changer = new AccountMoneyChanger(AccountMoneyChanger.ChangingMoneyEnumType.AddMoney, new BigDecimal(2), repo, user);

        try {
            changer.Execute();
        }
        catch (Exception e) {

        }

        assertEquals(shower.ShowBalance(user), new BigDecimal(2));
    }
}