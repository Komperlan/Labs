package BankSystem.Commans;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;

import java.util.Collection;

public class AccountCreator {
    public Repository LogRepository;

    private Collection<User> UserList;
    private final String Firstname;
    private final String Lastname;
    private final int Password;

    public AccountCreator(String firstname, String lastname, int password, Collection<User> users, Repository repo) {
        this.Firstname = firstname;
        this.Lastname = lastname;
        this.Password = password;
        UserList = users;
        LogRepository = repo;
    }

    public User Execute(){
        User user = new User(Firstname, Lastname, Password);
        LogRepository.AddUser(user.getID());
        LogRepository.AddTransaction(new Transactions(user.getID(), Transactions.TransactionEnumType.CreateAccount), user.getID());
        UserList.add(user);
        return user;
    }
}
