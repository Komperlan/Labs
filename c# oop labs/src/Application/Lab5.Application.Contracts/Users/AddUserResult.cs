namespace Lab5.Application.Contracts.Users;

public abstract record AddUserResult
{
    private AddUserResult() { }

    public sealed record Success : AddUserResult;

    public sealed record Unsuccess : AddUserResult;
}