namespace Lab5.Application.Abstractions.Repositories;

public interface ITransactionsRepository
{
    ICollection<string>? ShowTransactions(long userID);
}