using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;

namespace Lab5.Application.Users;

internal class AddUserService : IAddUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public AddUserService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public AddUserResult AddUser(string username, long password)
    {
        Models.Users.User? user = _repository.CreateUser(username, password);

        if (user is null)
        {
            return new AddUserResult.Unsuccess();
        }

        _currentUserManager.User = user;
        return new AddUserResult.Success();
    }
}
