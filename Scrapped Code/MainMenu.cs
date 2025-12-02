namespace Boulder_Dash;

public class MainMenu
{
    private AccountFactory _accountFactory;


    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Start Game");
            Console.WriteLine("3. Display Leaderboard");
            Console.WriteLine("4. Exit");
            
            string? input = Console.ReadLine();

            switch (input)
            {

                case "1":
                    //call for game start
                    break;

                case "2":
                    //Leaderboard menu
                    bool inLeaderBoard = true;

                    while (inLeaderBoard)
                    {
                        Console.WriteLine("1. Display your scores");
                        Console.WriteLine("2. Displa All Scores");
                        Console.WriteLine("3. Display Leaderboard");
                        Console.WriteLine("4. Exit");
            
                        string? leaderBoardInput = Console.ReadLine();

                        switch (leaderBoardInput)
                        {

                            case "1":
                                //call for game start
                                break;

                            case "2":
                                //Leaderboard menu
                    
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
