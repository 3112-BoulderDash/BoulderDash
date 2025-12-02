namespace Boulder_Dash;

public class GameField
{
    //Games instance, for singleton purposes 
    private static GameField gameInstance = null;
    private bool gamePaused;
    private bool gameEnded;


    private Car playerCar;
    private ObstacleSpawner obstacleSpawner;
    private List<TraversalEntity> activeEntities = new List<TraversalEntity>(); //List of current entities in the game

    public int rowLength { get; } = 3;

    public int columnLength { get; } = 8;
    
    //The Array that will be used to update the game
    private TraversalEntity[,] gameArray;
    
    private Clock gameClock = new Clock();
    private int speedUpTime = 30;
    private int speedUpTimer = 30;

    private GameField()
    {
        gameArray = new TraversalEntity[rowLength, columnLength];
        gamePaused = true;
        gameEnded = true;
    }
    
    //Get Game instance
    public static GameField GetGameInstance()
    {
        if (gameInstance == null) gameInstance = new GameField();
        return gameInstance;
    }
    
    //Game functions
    public void StartGame(Controller playerController)
    {
        if (gameEnded) gameEnded = false;
        if (gamePaused) gamePaused = false;
        //Reset Variables needed here
        
        //Create the Player character and give them the controller
        //AddInstance();
        playerCar = new Car(1, columnLength-1, CarTypes.Ferrari, playerController);
        AddInstance(playerCar);
        
        obstacleSpawner = new ObstacleSpawner();
    }

    public void RunGame()
    {
        if (!gameEnded && !gamePaused)
        {
            //Scan for player input 
            playerCar.ScanInputs();

            //Populates the game array that will be drawn.
            PopulateGameArray();

            //Render screen
            Render();


            //pass time with Clock 
            gameClock.passTime();
            //gameClock.TickSpeed = 15;
            if (gameClock.hasTickOccured())
            {
                //Run List of  functions
                InstanceStep();
                speedUpTimer--;
                if (speedUpTimer == 0)
                {
                    speedUpTimer = speedUpTime;
                    if (gameClock.TickSpeed > 14) gameClock.TickSpeed -= 2;
                }

                //Spawn Obstacles / Tick for obstacle creation
                obstacleSpawner.RequestObstacles();
            }
            //Console.WriteLine(gameClock.);

            //Check for ending condtions, end game if player is dead 
            if (playerCar.HealthPoints < 1) EndGame();
        }
        else if (gamePaused)
        {
            Console.WriteLine("|\tPaused\t|");
            Console.WriteLine("Choose one option");
            Console.WriteLine("1: Resume Game");
            Console.WriteLine("2: Quit Game");
            int reponse = 0;
            while (reponse != 0)
                try
                { 
                    reponse = Convert.ToInt32(Console.ReadLine());
                    if (reponse > 2 || reponse < 1)
                    {
                        Console.WriteLine("Invalid option! \n Please try again.");
                        reponse = 0;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input\n Please try again");
                }
        }
        else
        {
            gameEnded = true;
        }

    }

    public void EndGame()
    {
        gameEnded = true;
        
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

        Console.WriteLine("HP: " + playerCar.HealthPoints);
    }

    //Runs all the other instances code, Executes every tick. 
    private void InstanceStep()
    {
        //Foreach loop for each element in the 2D array. 
        for (int i = 0; i < activeEntities.Count; i++)
        {
            TraversalEntity e = activeEntities[i];
            
            //Run step function
            e.Step();

            if (activeEntities.IndexOf(e) == -1) i--;
        }
    }

    //Adding travesalEntitys into game
    public void AddInstance(TraversalEntity instance)
    {
        //Put instance in new space, 
        //Insert Instance by adding to the actives list and then run populate game array
        activeEntities.Add(instance);
        PopulateGameArray();
        
        //also Many objects need to be able to reference game field, maybe it should be a singleton?
    }

    public TraversalEntity getInstanceAtPosition(int x, int y)
    {
        //If X and Y go out of bounds then return null.
        if (x < 0 || x >  rowLength - 1 || y < 0 || y >  columnLength - 1 ) return null;
        
        return gameArray[x, y];
    }

    public void RemoveInstance(TraversalEntity instance)
    {
        activeEntities.Remove(instance);
        PopulateGameArray();
    }

    public bool GameIsRunning()
    {
        return !gameEnded;
    }
}