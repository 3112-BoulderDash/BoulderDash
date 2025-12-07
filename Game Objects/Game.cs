namespace Boulder_Dash;

public class Game
{
    //Games instance, for singleton purposes 
    private static Game gameInstance = null;
    private bool gamePaused;
    private bool gameEnded;
    
    private ObstacleSpawner obstacleSpawner;
    private List<TraversalEntity> activeEntities = new List<TraversalEntity>(); //List of current entities in the game
    public static Controller playerController = new Controller();
    public int Score { get; set; }
    
    private static int defaultRowLength = 3;
    public int rowLength { get; set; } = defaultRowLength;
    private static int defaultColLength = 8;
    public static  int columnLength { get; set; } = defaultColLength;
    public static Car playerCar;
    
    //Replace with creation of game renderer
    //The Array that will be used to update the game
    private GameRenderer gameRenderer;

    private static int defaultTickSpeed = 60;
    private int startTickSpeed = defaultTickSpeed;
    private Clock gameClock;
    private static int defaultSpeedUp = 5;
    private int speedUpTime = defaultSpeedUp;
    private int speedUpTimer;
    private static int defaultMinTickSpeed = 14;
    private int minTickSpeed = defaultMinTickSpeed;

    private Game()
    {
        gameRenderer = new GameRenderer(rowLength, columnLength);
        gamePaused = true;
        gameEnded = true;
        Score = 0;
    }
    
    //Get Game instance
    public static Game GetGameInstance()
    {
        if (gameInstance == null) gameInstance = new Game();
        return gameInstance;
    }
    
    //Game functions
    public void StartGame(ISkin selectedSkin)
    {
        //Reset Variables
        if (gameEnded) gameEnded = false;
        if (gamePaused) gamePaused = false;
        Score = 0;
        speedUpTimer = speedUpTime;
        
        //Reset instances
        activeEntities = new List<TraversalEntity>();
        gameRenderer = new GameRenderer(rowLength, columnLength);
        gameRenderer.PopulateGameArray(activeEntities);
        obstacleSpawner = new ObstacleSpawner();
        gameClock= new Clock(startTickSpeed);
        
        //Possibly rewrite so that type is for the cars skin instead and remove equpiskin from this function list
        playerCar = new Car(1, columnLength-1, CarTypes.Ferrari, playerController);
        playerCar.EquipSkin(selectedSkin);
        
        //Create the Player character and give them the controller
        AddInstance(playerCar);
    }

    public void RunGame()
    {
        if (!gameEnded && !gamePaused)
        {
            //Clear Screen
            Console.Clear();
            
            //Scan for player input 
            playerCar.ScanInputs();

            //Populates the game array that will be drawn.
            gameRenderer.PopulateGameArray(activeEntities);

            //Render screen
            gameRenderer.Render(playerCar.HealthPoints, Score);
            
            //pass time with Clock 
            gameClock.passTime();
            
            if (gameClock.hasTickOccured())
            {
                //Run List of  functions
                InstanceStep();
                speedUpTimer--;
                if (speedUpTimer == 0)
                {
                    speedUpTimer = speedUpTime;
                    if (gameClock.TickSpeed > minTickSpeed) gameClock.TickSpeed -= 2;
                }

                //Spawn Obstacles / Tick for obstacle creation
                obstacleSpawner.RequestObstacles();
                
                //Increase Score 
                Score += 1 * (60/gameClock.TickSpeed);
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
    
    
    //Remove Modifcations
    public void RemoveMods()
    {
        rowLength =  defaultRowLength;
        columnLength = defaultColLength; 
        startTickSpeed = defaultTickSpeed;
        minTickSpeed = defaultMinTickSpeed;
        speedUpTimer = defaultSpeedUp;
    }
    
    //Modification functions
    public void ModRowSize(int size)
    {
        rowLength = size;
    }
    
    public void ModColumnSize(int size)
    {
        columnLength = size;
    }

    public void ModStartTickSpeed(int miliseconds)
    {
        startTickSpeed = miliseconds;
    }

    public void ModMinTickSpeed(int miliseconds)
    {
        minTickSpeed = miliseconds;
    }

    public void ModSpeedUpTime(int ticks)
    {
        speedUpTime = ticks;
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
        gameRenderer.PopulateGameArray(activeEntities);
        
        //also Many objects need to be able to reference game field, maybe it should be a singleton?
    }

    public TraversalEntity getInstanceAtPosition(int x, int y)
    {
        //If X and Y go out of bounds then return null.
        if (x < 0 || x >  rowLength - 1 || y < 0 || y >  columnLength - 1 ) return null;
        foreach (TraversalEntity e in activeEntities)
        {
            if (e.posX == x && e.posY == y) return e;
        }
        return null;
    }

    public void RemoveInstance(TraversalEntity instance)
    {
        activeEntities.Remove(instance);
        gameRenderer.PopulateGameArray(activeEntities);
    }

    public bool GameIsRunning()
    {
        return !gameEnded;
    }
}