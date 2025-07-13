package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.User;
import java.math.BigDecimal;

public class AccountMoneyShower {
    public BigDecimal ShowBalance(User user) {
        return user.getMoney();
    }
}
