namespace Boulder_Dash
{
    public static class AccountFileStorage
    {
        private const string AccountFilePath = "accounts.txt";

        // Load accounts from accounts.txt into the given factory
        public static void LoadAccounts(AccountFactory factory)
        {
            if (!File.Exists(AccountFilePath))
                return;

            string[] lines = File.ReadAllLines(AccountFilePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length < 3)
                    continue;

                if (!int.TryParse(parts[0], out int id))
                    continue;

                string username = parts[1];
                if (!bool.TryParse(parts[2], out bool isAdmin))
                    isAdmin = false;

                // Let the factory create or ensure this account exists
                factory.CreateAccountFromFile(id, username, isAdmin);
            }
        }

        // Save all accounts currently in the factory to accounts.txt
        public static void SaveAccounts(AccountFactory factory)
        {
            var lines = new List<string>();

            foreach (var acc in factory.GetAllAccounts())
            {
                lines.Add($"{acc.Id},{acc.Username},{acc.IsAdmin}");
            }

            File.WriteAllLines(AccountFilePath, lines);
        }
    }
}