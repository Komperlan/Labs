namespace Lab5.Application.Contracts.Users;

public interface IUserService
{
    LoginResult Login(string username);
}