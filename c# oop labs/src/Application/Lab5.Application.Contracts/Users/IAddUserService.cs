namespace Lab5.Application.Contracts.Users;

public interface IAddUserService
{
    AddUserResult AddUser(string username, long scores);
}