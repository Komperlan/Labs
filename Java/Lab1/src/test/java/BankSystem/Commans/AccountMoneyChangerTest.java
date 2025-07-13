package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.User;
import org.junit.jupiter.api.Test;
import java.math.BigDecimal;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.assertEquals;

class AccountMoneyChangerTest {

    @Test
    void schouldReturnTrue_WhenAccountHaveCorrectMoneyAfterAdding() {
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

        assertEquals(user.getMoney(), new BigDecimal(2));
    }

    @Test
    void schouldReturnTrue_WhenAccountHaveCorrectMoneyAfterWidthdrawing() {
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

        changer = new AccountMoneyChanger(AccountMoneyChanger.ChangingMoneyEnumType.WithdrawMoney, new BigDecimal(1), repo, user);

        try {
            changer.Execute();
        }
        catch (Exception e) {

        }

        assertEquals(user.getMoney(), new BigDecimal(1));
    }
}