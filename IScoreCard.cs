namespace BoulderDash;

public interface IScoreCard
{
    int PlayerId { get; }
    int ScoreCount { get; }
    string PrintScore();

}