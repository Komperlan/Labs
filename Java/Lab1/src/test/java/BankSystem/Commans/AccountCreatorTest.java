package BankSystem.Commans;

import static org.junit.jupiter.api.Assertions.assertEquals;

import BankSystem.Repository;
import BankSystem.User;
import org.junit.jupiter.api.*;

import java.util.ArrayList;



class AccountCreatorTest {

    @Test
     void schouldReturnNewAccountWithCorrectParametres() {
        var users = new ArrayList<User>();
        var repo = new Repository();
        var creator = new AccountCreator("Edward", "Elric", 2003, users, repo);

        User user = creator.Execute();
        assertEquals(user.getFirstname(), "Edward");
        assertEquals(user.getLastname(), "Elric");
        assertEquals(user.getPassword(), 2003);
    }
}