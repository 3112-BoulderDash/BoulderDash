namespace Boulder_Dash;

public class GameRenderer
{
    //The Array that will be used to update the game
    private TraversalEntity[,] GameArray;
    private int columnLength;
    private int rowLength;

    public GameRenderer(int rowSize, int columnSize)
    {
        columnLength = columnSize;
        rowLength = rowSize;
        GameArray = new TraversalEntity[rowSize, columnSize];
    }
    
      //Updates the list of current objects that will be displayed. 
    public void PopulateGameArray(List<TraversalEntity> traversalEntities)
    {
        //Populate GameArray with empty slots 
        for (int y = 0; y < columnLength; y++)
        {
            for (int x = 0; x < rowLength; x++)
            {
                //See if space is empty and fill with null if so
                GameArray[x, y] = null;
            }
        }
        
        //Loop through active entities and add them
        foreach (TraversalEntity e in traversalEntities)
        {
            //If X and Y go out of bound then correct 
            if (e.posX < 0) e.posX = 0;
            if (e.posX >  rowLength - 1) e.posX = rowLength - 1;
            if (e.posY < 0) e.posY = 0;
            if (e.posY > columnLength - 1) e.posY = columnLength - 1;
            
            //IF this space has not been filled yet then we can fill it. 
            if (GameArray[e.posX, e.posY] == null)
            {
                GameArray[e.posX, e.posY] = e;
            }
        }
        
    }
    
    public int drawLength = 3;
    //Executes every time game needs to update screen
    public void Render(int health, int scoreNum)
    {

        //Create String were printing,
        string screenString = "";

        for (int col = 0; col < columnLength; col++)
        {
            for (int drawPos = 0; drawPos < drawLength; drawPos++)
            {
                for (int row = 0; row < rowLength; row++)
                {
                    screenString += " | ";
                    if (GameArray[row, col] != null)
                    {
                        //Update the string to space filled
                        screenString += GameArray[row, col].GetSprite()[drawPos];
                    }
                    else
                    {
                        screenString += "     ";
                    }

                }

                //add new line char 
                screenString += " |\n";
            }
            
            //END OF FOR LOOP
        }

        //Print string 
        Console.WriteLine(screenString);
        Console.WriteLine("HP: " +  health +  "\nScore: " + scoreNum );
    }
}