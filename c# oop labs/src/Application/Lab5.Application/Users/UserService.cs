using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;

namespace Lab5.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public UserService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public LoginResult Login(string username)
    {
        Models.Users.User? user = _repository.FindUserByUsername(username);

        if (user is null)
        {
            return new LoginResult.NotFound();
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }
}