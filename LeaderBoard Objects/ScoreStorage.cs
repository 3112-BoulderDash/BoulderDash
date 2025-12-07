namespace Boulder_Dash
{
    public static class ScoreFileStorage
    {
        private const string ScoreFilePath = "scores.txt";
        public static List<IScoreCard> LoadScores(AccountFactory factory)
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

                // parts[0] = playerId
                if (!int.TryParse(parts[0], out int playerId))
                    continue;

                // parts[1] = score
                if (!int.TryParse(parts[1], out int scoreCount))
                    continue;

                // check if exists
                IAccount? account = factory.FindById(playerId);
                if (account == null)
                {
                    continue;
                }

                result.Add(new ScoreCard(playerId, scoreCount, account.Username));
            }

            return result;
        }

        // this will now enter PlayerId,Score into scores.txt
        public static void AppendScore(IScoreCard score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            string line = $"{score.PlayerId},{score.ScoreCount}";
            File.AppendAllText(ScoreFilePath, line + Environment.NewLine);
        }
    }
}