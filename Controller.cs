namespace BoulderDash;

public class Controller : IController
{
    public int AccountID { get; set; }
    private bool left;
    private bool right;
    private bool up;
    private bool down;

    public Controller(int accountID)
    {
        AccountID = accountID;
    }

    public void ReadInputs()
    {
        left = false;
        right = false;
        up = false;
        down = false;
        
        var input = Console.ReadKey(true).Key;

        switch (input)
        {
            case ConsoleKey.LeftArrow
                left = true;
                break;

            case ConsoleKey.RightArrow
                right = true;
                break;

            case ConsoleKey.UpArrow
                up = true;
                break;

            case ConsoleKey.DownArrow
                down = true;
                break;
        }
    }

    public bool LeftInput()
    {
        return left;
    }
    
    public bool RightInput()
    {
        return right;
    }
    
    public bool UpInput()
    {
        return up;
    }
    
    public bool DownInput()
    {
        return down;
    }
}