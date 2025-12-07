namespace Boulder_Dash;

public class ScoreCard : IScoreCard
{
    public int PlayerId { get; private set; }
    public string Username { get; private set; } 
    public int ScoreCount { get; private set; }

    // Used when you have both Id + username
    public ScoreCard(int playerId, int scoreCount, string username)
    {
        PlayerId = playerId;
        ScoreCount = scoreCount;
        Username = username;
    } // Used when loading from file (username,score)
    
    public ScoreCard(string username, int scoreCount)
    {
        Username = username;
        ScoreCount = scoreCount;
        PlayerId = 0; // new user
    }

    public string PrintScore()
    {
        return $"Player {Username} - Score: {ScoreCount}";
    }
}