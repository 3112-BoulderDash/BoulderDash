namespace Boulder_Dash;

public static class ScoreFileStorage
{
    private const string ScoreFilePath = "scores.txt";

    public static List<IScoreCard> LoadScores()
    {
        var result = new List<IScoreCard>();

        if (!File.Exists(ScoreFilePath))
            return result;

        string[] lines = File.ReadAllLines(ScoreFilePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length != 2)
                continue;

            string username = parts[0];

            if (!int.TryParse(parts[1], out int scoreCount))
                continue;

            // (username, score) constructor
            result.Add(new ScoreCard(username, scoreCount));
        }

        return result;
    }

    public static void AppendScore(IScoreCard score) //this should enter "Username, Score" into scores.txt
    {
        if (score == null)
            throw new ArgumentNullException(nameof(score));
        string line = $"{score.Username},{score.ScoreCount}";
        File.AppendAllText(ScoreFilePath, line + Environment.NewLine);
    }
}