using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Scores;
using Lab5.Application.Users;

namespace Lab5.Application.BankSystem;

internal class ShowScoreService : IShowScoreService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public ShowScoreService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public ShowScoreResult ShowScore(long userID)
    {
        Models.Users.User? user = _repository.FindUserByUserID(userID);

        if (user is null)
        {
            return new ShowScoreResult.NotFound();
        }

        _currentUserManager.User = user;
        return new ShowScoreResult.Success();
    }
}