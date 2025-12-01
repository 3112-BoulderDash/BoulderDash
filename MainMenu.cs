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
                    //create account
                    Console.Write("Enter username: ");
                    string? username = Console.ReadLine();
                    _accountFactory.CreateAccount(username);
                        
                    break;

                case "2":
                    //call for game start
                    //gameField.RunGame();
                    break;

                case "3":
                    //call display leaderboard
                    //leaderBoard.Display();
                    break;
                //exit
                case "4":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
