using Lab5.Application.Models.Users;

namespace Lab5.Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? FindUserByUsername(string username);

    User? FindUserByUserID(long userID);

    User? CreateUser(string username, long password);

    void UpdateUserScore(long score, long userID);
}