using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Transactions;
using Lab5.Application.Users;

namespace Lab5.Application.BankSystem;

internal class ShowTransactionsService : IShowTransactionsService
{
    private readonly ITransactionsRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public ShowTransactionsService(ITransactionsRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public ICollection<string>? ShowTransactions()
    {
        if (_currentUserManager.User == null)
            return null;
        return _repository.ShowTransactions(_currentUserManager.User.Id);
    }
}