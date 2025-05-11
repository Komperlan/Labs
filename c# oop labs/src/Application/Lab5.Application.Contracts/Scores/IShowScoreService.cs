namespace Lab5.Application.Contracts.Scores;

public interface IShowScoreService
{
    ShowScoreResult ShowScore(long userID);
}