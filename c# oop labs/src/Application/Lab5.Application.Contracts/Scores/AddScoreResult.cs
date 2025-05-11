namespace Lab5.Application.Contracts.Scores;

public abstract record AddScoreResult
{
    private AddScoreResult() { }

    public sealed record Success : AddScoreResult;

    public sealed record NotFound : AddScoreResult;
}