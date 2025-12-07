namespace Boulder_Dash;
class Program
{
    //Single shared AccountFactory instance
    private static AccountFactory AccountFactory = new AccountFactory();
    private static IAccount currentPlayer;
    private static LeaderBoard leaderBoard;
    private static Game _game;
    private static Shop shop = new Shop();
    static void Main(string[] args)
    {
        
        leaderBoard = new LeaderBoard();
        foreach (var score in ScoreFileStorage.LoadScores())
        {
            leaderBoard.AddScore(score);
        }
        
        currentPlayer = RunLoginMenu();
        //TempScoreLoggingDemo(leaderBoard, currentPlayer);
        _game = Game.GetGameInstance();
        // main menu
        RunMainMenu();
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
    private static void TempScoreLoggingDemo(LeaderBoard leaderBoard, IAccount currentPlayer)
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
        Console.ReadKey(true);
    }

    //main menu
   public static void RunMainMenu()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Display Leaderboard");
            Console.WriteLine("3. Shop");
            Console.WriteLine("4. Equip Item");
            Console.WriteLine("5. Exit");
            
            string? input = Console.ReadLine();

            switch (input)
            {

                case "1":
                    //call for game start
                    _game.StartGame();
        

                    while (_game.GameIsRunning())
                    {
                        _game.RunGame();
                    }
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);
                    break;

                case "2":
                    //Leaderboard menu
                    bool inLeaderBoard = true;

                    while (inLeaderBoard)
                    {
                        Console.WriteLine("1. Display Your Scores");
                        Console.WriteLine("2. Display All Scores");
                        Console.WriteLine("3. Display Score by Index");
                        Console.WriteLine("4. Display Leaderboard with capped # of results");
                        Console.WriteLine("5. Exit");
            
                        string? leaderBoardInput = Console.ReadLine();

                        switch (leaderBoardInput)
                        {

                            case "1":
                                //call for game start
                                var localScores = leaderBoard.GetLocalScores(currentPlayer.Id);

                                Console.WriteLine("Your Scores:");
                                foreach (var s in localScores)
                                {
                                    Console.WriteLine($"{s.Username} : {s.ScoreCount}");
                                }

                                break;

                            case "2":
                                //All scores
                                var allScores = leaderBoard.GetTopScores();

                                Console.WriteLine("All Scores:");
                                foreach (var s in allScores)
                                {
                                    Console.WriteLine($"{s.Username} : {s.ScoreCount}");
                                }
                                break;
                            case "3":
                                //Leaderboard by index in ordered list by top score
                                Console.WriteLine("Enter an index (int) ");
            
                                int index = int.Parse(Console.ReadLine());
                                var score = leaderBoard.GetScore(index);

                                if (score != null)
                                {
                                    Console.WriteLine($"{score.Username} : {score.ScoreCount}");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid index.");
                                }

                                break;
                            case "4":
                                //Leaderboard with cap
                                Console.WriteLine("Enter a number(cap) (int) ");
            
                                int thing = int.Parse(Console.ReadLine());
                                var topScores = leaderBoard.GetTopScores(thing);

                                Console.WriteLine($"Top {thing} Scores:");
                                foreach (var s in topScores)
                                {
                                    Console.WriteLine($"{s.Username} : {s.ScoreCount}");
                                }

                                break;
                            //exit
                            case "5": 
                                inLeaderBoard = false;
                                break;

                            default:
                                Console.WriteLine("Invalid option.");
                                break;
                        }
                    }
                    break;
                
                case "3":
                    shop.DisplayMenu();
                    shop.BoughtItem(currentPlayer);
                    break;
                case "4":
                    int choice = 1;
                    Console.WriteLine("Welcome to the shop!");
                    foreach (var item in currentPlayer.PlayerSkins)
                    {
                        Console.WriteLine($"{choice}. {item.name}");
                        choice++;
                    }
                    Console.WriteLine("Enter the number of the item to buy:");
                    string? itemchoice = Console.ReadLine();
                    if (!int.TryParse(itemchoice, out int option))
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    ISkin selectedItem = currentPlayer.PlayerSkins[option - 1];
                    
                    Game.playerCar.EquipSkin(selectedItem);

                    //add item to player inventory
                    
                    
                    break;
                //exit
                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}

