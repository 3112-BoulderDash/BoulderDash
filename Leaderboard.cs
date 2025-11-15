using System.Collections.Generic;
using System.Linq;
namespace BoulderDash;

public class LeaderBoard
{
    // store  all score entries in a readonly list
    private readonly List<IScoreCard> _scores = new();

    public IReadOnlyList<IScoreCard> Scores => _scores.AsReadOnly();

    // add new score to the leaderboard and sorts highest to lowest.
    public void AddScore(IScoreCard scoreCard)
    {
        if (scoreCard == null)
            throw new ArgumentNullException(nameof(scoreCard));

        _scores.Add(scoreCard);
        _scores.Sort((a, b) => b.ScoreCount.CompareTo(a.ScoreCount));
    }

    // Gets the scorecard at a given index in the (sorted) list.
    // Returns null if the index is out of range

    public IScoreCard? GetScore(int index)
    {
        if (index < 0 || index >= _scores.Count)
            return null;

        return _scores[index];
    }

    // return all scores.
    public List<IScoreCard> GetTopScores()
    {
        return new List<IScoreCard>(_scores);
    }

    //use linq Take to return the users choice of top scores.
    public List<IScoreCard> GetTopScores(int count)
    {
        return _scores
            .Take(count)
            .ToList();
    }

    /// return all scores for a specific player by using linq keyword where.
    public List<IScoreCard> GetLocalScores(int userId)
    {
        return _scores
            .Where(s => s.PlayerId == userId)
            .ToList();
    }
}
