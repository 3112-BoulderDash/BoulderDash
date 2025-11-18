namespace Boulder_Dash___Final_Project;

public class GameField
{
    private bool gamePaused;
    private bool gameEnded;
    private int[,] gameArray =
    {
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,1,0}
    };
    private Clock gameClock = new Clock();

    public void StartGame()
    {
        if (gameEnded) gameEnded = false;
        //Reset Variables needed here
    }

    public void RunGame()
    {
        Render();
        
        //pass time with Clock 
        gameClock.passTime();
        if (gameClock.hasTickOccured())
        {

            //Run List of  functions
            
        }
        //Console.WriteLine(gameClock.);

}
    
    //Executes every tick.
    private void Render()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

        for (int row = 0; row < gameArray.GetLength(0); row++)
        {
            for (int col = 0; col < gameArray.GetLength(1); col++)
            {
                Console.Write(gameArray[row, col]);
            }

            //Next Line
            Console.WriteLine();
        }
    }

    //Runs all the other instances code, Executes every tick. 
    private void InstanceStep()
    {
        //Foreach loop for each element in the 2D array. 
    }
    
    

    public void AddInstance(int instance, int startX, int starY)
    {
        
    }

    public void MoveInstanceTo(int currX, int currY, int newX, int newY)
    {
        //Correct any invalid coordinates
        currY %= gameArray.GetLength(0); currY = int.Abs(currY);
        newY %= gameArray.GetLength(0); newY = int.Abs(newY);
        currX %= gameArray.GetLength(1); currX = int.Abs(currX);
        newX %= gameArray.GetLength(1); newX = int.Abs(newX);
        
        //Console.WriteLine($"Recent Coords: {currX}, {currY} \nNew Coords: {newX}, {newY}");
        
        if (!(currX == newX && currY == newY))
        {
            
            //Switch out the current index with a 0 and input us into the new area. 
            int instance = gameArray[currY, currX];
            gameArray[newY, newX] = instance;
            gameArray[currY, currX] = 0;
        }
    }

    public int[] whereIsOne()
    {
        for (int row = 0; row < gameArray.GetLength(0); row++)
        {
            for (int col = 0; col < gameArray.GetLength(1); col++)
            {
                if (gameArray[row, col] == 1) return new int[] { row, col };
            }
        }

        return new int[] {-1, -1};
    }
}