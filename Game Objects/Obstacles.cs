namespace  Boulder_Dash;

public class Obstacles : TraversalEntity 
{
    public ObstacleShape Shape { get; set; }
    public Obstacles(int posX, ObstacleShape shape) : base(posX, 0)
    {
        Shape = shape;
    }

    public override void CollisionAction(TraversalEntity TEntity)
    {
        
    }

    public void DropObject(TraversalEntity Obstacle)
    {
        MoveDown();
    }
    public override String[] GetSprite()
    {
        return new string[]
        {
            " 8 ",
            "888",
            "888"
        };
    }

    public override void Step()
    {
        //Check if we are meeting with a car
        Game game = Game.GetGameInstance();
        TraversalEntity entity = game.getInstanceAtPosition(posX, posY + 1);
        
        Car playerCar = entity as Car;
        if (playerCar != null)
        {
            playerCar.HealthPoints--;
            game.RemoveInstance(this);
        }
        
        //Check if we are going to move off grid if so DELETE ourselves 
        if (posY + 1 >= game.columnLength) game.RemoveInstance(this);
        
        //If we are at the bottom then delete self, use the gamefield array sizes to figure this out. WHEN it becomes a signleton
        MoveDown();
    }
}