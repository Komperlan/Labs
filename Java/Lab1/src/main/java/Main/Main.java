package Main;

import BankSystem.Repository;
import BankSystem.Transactions;
import BankSystem.User;
import UserInterface.Scenarios.AccountCreatingScenario;
import UserInterface.Scenarios.AbstractScenario;
import UserInterface.Scenarios.MainMenuScenario;

import java.util.Arrays;
import java.util.Vector;

public class Main {
    public static void main( String[] args) {
        var users = new Vector<User>();
        var logs = new Repository();

        AbstractScenario Scenario = new MainMenuScenario(null, logs, users);

        while(true){
            Scenario = Scenario.Run();
            System.out.print("\033[H\033[2J"); //очистка вывода
        }
    }
}
