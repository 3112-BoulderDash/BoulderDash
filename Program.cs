namespace Boulder_Dash;
class Program
{
    //Single shared AccountFactory instance
    private static AccountFactory AccountFactory = new AccountFactory();
    static void Main(string[] args)
    {
        
        LeaderBoard leaderBoard = new LeaderBoard();
        foreach (var score in ScoreFileStorage.LoadScores())
        {
            leaderBoard.AddScore(score);
        }
        
        IAccount currentPlayer = RunLoginMenu();

        GameField gameField = GameField.GetGameInstance();
        Controller playerController = new Controller();
        // main menu will go here
        //RunMainMenu();
        gameField.StartGame(playerController);

        // temporary section - when bryan implements the health functionality
        // and how the game ends, all temp should change
        //TempScoreLoggingDemo(leaderBoard, currentPlayer);

        while (gameField.GameIsRunning())
        {
            gameField.RunGame();
        }
    }
    

    // login menu
    private static IAccount RunLoginMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("____ Boulder Dash ___");
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Create account");
            Console.Write("Select 1 or 2: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? string.Empty;
                IAccount? account = AccountFactory.FindByUsername(username);

                if (account != null)
                {
                    Console.WriteLine($"Welcome back, {account.Username} (ID {account.Id})!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return account;
                }
                Console.WriteLine("No account with that username. Press any key to return to menu.");
                Console.ReadKey(true);
            }
            else if (choice == "2")
            {
                Console.Write("Choose a username: ");
                string username = Console.ReadLine() ?? string.Empty;

                try
                {
                    IAccount account = AccountFactory.CreateAccount(username);
                    Console.WriteLine($"Account created! Username: {account.Username}, ID: {account.Id}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return account;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not create account: {ex.Message}");
                    Console.WriteLine("Press any key to return and retry.");
                    Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Press any key to return to menu.");
                Console.ReadKey(true);
            }
        }
    }

    // temp score logging, logic should stay similar once end to game loop is created.
   /* private static void TempScoreLoggingDemo(LeaderBoard leaderBoard, IAccount currentPlayer)
    {
        Console.Clear();
        Console.WriteLine($"Logged in as: {currentPlayer.Username} (ID {currentPlayer.Id})");
        Console.WriteLine("Enter a test score to log for this player, or just press Enter to skip:");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int score))
        {
            IScoreCard card = new ScoreCard(currentPlayer.Username, score);

            // add to in-memory leaderboard
            leaderBoard.AddScore(card);

            // append to scores.txt
            ScoreFileStorage.AppendScore(card);

            Console.WriteLine("Score saved to leaderboard and scores.txt.");
        }
        else
        {
            Console.WriteLine("No score logged (skipped).");
        }

        Console.WriteLine("Press any key to start the game...");
        Console.ReadKey(true); */
   
   //main menu
   public static void RunMainMenu()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Display Leaderboard");
            Console.WriteLine("3. Exit");
            
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //call for game start
                    //gameField.RunGame();
                    break;

                case "2":
                    //call display leaderboard
                    //leaderBoard.Display();
                    break;
                //exit
                case "3":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}



