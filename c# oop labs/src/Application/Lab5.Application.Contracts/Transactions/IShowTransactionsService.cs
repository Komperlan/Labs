namespace Lab5.Application.Contracts.Transactions;

public interface IShowTransactionsService
{
    ICollection<string>? ShowTransactions();
}
