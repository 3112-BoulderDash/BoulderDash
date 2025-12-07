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
        _game = Game.GetGameInstance();
        bool exitApplication = false;
        while (!exitApplication)
        {
            // Login screen
            IAccount? account = RunLoginMenu();

            if (account == null)
            {
                exitApplication = true;
                break;
            }
            currentPlayer = account;

            // main menu returns to login menu if logout occurs
            RunMainMenu();
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
            Console.WriteLine("3) Create ADMIN");
            Console.WriteLine("Exit");

            Console.Write("Enter 1,2,3 or any other key to exit: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? string.Empty;
                IAccount? account = AccountFactory.FindByUsername(username);

                if (account != null)
                {
                    var localScores = leaderBoard.GetLocalScores(account.Username);
                    int total = 0;
                    foreach (var s in localScores)
                    {
                        total += s.ScoreCount;
                    }
                    account.TotalPoints = total;

                    Console.WriteLine($"Welcome back, {account.Username} (ID {account.Id})!");
                    Console.WriteLine($"Your total points: {account.TotalPoints}");
                    Console.WriteLine("Press a key to continue...");
                    Console.ReadKey(true);
                    return account;
                }
                else
                {
                    Console.WriteLine("No account found. Press any key to return to menu.");
                    Console.ReadKey(true);
                }
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
                    Console.WriteLine("Press a key and retry.");
                    Console.ReadKey(true);
                }
            }
            else if (choice == "3") 
            {
                Console.Write("Choose an ADMIN username: ");
                string username = Console.ReadLine() ?? string.Empty;

                try
                {
                    IAccount account = AccountFactory.CreateAdminAccount(username);
                    Console.WriteLine($"ADMIN account successfully created! Username: {account.Username}, ID: {account.Id}");
                    Console.WriteLine("Press anything to navigate to menu:");
                    Console.ReadKey(true);
                    return account;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sorry couldn't create admin account: {ex.Message}");
                    Console.WriteLine("Press any key to return and retry.");
                    Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine("Closing Boulder Dash.");
                Console.ReadKey(true);
                return null; 
            }
        }
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
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Settings (Admin only)");

            
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

                    // log the score from game
                    int finalScore = _game.Score;

                    if (finalScore > 0)
                    {
                        currentPlayer.TotalPoints += finalScore;
                        IScoreCard card = new ScoreCard(currentPlayer.Username, finalScore);
                        leaderBoard.AddScore(card);
                        ScoreFileStorage.AppendScore(card);

                        Console.WriteLine($"Game over! You earned {finalScore} points this run.");
                        Console.WriteLine($"Your new total points: {currentPlayer.TotalPoints}");
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey(true);
                    }

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
                                
                                var localScores = leaderBoard.GetLocalScores(currentPlayer.Username);

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
                case "6":
                    if (currentPlayer.IsAdmin)
                    {
                        RunSettingsMenu();
                    }
                    else
                    {
                        Console.WriteLine("Sorry you need to be an admin in order to access settings.");
                        Console.WriteLine("Press to return to menu:");
                        Console.ReadKey(true);
                    }
                    break;

                default:
                    Console.WriteLine("Please select a valid option.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    // Admin-only settings menu
    private static void RunSettingsMenu()
    {
        if (!currentPlayer.IsAdmin)
            return;

        bool inSettings = true;

        while (inSettings)
        {
            Console.Clear();
            Console.WriteLine("==== Admin Settings ====");
            Console.WriteLine($"Admin: {currentPlayer.Username}");
            Console.WriteLine("1. Give myself points");
            Console.WriteLine("2. Control Difficulty");
            Console.WriteLine("3. Back to main menu");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter number of points to give yourself: ");
                    string? pointsInput = Console.ReadLine();
                    if (int.TryParse(pointsInput, out int points) && points > 0)
                    {
                        IScoreCard card = new ScoreCard(currentPlayer.Username, points);
                        leaderBoard.AddScore(card);
                        ScoreFileStorage.AppendScore(card);

                        currentPlayer.TotalPoints += points;

                        Console.WriteLine($"Gave {currentPlayer.Username} {points} points.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid points value.");
                    }
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey(true);
                    break;
                case "2":
                    //not implemented yet. 
                    Console.Write("Select difficulty: ");
                    break;
                case "3":
                    inSettings = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}