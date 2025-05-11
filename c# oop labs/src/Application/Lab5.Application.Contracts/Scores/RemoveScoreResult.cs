namespace Lab5.Application.Contracts.Scores;

public abstract record RemoveScoreResult
{
    private RemoveScoreResult() { }

    public sealed record Success : RemoveScoreResult;

    public sealed record NotEnoughMoney : RemoveScoreResult;
}