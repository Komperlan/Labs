package BankSystem;

import lombok.AccessLevel;
import lombok.Getter;
import lombok.Setter;

import java.util.UUID;
import java.math.BigDecimal;

@Getter
public class User {
    @Setter(AccessLevel.PUBLIC)
    private BigDecimal Money = new BigDecimal(0);;

    private final String Firstname;
    private final String Lastname;

    private int Password;

    private final UUID ID;
    public User(String Firstname, String Lastname, int Password) {
        ID = UUID.randomUUID();
        this.Firstname = Firstname;
        this.Lastname = Lastname;
        this.Password = Password;
    }
}

