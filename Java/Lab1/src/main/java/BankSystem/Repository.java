package BankSystem;

import java.util.*;

public class Repository {
    /**
     * for javadoc
     * @param args
     */
    private Map<UUID, List<Transactions>> values;

    public Repository() {
        values = new HashMap<UUID, List<Transactions>>();
    }

    public void AddUser(UUID id) {
        values.put(id, new ArrayList<Transactions>());
    }

    public void AddTransaction(Transactions transaction, UUID id) {
        values.get(id).add(transaction);
    }

    public List<Transactions> GetTransactions(UUID id) {
        return values.get(id);
    }
}
