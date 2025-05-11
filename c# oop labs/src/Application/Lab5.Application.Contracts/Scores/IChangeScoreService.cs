namespace Lab5.Application.Contracts.Scores;

public interface IChangeScoreService
{
    AddScoreResult AddScore(long scores);

    RemoveScoreResult RemoveScore(long scores);
}