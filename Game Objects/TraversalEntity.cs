namespace BoulderDash;

public abstract class TraversalEntity
{
    public int posX { get; set; }
    public int posY { get; set; }
    
    protected TraversalEntity(int posX, int posY)
        {
        this.posX = posX;
        this.posY = posY;
        }

    public abstract void CollisionAction(TraversalEntity Tentity);

    protected void MoveLeft()
    {
        posX--;
    }

    protected void MoveRight()
    {
        posX++;
    }

    protected void MoveDown()
    {
        posY++;
    }

    public abstract string DrawMe(); //Rename to getSprite, return an array. 
    
    public abstract void ScheduleUpdate();  //Runs every tick by gamefield+
}