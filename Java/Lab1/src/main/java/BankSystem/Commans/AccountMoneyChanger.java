package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;

import java.math.BigDecimal;

public class AccountMoneyChanger {
    public enum ChangingMoneyEnumType {AddMoney, WithdrawMoney}

    private final ChangingMoneyEnumType ChangeMoneyType;
    private final BigDecimal Amount;

    public Repository LogRepository;
    private User User;

    public AccountMoneyChanger(ChangingMoneyEnumType changeMoneyType, BigDecimal amount, Repository repository, User user) {
        this.ChangeMoneyType = changeMoneyType;
        this.Amount = amount;
        this.LogRepository = repository;
        this.User = user;
    }

    private void WithdrawingMoneyFromAccount(BigDecimal amount, User user) throws Exception {
        LogRepository.AddTransaction(new Transactions(user.getID(), Transactions.TransactionEnumType.WithdrawingMoney), user.getID());
        if (user.getMoney().compareTo(amount) >= 0)
        {
            user.setMoney(user.getMoney().subtract(amount));
        }
        else {
            throw new Exception("not enough money");
        }
    }

    private void AddingMoneyToAccount(BigDecimal amount, User user) {
        LogRepository.AddTransaction(new Transactions(user.getID(), Transactions.TransactionEnumType.AddMoney), user.getID());
        user.setMoney(user.getMoney().add(amount));
    }

    public void Execute(){
        if(ChangeMoneyType == ChangingMoneyEnumType.AddMoney) {
            AddingMoneyToAccount(Amount, User);
        }
        else if(ChangeMoneyType == ChangingMoneyEnumType.WithdrawMoney) {
            try {
                WithdrawingMoneyFromAccount(Amount, User);
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
