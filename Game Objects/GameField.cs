namespace Boulder_Dash;

public class GameField
{
    private bool gamePaused;
    private bool gameEnded;


    private Car playerCar;
    private List<TraversalEntity> activeEntities = new List<TraversalEntity>(); //List of current entities in the game
    
    private static int rowLength = 3; 
    private static int columnLength = 4;
    //The Array that will be used to update the game
    private TraversalEntity[,] gameArray = new TraversalEntity[rowLength, columnLength];
    
    private Clock gameClock = new Clock();
    
    //Game functions

    public void StartGame(Controller PlayerController)
    {
        if (gameEnded) gameEnded = false;
        //Reset Variables needed here
        
        //Create the Player character and give them the controller
        //AddInstance();
        playerCar = new Car(1, columnLength-1, CarTypes.Ferrari, PlayerController);
        activeEntities.Add(playerCar);
    }

    public void RunGame()
    {
        //Scan for player input 
        playerCar.ScanInputs();
        
        //Populates the game array that will be drawn.
        PopulateGameArray();
        
        //Render screen
        Render();
        
        
        //pass time with Clock 
        gameClock.passTime();
        if (gameClock.hasTickOccured())
        {
            //Run List of  functions
            
        }
        //Console.WriteLine(gameClock.);

    }
    
    //Executes every frame, updating the list of current objects that will be displayed. 
    private void PopulateGameArray()
    {
        //Populate GameArray with empty slots 
        for (int y = 0; y < columnLength; y++)
        {
            for (int x = 0; x < rowLength; x++)
            {
                //See if space is empty and fill with null if so
                gameArray[x, y] = null;
            }
        }
        
        //Loop through active entities and add them
        foreach (TraversalEntity e in activeEntities)
        {
            //If X and Y go out of bound then correct 
            if (e.posX < 0) e.posX = 0;
            if (e.posX >  rowLength - 1) e.posX = rowLength - 1;
            if (e.posY < 0) e.posY = 0;
            if (e.posY > columnLength - 1) e.posY = columnLength - 1;
            
            //IF this space has not been filled yet then we can fill it. 
            if (gameArray[e.posX, e.posY] == null)
            {
                gameArray[e.posX, e.posY] = e;
            }
        }
        
    }
    
    //Executes every time game needs to update screen
    private void Render()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

        //Create String were printing,
        string screenString = "";

        for (int col = 0; col < columnLength; col++) 
        {
            for (int row = 0; row < rowLength; row++)
            {
                if (gameArray[row, col] != null)
                {
                    //Update the string to space filled
                    screenString += gameArray[row, col].DrawMe();
                }
                else
                {
                    screenString += "-";
                }

            }

            //add new line char 
            screenString += "\n";
        }

        //Print string 
        Console.WriteLine(screenString);
    }

    //Runs all the other instances code, Executes every tick. 
    private void InstanceStep()
    {
        //Foreach loop for each element in the 2D array. 
    }

    //Adding travesalEntitys into game
    public void AddInstance(int instance, int startX, int starY)
    {
        //Put instance in new space, 
    }
    
    //Checking if space is free
    // public TraversalEntity GetInstanceFromSpace(int x, int y)
    // {
    //     return gameArray[x, y];
    // }

    // public void MoveInstanceTo(int currX, int currY, int newX, int newY)
    // {
    //     //Correct any invalid coordinates
    //     currY %= gameArray.GetLength(0); currY = int.Abs(currY);
    //     newY %= gameArray.GetLength(0); newY = int.Abs(newY);
    //     currX %= gameArray.GetLength(1); currX = int.Abs(currX);
    //     newX %= gameArray.GetLength(1); newX = int.Abs(newX);
    //     
    //     //Console.WriteLine($"Recent Coords: {currX}, {currY} \nNew Coords: {newX}, {newY}");
    //     
    //     if (!(currX == newX && currY == newY))
    //     {
    //         
    //         //Switch out the current index with a 0 and input us into the new area. 
    //         TraversalEntity? instance = gameArray[currY, currX];
    //         gameArray[newY, newX] = instance;
    //         gameArray[currY, currX] = null;
    //     }
    // }
    
    //Temporary function, will be scrapped after controls for player works. 

    // public int[] whereIsOne()
    // {
    //     for (int row = 0; row < rowLength; row++)
    //     {
    //         for (int col = 0; col < columnLength; col++)
    //         {
    //             //if (gameArray[row, col] == 1) return new int[] { row, col };
    //         }
    //     }
    //
    //     return new int[] {-1, -1};
    // }
}