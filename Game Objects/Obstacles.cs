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
    
    public override String[] GetSprite()
    {
        switch (Shape)
        {
            case (ObstacleShape.Tree):
                return new string[]
                {
                    " 888 ",
                    "88888",
                    "  |  "
                };
            break;
            case (ObstacleShape.Cone):
                return new string[]
                {
                    "  _  ",
                    " /=\\ ",
                    "/___\\"
                };
            break;
            case (ObstacleShape.Rocks):
                return new string[]
                {
                    "  __ ",
                    " /0 \\",
                    "\\_0_/"
                };
                break;
            
            case (ObstacleShape.LooseTire):
                return new string[]
                {
                    " []  ",
                    "[][] ",
                    "(000)"
                };
                break;
            
            default: 
                return new string[]
                {
                    " 888 ",
                    "88888",
                    "  |  "
                };
                break;
        }
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
        if (posY + 1 >= Game.columnLength) game.RemoveInstance(this);
        
        //If we are at the bottom then delete self, use the gamefield array sizes to figure this out. WHEN it becomes a signleton
        MoveDown();
    }
}