namespace Lab5.Application.Contracts.Scores;

public abstract record ShowScoreResult
{
    private ShowScoreResult() { }

    public sealed record Success : ShowScoreResult;

    public sealed record NotFound : ShowScoreResult;
}