namespace BoulderDash;

public class ScoreCard : IScoreCard
{
    public int PlayerId { get; private set; }
    public int ScoreCount { get; private set; }

    public ScoreCard(int playerId, int scoreCount)
    {
        PlayerId = playerId;
        ScoreCount = scoreCount;
    }

    //returns player name and score
    public string PrintScore()
    {
        return $"Player {PlayerId} - Score: {ScoreCount}";
    }
}