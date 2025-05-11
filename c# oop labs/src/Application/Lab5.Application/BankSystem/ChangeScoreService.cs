using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Scores;
using Lab5.Application.Users;

namespace Lab5.Application.BankSystem;

public class ChangeScoreService : IChangeScoreService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public ChangeScoreService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public AddScoreResult AddScore(long scores)
    {
        Models.Users.User? user = _currentUserManager.User;

        if (user is null)
        {
            return new AddScoreResult.NotFound();
        }

        _repository.UpdateUserScore(user.Score + scores, user.Id);
        return new AddScoreResult.Success();
    }

    public RemoveScoreResult RemoveScore(long scores)
    {
        Models.Users.User? user = _currentUserManager.User;

        if (user is null)
        {
            return new RemoveScoreResult.NotEnoughMoney();
        }

        if (user.Score < scores)
        {
            return new RemoveScoreResult.NotEnoughMoney();
        }

        _repository.UpdateUserScore(user.Score - scores, user.Id);

        _currentUserManager.User = user;
        return new RemoveScoreResult.Success();
    }
}