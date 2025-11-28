namespace Boulder_Dash;

public interface IScoreCard
{
    int PlayerId { get; }
    int ScoreCount { get; }
    string PrintScore();

}