namespace  Boulder_Dash;

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

    // public void RequestMoveLeft()
    // {
    //     MoveLeft();
    // }
    //
    // public void RequestMoveRight()
    // {
    //     MoveRight();
    // }
    //
    // public void RequestMoveDown()
    // {
    //     MoveDown();
    // }
    
    public abstract string DrawMe(); //Rename to RenderInstance, return an array. 
    
    public abstract void ScheduleUpdate();  //Runs every tick by gamefield+
}