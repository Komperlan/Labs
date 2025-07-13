package BankSystem;

import java.util.UUID;
import lombok.AccessLevel;
import lombok.Getter;
import lombok.Setter;

@Getter(AccessLevel.PUBLIC)
public class Transactions {

    public enum TransactionEnumType {CreateAccount, ShowMoney, AddMoney, WithdrawingMoney, EnterInAccount}
    @Getter(AccessLevel.PUBLIC)
    private final UUID UserID;

    private final UUID TransactionID;
    
    @Getter(AccessLevel.PUBLIC)
    private final TransactionEnumType TransactionType;

    public Transactions(UUID userID, TransactionEnumType transactionType) {
        this.UserID = userID;
        this.TransactionID = UUID.randomUUID();
        this.TransactionType = transactionType;
    }
}
