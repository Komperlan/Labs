using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public class ChangeResultType : IResultType
{
    public bool IsSuccess { get; }

    public User User { get; }

    public ChangeResultType(bool isSucces, User user)
    {
        IsSuccess = isSucces;
        User = user;
    }
}
